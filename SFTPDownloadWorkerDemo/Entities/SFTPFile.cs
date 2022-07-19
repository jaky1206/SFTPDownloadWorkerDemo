

using System.ComponentModel.DataAnnotations.Schema;

namespace SFTPDownloadWorkerDemo.Entities
{
    public class SFTPFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
    }
}


