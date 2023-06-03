namespace RTSADocs.Shared.Models
{
    public class Cabinet : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid FileStoreId { get; set; }
        public FileStore FileStore { get; set; }
        public List<Library> Libraries { get; set; }
    }
}
