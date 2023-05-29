namespace Application.Tests.Queries.Tickets
{
    public class GetClippingsPerDrawHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetClippingsPerDrawValidSeed))]
        public async Task GetClippingsPerDrawHandler_ValidParameters(
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

            var existingDraw = new DrawBuilder()
                .Build();

            // Arrange
            var ticketRepository = new Mock<IRepository<Ticket>>();
            var query = new GetClippingsPerDrawQuery();
            var handler = new GetClippingsPerDrawQueryHandler(ticketRepository.Object);
            var existingTickets = new List<Ticket>() { existingTicket };

            ticketRepository.Setup(x => x.ListAsync(It.IsAny<GetTicketsPerDrawSpecification>(), default)).ReturnsAsync(existingTickets);

            var queryHandlerType = typeof(GetClippingsPerDrawQueryHandler);
            var queryHandlerConstructor = queryHandlerType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(IRepository<Ticket>) },
                null
            );

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.IsType<Result<List<TicketDto>>>(result);
            var ticketDto = result.Data.FirstOrDefault();
            Assert.Equal(existingTickets.FirstOrDefault()?.Id, ticketDto.Id);
            Assert.Equal(existingTickets.FirstOrDefault()?.Price, ticketDto.Price);
            Assert.Equal(existingTickets.FirstOrDefault()?.Profit.Profit, ticketDto.Profit);
            Assert.Equal(existingTickets.FirstOrDefault()?.DrawId, ticketDto.DrawId);
            Assert.Equal(existingTickets.FirstOrDefault()?.CreatedOn, ticketDto.CreatedOn);
            Assert.Equal(existingTickets.FirstOrDefault()?.LastModifiedOn, ticketDto.LastModifiedOn);
            ticketRepository.Verify(x => x.ListAsync(It.IsAny<Ardalis.Specification.ISpecification<Ticket>>(), default), Times.Exactly(listAsyncTimesCalled));
        }
    }
}
