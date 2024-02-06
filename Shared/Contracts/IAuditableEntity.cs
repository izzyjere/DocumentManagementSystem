namespace DMS.Shared.Contracts
{
    public interface IAuditableEntity : IEntity
    {
        // This interface represents an auditable entity in the system.

        DateTime CreatedOn { get; set; }
        // The timestamp when the entity was created.

        string CreatedBy { get; set; }
        // The user or system who created the entity.

        DateTime? LastModifiedOn { get; set; }
        // The timestamp when the entity was last modified.

        string? LastModifiedBy { get; set; }
        // The user or system who last modified the entity.
    }
}
