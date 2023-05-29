namespace Domain.ValueObjects.Tickets
{
    public class TicketProfit : ValueObject
    {
        public double Profit { get; private set; }
        public TicketProfit(double profit)
        {
            Profit = Guard.Against.Null(profit, nameof(profit));
        }
        public static TicketProfit Create(double profit)
        {
            return new TicketProfit(profit);
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
