namespace RTSADocs.Shared.Contracts
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
