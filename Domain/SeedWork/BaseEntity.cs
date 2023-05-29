namespace Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; protected set; }
    }
}
