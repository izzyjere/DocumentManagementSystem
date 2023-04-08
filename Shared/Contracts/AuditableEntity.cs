namespace RTSADocs.Shared.Contracts
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
