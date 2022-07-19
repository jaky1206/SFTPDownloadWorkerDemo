using AutoMapper;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using SFTPDownloadWorkerDemo.Entities;
using SFTPDownloadWorkerDemo.Helpers;
using SFTPDownloadWorkerDemo.Services;

namespace SFTPDownloadWorkerDemo;

public sealed class WindowsBackgroundService : BackgroundService
{
    private readonly ILogger<WindowsBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private readonly DataContext _dataContext;
    private readonly ISFTPFileService _sFTPFileService;

    public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _dataContext = new DataContext(_configuration);
        _sFTPFileService = new SFTPFileService(_dataContext);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string host = _configuration.GetValue<string>("SFTPConfigurations:host");
                int port = _configuration.GetValue<int>("SFTPConfigurations:port");
                string username = _configuration.GetValue<string>("SFTPConfigurations:username");
                string password = _configuration.GetValue<string>("SFTPConfigurations:password");
                string remoteDirectory = _configuration.GetValue<string>("SFTPConfigurations:remotedirectory");
                string localdirectory = _configuration.GetValue<string>("SFTPConfigurations:localdirectory");

                // Path where the file should be saved once downloaded (locally)
                string pathLocalDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), localdirectory);

                using (SftpClient sftp = new SftpClient(host, port, username, password))
                {
                    try
                    {
                        sftp.Connect();

                        // Start download of the directory
                        DownloadDirectory(
                            sftp,
                            remoteDirectory,
                            pathLocalDirectory
                        );

                        sftp.Disconnect();
                    }
                    catch (Exception er)
                    {
                        _logger.LogError("An exception has been caught " + er.ToString());
                    }
                }

                _logger.LogInformation("Checing for new files after {0} minute(s).", 1);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Downloads a remote directory into a local directory
    /// </summary>
    /// <param name="client"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    private void DownloadDirectory(SftpClient client, string source, string destination, bool recursive = true)
    {
        // List the files and folders of the directory
        var files = client.ListDirectory(source);

        // Iterate over them
        foreach (SftpFile file in files)
        {
            // If is a file, download it
            if (!file.IsDirectory && !file.IsSymbolicLink)
            {
                DownloadFile(client, file, destination);
            }
            // If it's a symbolic link, ignore it
            else if (file.IsSymbolicLink)
            {
                _logger.LogInformation("Symbolic link ignored: {0}", file.FullName);
            }
            // If its a directory, create it locally (and ignore the .. and .=) 
            //. is the current folder
            //.. is the folder above the current folder -the folder that contains the current folder.
            else if (file.Name != "." && file.Name != "..")
            {
                var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                // and start downloading it's content recursively :) in case it's required
                if (recursive)
                {
                    DownloadDirectory(client, file.FullName, dir.FullName);
                }
            }
        }
    }

    /// <summary>
    /// Downloads a remote file through the client into a local directory
    /// </summary>
    /// <param name="client"></param>
    /// <param name="file"></param>
    /// <param name="directory"></param>
    private void DownloadFile(SftpClient client, SftpFile file, string directory)
    {
        _logger.LogInformation("Downloading {0}", file.FullName);

        using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
        {
            client.DownloadFile(file.FullName, fileStream);
            
            // saving file related information to the database.
            var fileRecord = new SFTPFile();
            fileRecord.FileName = file.Name;
            fileRecord.FilePath = directory;
            fileRecord.LastWriteTimeUtc = file.LastWriteTimeUtc;
            _logger.LogInformation("Information on {0} file is being saved to database: ", file.FullName);
            _sFTPFileService.Create(fileRecord);
        }
    }
}