namespace Domain.ValueObjects.Columns
{
    public class ColumnSuccess : ValueObject
    {
        public int Success { get; private set; }
        public ColumnSuccess(int success)
        {
            Success = Guard.Against.Null(success, nameof(success));
        }
        public static ColumnSuccess Create(int success)
        {
            return new ColumnSuccess(success);
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
