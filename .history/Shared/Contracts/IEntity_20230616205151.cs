namespace RTSADocs.Shared.Contracts
{
    public interface IEntity
    {
        // This interface represents an entity in the system.

        Guid Id { get; set; }
        // The unique identifier of the entity.
    }
}
