namespace RTSADocs.Shared.Models
{
    public class Library : Entity
    {
        public Guid CabinetId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Cabinet Cabinet { get; set;}
        public string Code { get; set; }
        public List<Document> Documents { get; set; }
    }
}
