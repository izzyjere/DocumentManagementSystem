namespace DMS.Shared.DTOs
{
    public class LibraryDTO
    {
        public Guid Id { get; set; } // Represents the unique identifier of the library
        public Guid CabinetId { get; set; } // Represents the unique identifier of the cabinet that the library belongs to
        public string Name { get; set; } // Represents the name of the library
        public string Path { get; set; } // Represents the path of the library
        public string Code { get; set; } // Represents the code associated with the library
    }
}

