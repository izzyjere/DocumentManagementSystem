namespace RTSADocs.Shared.DTOs
{
    public class DocumentDTO
    {
        public Guid Id { get; set; } // Represents the unique identifier of the document
        public string Title { get; set; } // Represents the title of the document
        public string Description { get; set; } // Represents the description of the document
        public string SubmittedBy { get; set; } // Represents the name of the user who submitted the document
        public bool IsConfidential { get; set; } // Indicates whether the document is confidential or not
        public string Status { get; set; } // Represents the status of the document
        public DateTime SubmittedOn { get; set; } // Represents the date and time when the document was submitted
        public string Code { get; set; } // Represents the code associated with the document
        public Guid LibraryId { get; set; } // Represents the unique identifier of the library that the document belongs to
    }
}

