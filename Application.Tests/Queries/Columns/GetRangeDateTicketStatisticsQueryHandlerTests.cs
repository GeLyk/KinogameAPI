namespace Application.Tests.Queries.Columns
{
    public class GetRangeDateTicketStatisticsQueryHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetRangeDateTicketStatisticsQueryHandlerValidSeed))]
        public async Task GetTicketsPerDrawHandler_ValidParameters(
           int id,
           double price,
           double profit,
           int drawId,
           string startDate,
           string endDate,
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
            var query = new GetRangeDateTicketStatisticsQuery(startDate: startDate, endDate: endDate);
            var handler = new GetRangeDateTicketStatisticsQueryHandler(ticketRepository.Object);
            var existingTickets = new List<Ticket>() { existingTicket };

            ticketRepository.Setup(x => x.ListAsync(It.IsAny<GetRangeDateTicketStatisticsSpecification>(), default)).ReturnsAsync(existingTickets);

            // Act
            var result = await handler.Handle(query, default);
            var data = result.Data;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Succeeded);
            Assert.Equal(existingTickets.Count, result.Data.Count);
            Assert.NotEmpty((System.Collections.IEnumerable)result.Data);
            for (var i = 0; i < existingTickets.Count; i++)
            {
                var expectedTicketDto = existingTickets[i];
                var actualTicketDto = result.Data[i];

                Assert.Equal(expectedTicketDto.Id, actualTicketDto.Id);
                Assert.Equal(expectedTicketDto.Price, actualTicketDto.Price);
            }
            ticketRepository.Verify(x => x.ListAsync(It.IsAny<Ardalis.Specification.ISpecification<Ticket>>(), default), Times.Exactly(listAsyncTimesCalled));
        }
    }
}
