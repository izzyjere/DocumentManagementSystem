using RTSADocs.Shared.Models;

namespace RTSADocs.Shared.DTOs
{
    public class LibraryDTO
    {      
        public Guid Id { get; set; }
        public Guid CabinetId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }         
        public string Code { get; set; }
    }
}
