global using RTSADocs.Shared.Contracts;

using System.ComponentModel.DataAnnotations.Schema;

namespace RTSADocs.Shared.Models
{
    public class Document : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubmittedBy { get; set; }
        public bool IsConfidential { get; set; }
        public string Status { get; set; } = Constants.Status.CREATE;
        public DateTime SubmittedOn { get; set; }
        public string Code { get; set; }
        public Guid LibraryId { get; set; }         
        public Library Library { get; set; }
        public List<PageFile> Pages { get; set; }


    }
}

