using SFTPDownloadWorkerDemo.Entities;

namespace SFTPDownloadWorkerDemo.Services
{
    public interface ISftpLogService
    {
        IEnumerable<SftpLog> GetAll();
        SftpLog GetById(int id);
        SftpLog GetByFullName(string fullName);
        bool doesFileExists(string fullName);
        void Create(SftpLog file);
        void Update(int id, SftpLog file);
        void Delete(int id);
    }
}
