namespace DMS.Shared.Models
{
    public class Cabinet : AuditableEntity
    {
        public string Name { get; set; } // Name of the cabinet
        public string Code { get; set; } // Code representing the cabinet
        public Guid FileStoreId { get; set; } // ID of the associated file store
        public FileStore FileStore { get; set; } // Associated file store object
        public List<Library> Libraries { get; set; } // List of libraries within the cabinet
    }
}

