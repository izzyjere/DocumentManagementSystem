﻿global using RTSADocs.Shared.Contracts;
using RTSADocs.Shared.Constants;

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
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<PageFile> Pages { get; set; }

    }
}

