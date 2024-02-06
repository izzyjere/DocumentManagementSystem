global using DMS.Shared.Contracts;

using System.ComponentModel.DataAnnotations;

namespace DMS.Shared.Models
{
    public class Document : AuditableEntity
    {
        [Required]
        public string Title { get; set; } // Title of the document
        [Required]
        public string Description { get; set; } // Description of the document
        
        public string? SubmittedBy { get; set; } // Optional property representing the submitter of the document

        public bool IsConfidential { get; set; } // Flag indicating if the document is confidential
        public string Status { get; set; } = Constants.Status.CREATE; // Status of the document, default value is "CREATE"
        public DateTime SubmittedOn { get; set; } // Date and time when the document was submitted
        [Required]
        public string Code { get; set; } // Code representing the document
        public Guid LibraryId { get; set; } // ID of the associated library
        public Library Library { get; set; } // Associated library object
        public List<PageFile> Pages { get; set; } // List of page files belonging to the document
    }
}


