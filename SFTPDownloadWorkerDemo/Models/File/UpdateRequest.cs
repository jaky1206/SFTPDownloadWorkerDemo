namespace SFTPDownloadWorkerDemo.Models.File
{
    public class UpdateRequest
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
