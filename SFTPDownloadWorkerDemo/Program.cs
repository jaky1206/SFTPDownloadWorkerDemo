using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

using SFTPDownloadWorkerDemo;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "SFTP Download Worker Demo";
    })
    .ConfigureServices(services =>
    {
        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(services);

        services.AddSingleton<SFTP>();
        services.AddHostedService<WindowsBackgroundService>();
    })
    .ConfigureLogging((context, logging) =>
    {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
            context.Configuration.GetSection("Logging"));
    })
    .Build();

await host.RunAsync();