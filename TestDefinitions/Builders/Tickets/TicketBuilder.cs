namespace TestDefinitions.Builders.Tickets
{
    public class TicketBuilder
    {
        public int id = 1;
        public double price = 1;
        public double profit = 1;
        public int drawId = 1;
        public int columnId = 2;
        public string selectionNumbers = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20";
        public int selectionGame = 2;
        public int multiplier = 2;
        public double columnPrice = 2.12345;
        public bool kinoBonus = true;
        public bool selectionRandom = true;
        public bool cancel = true;
        public double columnProfit = 2;
        public int columnSuccess = 2;

        public List<Column> columns = new List<Column>();

        public Ticket Build()
        {
            var ticket = Ticket.Create(id, price, profit, drawId, columns);
            var column = Column.Create(
                columnId,
                selectionNumbers,
                selectionGame,
                multiplier,
                columnPrice,
                kinoBonus,
                selectionRandom,
                cancel,
                columnProfit,
                columnSuccess
                );

            this.columns.Add(column);
            return ticket;
        }
        public TicketBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }
        public TicketBuilder WithPrice(double price)
        {
            this.price = price;
            return this;
        }
        public TicketBuilder WithProfit(double profit)
        {
            this.profit = profit;
            return this;
        }
        public TicketBuilder WithDrawId(int drawId)
        {
            this.drawId = drawId;
            return this;
        }
        //public TicketBuilder WithColumns(
        //    int columnId,
        //    string selectionNumbers,
        //    int selectionGame,
        //    int multiplier,
        //    double columnPrice,
        //    bool kinoBonus,
        //    bool selectionRandom,
        //    bool cancel,
        //    double columnProfit,
        //    int columnSuccess
        //    )
        //{
        //    this.columnId = columnId;
        //    this.selectionNumbers = selectionNumbers;
        //    this.selectionGame = selectionGame;
        //    this.multiplier = multiplier;
        //    this.columnPrice = columnPrice;
        //    this.kinoBonus = kinoBonus;
        //    this.selectionRandom = selectionRandom;
        //    this.cancel = cancel;
        //    this.columnProfit = columnProfit;
        //    this.columnSuccess = columnSuccess;
        //    return this;
        //}
        public static implicit operator Ticket(TicketBuilder instance)
        {
            return instance.Build();
        }
    }
}
