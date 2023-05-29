namespace Application.Tests.Queries.Columns
{
    public class GetProfitStatisticsQueryHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetProfitStatisticsQueryHandlerValidSeed))]
        public async Task GetTicketsPerDrawHandler_ValidParameters(
            int id,
            double price,
            double profit,
            int drawId,
            int listAsyncTimesCalled
            )
        {
            var existingTicket = new TicketBuilder()
                .WithId(id)
                .WithPrice(price)
                .WithProfit(profit)
                .WithDrawId(drawId)
                .Build();

            // Arrange
            var ticketRepository = new Mock<IRepository<Ticket>>();
            var query = new GetProfitStatisticsQuery();
            var handler = new GetProfitStatisticsQueryHandler(ticketRepository.Object);
            var existingTickets = new List<Ticket>() { existingTicket };

            ticketRepository.Setup(x => x.ListAsync(It.IsAny<GetTicketsPerYearSpecification>(), default)).ReturnsAsync(existingTickets);

            // Act
            var result = await handler.Handle(query, default);
            var data = result.Data;
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Succeeded);
            Assert.IsType<Result<List<object>>>(result);
            Assert.NotEmpty((System.Collections.IEnumerable)result.Data);
            ticketRepository.Verify(x => x.ListAsync(It.IsAny<Ardalis.Specification.ISpecification<Ticket>>(), default), Times.Exactly(listAsyncTimesCalled));
        }
    }
}
