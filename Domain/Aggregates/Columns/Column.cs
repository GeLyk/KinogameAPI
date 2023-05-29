namespace Domain.Aggregates.Columns
{
    public class Column : BaseEntity<int>, IAggregateRoot
    {
        public int Id { get; private set; }
        public string SelectionNumbers { get; private set; }
        public int SelectionGame { get; private set; }
        public int Multiplier { get; private set; }
        public double Price { get; private set; }
        public bool KinoBonus { get; private set; }
        public bool SelectionRandom { get; private set; }
        public bool Cancel { get; private set; }
        public ColumnProfit Profit { get; private set; }
        public ColumnSuccess Success { get; private set; }

        private Column() { }
        public Column(
            int id,
            string selectionNumbers,
            int selectionGame,
            int multiplier,
            double price,
            bool kinoBonus,
            bool selectionRandom,
            bool cancel,
            ColumnProfit profit,
            ColumnSuccess success
            )
        {
            Id = Guard.Against.Null(id, nameof(id));
            SelectionNumbers = selectionNumbers;
            SelectionGame = selectionGame;
            Multiplier = multiplier;
            Price = price;
            KinoBonus = kinoBonus;
            SelectionRandom = selectionRandom;
            Cancel = cancel;
            Profit = profit;
            Success = success;
        }
        public static Column Create(
            int id,
            string selectionNumbers,
            int selectionGame,
            int multiplier,
            double price,
            bool kinoBonus,
            bool selectionRandom,
            bool cancel,
            double profit,
            int success
            )
        {
            var columnProfit = ColumnProfit.Create(profit: profit);
            var columnSuccess = ColumnSuccess.Create(success: success);

            var column = new Column(
                id: id,
                selectionNumbers: selectionNumbers,
                selectionGame: selectionGame,
                multiplier: multiplier,
                price: price,
                kinoBonus: kinoBonus,
                selectionRandom: selectionRandom,
                cancel: cancel,
                profit: columnProfit,
                success: columnSuccess
                );

            return column;
        }
        public void UpdateColumnProfit(double profit)
        {
            var columnProfit = ColumnProfit.Create(profit: profit);

            Profit = columnProfit;
        }
        public void UpdateColumnSuccess(int success)
        {
            var columnSuccess = ColumnSuccess.Create(success: success);

            Success = columnSuccess;
        }
    }
}



