using SFTPDownloadWorkerDemo.Entities;
using SFTPDownloadWorkerDemo.Helpers;

namespace SFTPDownloadWorkerDemo.Services

{
    public class SftpLogService : ISftpLogService
    {
        private DataContext _context;

        #region PUBLIC METHODS
        public SftpLogService(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get All Records from the Database
        /// </summary>
        /// <returns>SftpLog records</returns>
        public IEnumerable<SftpLog> GetAll()
        {
            return _context.SftpLogs;
        }
        /// <summary>
        /// Get record by Id which calls the getftpLogById method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SftpLog record</returns>
        public SftpLog GetById(int id)
        {
            return getftpLogById(id);
        }
        /// <summary>
        /// Get record by FullName which calls the getftpLogByFullName method
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public SftpLog GetByFullName(string fullName)
        {
            return getftpLogByFullName(fullName);
        }
        /// <summary>
        /// Check whether a record exists or not
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>true or false</returns>
        public bool doesFileExists(string fullName)
        {
            SftpLog sftpLog = getftpLogByFullName(fullName);
            return sftpLog is null ? false : true;
        }
        /// <summary>
        /// create a recrd
        /// </summary>
        /// <param name="file"></param>
        /// <exception cref="AppException"></exception>
        public void Create(SftpLog file)
        {
            // save
            _context.SftpLogs.Add(file);
            _context.SaveChanges();
        }
        /// <summary>
        /// update a single record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <exception cref="AppException"></exception>
        public void Update(int id, SftpLog file)
        {
            SftpLog _file = GetById(id);
            // save
            _context.SftpLogs.Update(file);
            _context.SaveChanges();
        }
        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var file = GetById(id);
            _context.SftpLogs.Remove(file);
            _context.SaveChanges();
        }
        #endregion

        #region HELPER METHODS
        /// <summary>
        /// gets a single record by primary id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SftpLog record</returns>
        private SftpLog getftpLogById(int id)
        {
            return _context.SftpLogs.Find(id);
        }
        /// <summary>
        /// selects a single record based on Full Name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>SftpLog record</returns>
        private SftpLog getftpLogByFullName(string fullName)
        {
            return _context.SftpLogs.FirstOrDefault(x => x.FullName == fullName);
        }
        #endregion
    }
}







