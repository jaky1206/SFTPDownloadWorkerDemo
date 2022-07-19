using System.ComponentModel.DataAnnotations.Schema;

namespace SFTPDownloadWorkerDemo.Entities
{
    public class SftpLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public long Length { get; set; }
        public string LocalFilePath { get; set; }
    }
}


