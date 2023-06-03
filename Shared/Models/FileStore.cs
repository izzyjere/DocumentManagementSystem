using System.ComponentModel.DataAnnotations;

namespace RTSADocs.Shared.Models
{
    public class FileStore : AuditableEntity
    {
        public List<Cabinet> Cabinets { get; set;}
        [Required]
        [MinLength(5)]
        public string Name { get; set;}
        public string ShortName { get; set;}
        public string? Path { get; set;}

    }
}
