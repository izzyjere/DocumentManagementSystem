using System.ComponentModel.DataAnnotations;

namespace DMS.Shared.Models
{
    public class FileStore : AuditableEntity
    {
        public List<Cabinet> Cabinets { get; set; } // Represents the list of cabinets associated with the file store

        [Required]
        [MinLength(5)]
        public string Name { get; set; } // Represents the name of the file store

        public string ShortName { get; set; } // Represents the short name or abbreviation of the file store

        public string? Path { get; set; } // Represents the path of the file store (optional)
    }
}
