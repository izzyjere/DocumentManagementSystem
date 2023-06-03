namespace RTSADocs.Shared.Models
{
    public class DocumentSource : AuditableEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<Document> Documents { get; set; }
    }
}
