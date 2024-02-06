namespace DMS.Shared.Contracts
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        // This abstract class serves as a base for auditable entities in the system.

        public DateTime CreatedOn { get; set; }
        // The timestamp when the entity was created.

        public string CreatedBy { get; set; }
        // The user or system who created the entity.

        public DateTime? LastModifiedOn { get; set; }
        // The timestamp when the entity was last modified.

        public string? LastModifiedBy { get; set; }
        // The user or system who last modified the entity.
    }
}
