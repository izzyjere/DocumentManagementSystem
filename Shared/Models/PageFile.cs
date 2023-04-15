namespace RTSADocs.Shared.Models
{
    public class PageFile : Entity
    {
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public Document Document { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
