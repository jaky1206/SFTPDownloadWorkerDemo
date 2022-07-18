using System.ComponentModel.DataAnnotations;

namespace SFTPDownloadWorkerDemo.Models.File
{
    public class CreateRequest
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public DateTime DownloadDate { get; set; }
    }
}


