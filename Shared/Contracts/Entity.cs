using System.ComponentModel.DataAnnotations;

namespace RTSADocs.Shared.Contracts
{
    public abstract class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
