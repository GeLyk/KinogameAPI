namespace Domain.Aggregates.Tickets
{
    public class Ticket : BaseEntity<int>, IAggregateRoot
    {
        public int Id { get; private set; }
        public double Price { get; private set; }
        public TicketProfit Profit { get; private set; }
        public int DrawId { get; private set; }
        public List<Column> Columns { get;  private set; }  

        private Ticket() { }
        public Ticket(
            int id,
            double price,
            TicketProfit profit,
            int drawId,
            List<Column> columns
            )
        {
            Id = Guard.Against.Null(id, nameof(id));
            Price = Guard.Against.Null(price, nameof(price));
            Profit = profit;
            DrawId = Guard.Against.Null(drawId, nameof(drawId));
            Columns = columns;
        }

        public static Ticket Create(
            int id,
            double price,
            double profit,
            int drawId,
            List<Column> columns
            )
        {
            var ticketProfit = TicketProfit.Create(profit: profit);

            var ticket = new Ticket(
                id: id,
                price: price,
                profit: ticketProfit,
                drawId: drawId,
                columns: columns
                );

            return ticket;
        }
        public void UpdateDrawId(int id)
        {
            DrawId = id;
        }
        public void UpdateTicketProfit(double profit)
        {
            var ticketProfit = TicketProfit.Create(profit: profit);

            Profit = ticketProfit;
        }
    }
}
