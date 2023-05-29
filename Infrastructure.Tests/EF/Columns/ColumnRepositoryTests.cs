namespace Infrastructure.Tests.EF.Columns
{
    public class ColumnRepositoryTests : RepositoryTests
    {
        [Fact]
        public async Task Create_Column_Success_Test()
        {
            // Arrange
            var column = new ColumnBuilder()
                .Build();

            var columnsRepo = new EfRepository<Column>(_dbContext);

            // Act
            await columnsRepo.AddAsync(column);
            var columns = await columnsRepo.ListAsync();

            // Assert
            var createdColumn = await _dbContext.Columns.FindAsync(column.Id);
            Assert.NotNull(createdColumn);
            Assert.Equal(column, createdColumn);
            Assert.Equal(1, columns.Count);
        }

        [Theory]
        [ClassData(typeof(UpdateColumnValidSeed))]
        public async Task Update_Column_Success_Test(
            double profit,
            int success,
            double updatedProfit,
            int updatedSuccess
            )
        {
            // Arrange
            var column = new ColumnBuilder()
                .WithProfit(profit)
                .WithSuccess(success)
                .Build();

            var columnsRepo = new EfRepository<Column>(_dbContext);
            await columnsRepo.AddAsync(column);

            // Act
            column.UpdateColumnProfit(updatedProfit);
            column.UpdateColumnSuccess(updatedSuccess);

            await columnsRepo.UpdateAsync(column);

            // Assert
            var updatedColumn = await _dbContext.Columns.FindAsync(column.Id);
            Assert.Equal(updatedProfit, updatedColumn.Profit.Profit);
            Assert.Equal(updatedSuccess, updatedColumn.Success.Success);
        }
    }
}
