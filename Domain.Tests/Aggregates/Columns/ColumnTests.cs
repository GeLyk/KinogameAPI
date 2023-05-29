namespace Domain.Tests.Aggregates.Columns
{
    public class ColumnTests
    {
        [Theory]
        [ClassData(typeof(ColumnValidSeed))] 
        public void Create_ValidParameters(
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
            var column = new ColumnBuilder()
                .WithId(id)
                .WithSelectionNumbers(selectionNumbers)
                .WithSelectionGame(selectionGame)
                .WithMultiplier(multiplier)
                .WithPrice(price)
                .WithKinoBonus(kinoBonus)
                .WithSelectionRandom(selectionRandom)
                .WithCancel(cancel)
                .WithProfit(profit)
                .WithSuccess(success)
                .Build();

            Assert.NotNull(column);
            Assert.Equal(id, column.Id);
            Assert.Equal(selectionNumbers, column.SelectionNumbers);
            Assert.Equal(selectionGame, column.SelectionGame);
            Assert.Equal(multiplier, column.Multiplier);
            Assert.Equal(price, column.Price);
            Assert.Equal(kinoBonus, column.KinoBonus);
            Assert.Equal(selectionRandom, column.SelectionRandom);
            Assert.Equal(cancel, column.Cancel);
            Assert.Equal(profit, column.Profit.Profit);
            Assert.Equal(success, column.Success.Success);
        }

        [Theory]
        [ClassData(typeof(UpdateColumnProfitValidSeed))]
        public void UpdateColumnProfit_ValidParameters(double profit)
        {
            var column = new ColumnBuilder()
                .WithProfit(profit)
                .Build();

            Assert.NotNull(column);
            Assert.Equal(profit, column.Profit.Profit);
        }

        [Theory]
        [ClassData(typeof(UpdateColumnSuccessValidSeed))]
        public void UpdateColumnSuccess_ValidParameters(int success)
        {
            var column = new ColumnBuilder()
                .WithSuccess(success)
                .Build();

            Assert.NotNull(column);
            Assert.Equal(success, column.Success.Success);
        }
    }
}
