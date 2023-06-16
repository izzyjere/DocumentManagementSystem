namespace RTSADocs.Shared.Models
{
    public class PageFile : Entity
    {
        public string Path { get; set; } // Represents the path of the page file
        public string Format { get; set; } // Represents the format of the page file
        public string FileName { get; set; } // Represents the file name of the page file
        public Document Document { get; set; } // Represents the associated document entity
        public DateTime DateUploaded { get; set; } // Represents the date and time when the file was uploaded
    }
}
