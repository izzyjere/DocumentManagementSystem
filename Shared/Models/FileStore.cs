namespace RTSADocs.Shared.Models
{
    public class FileStore : Entity
    {
        public List<Cabinet> Cabinets { get; set;}
        public string Name { get; set;}
        public string Path { get; set;}
    }
}
