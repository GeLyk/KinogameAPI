namespace TestDefinitions.Builders.Columns
{
    public class ColumnBuilder
    {
        public int id = 1;
        public string selectionNumbers = "selectionNumbers";
        public int selectionGame = 1;
        public int multiplier = 1;
        public double price = 1;
        public bool kinoBonus = true;
        public bool selectionRandom = true;
        public bool cancel = true;
        public double profit = 1;
        public int success = 1;
        public Column Build()
        {
            var ticket = Column.Create(
                id,
                selectionNumbers,
                selectionGame,
                multiplier,
                price,
                kinoBonus,
                selectionRandom,
                cancel,
                profit,
                success
                );

            return ticket;
        }
        public ColumnBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }
        public ColumnBuilder WithSelectionNumbers(string selectionNumbers)
        {
            this.selectionNumbers = selectionNumbers;
            return this;
        }
        public ColumnBuilder WithSelectionGame(int selectionGame)
        {
            this.selectionGame = selectionGame;
            return this;
        }
        public ColumnBuilder WithMultiplier(int multiplier)
        {
            this.multiplier = multiplier;
            return this;
        }
        public ColumnBuilder WithPrice(double price)
        {
            this.price = price;
            return this;
        }
        public ColumnBuilder WithKinoBonus(bool kinoBonus)
        {
            this.kinoBonus = kinoBonus;
            return this;
        }
        public ColumnBuilder WithSelectionRandom(bool selectionRandom)
        {
            this.selectionRandom = selectionRandom;
            return this;
        }
        public ColumnBuilder WithCancel(bool cancel)
        {
            this.cancel = cancel;
            return this;
        }
        public ColumnBuilder WithProfit(double profit)
        {
            this.profit = profit;
            return this;
        }
        public ColumnBuilder WithSuccess(int success)
        {
            this.success = success;
            return this;
        }

        public static implicit operator Column(ColumnBuilder instance)
        {
            return instance.Build();
        }
    }
}
