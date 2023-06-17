namespace RTSADocs.Shared.DTOs
{
    public class DocumentDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubmittedBy { get; set; }
        public bool IsConfidential { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string Code { get; set; }
        public Guid LibraryId { get; set; }
    }
}
