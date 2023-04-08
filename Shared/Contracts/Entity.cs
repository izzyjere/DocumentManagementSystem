using System.ComponentModel.DataAnnotations;

namespace RTSADocs.Shared.Contracts
{
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Represents the primary key of this entity. 
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
