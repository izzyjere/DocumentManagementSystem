namespace RTSADocs.Shared.Models
{
    public class Library : AuditableEntity
    {
        public Guid CabinetId { get; set; } // Represents the ID of the associated cabinet

        public string Name { get; set; } // Represents the name of the library

        public Cabinet Cabinet { get; set; } // Represents the associated cabinet entity

        public string Code { get; set; } // Represents the code of the library

        public List<Document> Documents { get; set; } // Represents the list of documents in the library

        public bool HasFiles()
        {
            if (Documents is not null) // Checks if the list of documents is not null
            {
                return Documents.Any(); // Returns true if there are any documents in the list
            }
            return false; // Returns false if the list of documents is null or empty
        }
    }
}

