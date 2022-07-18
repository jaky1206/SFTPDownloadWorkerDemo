//using AutoMapper;
//using BCrypt.Net;
using AutoMapper;
using SFTPDownloadWorkerDemo.Entities;
using SFTPDownloadWorkerDemo.Helpers;
using SFTPDownloadWorkerDemo.Models.File;

namespace SFTPDownloadWorkerDemo.Services

{
    public interface ISFTPFileService
    {
        IEnumerable<SFTPFile> GetAll();
        SFTPFile GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class SFTPFileService : ISFTPFileService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public SFTPFileService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<SFTPFile> GetAll()
        {
            return _context.SFTPFiles;
        }

        public SFTPFile GetById(int id)
        {
            return getSFTPFile(id);
        }

        public void Create(CreateRequest model)
        {
            // validate
            if (_context.SFTPFiles.Any(x => x.DownloadDate == model.DownloadDate && x.FileName == model.FileName))
                throw new AppException("File already exists");

            // map model to new user object
            var file = _mapper.Map<SFTPFile>(model);

            // save user
            file.FileName = "teest";
            file.FilePath = "test Path";
            file.DownloadDate = DateTime.Now;

            _context.SFTPFiles.Add(file);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var file = getSFTPFile(id);

            // validate
            if (_context.SFTPFiles.Any(x => x.DownloadDate == model.DownloadDate && x.FileName == model.FileName))
                throw new AppException("File already exists");

            // copy model to user and save
            _mapper.Map(model, file);
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







