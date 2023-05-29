namespace Domain.SeedWork
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract void Validate();
        protected abstract IEnumerable<object> GetEqualityComponents();
        public bool Equals(ValueObject? other)
        {
            if (other == null)
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
    }
}
