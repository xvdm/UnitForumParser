namespace Domain.Entities.Base;

public abstract class Auditable
{
    /// <summary>
    /// When this entity was added to the database
    /// </summary>
    public DateTime? EntityCreatedAt { get; set; }
    
    /// <summary>
    /// Last time this entity was modified
    /// </summary>
    public DateTime? EntityModifiedAt { get; set; }
}