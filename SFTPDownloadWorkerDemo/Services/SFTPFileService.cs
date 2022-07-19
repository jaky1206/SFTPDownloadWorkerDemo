using SFTPDownloadWorkerDemo.Entities;
using SFTPDownloadWorkerDemo.Helpers;

namespace SFTPDownloadWorkerDemo.Services

{
    public interface ISFTPFileService
    {
        IEnumerable<SFTPFile> GetAll();
        SFTPFile GetById(int id);
        void Create(SFTPFile file);
        void Update(int id, SFTPFile file);
        void Delete(int id);
    }

    public class SFTPFileService : ISFTPFileService
    {
        private DataContext _context;

        public SFTPFileService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SFTPFile> GetAll()
        {
            return _context.SFTPFiles;
        }

        public SFTPFile GetById(int id)
        {
            return getSFTPFile(id);
        }

        public void Create(SFTPFile file)
        {
            // validate
            if (_context.SFTPFiles.Any(x => x.FileName == file.FileName&& x.FilePath == file.FilePath))
            {
                throw new AppException("File already exists");
            }

            // save
            _context.SFTPFiles.Add(file);
            _context.SaveChanges();
        }

        public void Update(int id, SFTPFile file)
        {
            SFTPFile _file = getSFTPFile(id);

            // validate
            if (_context.SFTPFiles.Any(x => x.FileName == file.FileName && x.FilePath == file.FilePath))
            {
                throw new AppException("File already exists");
            }

            // save
            _context.SFTPFiles.Update(file);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var file = getSFTPFile(id);
            _context.SFTPFiles.Remove(file);
            _context.SaveChanges();
        }

        // helper methods

        private SFTPFile getSFTPFile(int id)
        {
            var file = _context.SFTPFiles.Find(id);
            if (file == null) throw new KeyNotFoundException("File not found");
            return file;
        }
    }
}







