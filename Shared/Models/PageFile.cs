namespace RTSADocs.Shared.Models
{
    public class PageFile : Entity
    {
        public string Path { get; set; }
        public string Format { get; set; }
        public Document Document { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
