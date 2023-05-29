namespace Domain.ValueObjects.Columns
{
    public class ColumnProfit : ValueObject
    {
        public double Profit { get; private set; }
        public ColumnProfit(double profit)
        {
            Profit = Guard.Against.Null(profit, nameof(profit));
        }
        public static ColumnProfit Create(double profit)
        {
            return new ColumnProfit(profit);
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
