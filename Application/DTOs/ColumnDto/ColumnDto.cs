namespace Application.DTO.ColumnDto 
{
    public class ColumnDto : BaseEntity<int>, IAggregateRoot
    {
        public int Id { get; private set; }
        public string SelectionNumbers { get; private set; }
        public int SelectionGame { get; private set; }
        public int Multiplier { get; private set; }
        public double Price { get; private set; }
        public bool KinoBonus { get; private set; }
        public bool SelectionRandom { get; private set; }
        public bool Cancel { get; private set; }
        public double Profit { get; private set; }
        public int Success { get; private set; }

        private ColumnDto() { }
        public ColumnDto(
            int id,
            string selectionNumbers,
            int selectionGame,
            int multiplier,
            double price,
            bool kinoBonus,
            bool selectionRandom,
            bool cancel,
            double profit,
            int success,
            DateTime createdOn,
            DateTime? lastModifiedOn
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
        public static ColumnDto Create(
            int id,
            string selectionNumbers,
            int selectionGame,
            int multiplier,
            double price,
            bool kinoBonus,
            bool selectionRandom,
            bool cancel,
            double profit,
            int success,
            DateTime createdOn,
            DateTime? lastModifiedOn
            )
        {
            var column = new ColumnDto(
                id: id,
                selectionNumbers: selectionNumbers,
                selectionGame: selectionGame,
                multiplier: multiplier,
                price: price,
                kinoBonus: kinoBonus,
                selectionRandom: selectionRandom,
                cancel: cancel,
                profit: profit,
                success: success,
                createdOn: createdOn,
                lastModifiedOn: lastModifiedOn
                );

            return column;
        }
    }
}
