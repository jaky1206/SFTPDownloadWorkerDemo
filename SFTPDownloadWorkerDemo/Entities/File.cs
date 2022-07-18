

using System.ComponentModel.DataAnnotations.Schema;

namespace SFTPDownloadWorkerDemo.Entities
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DownloadDate { get; set; }
    }
}


