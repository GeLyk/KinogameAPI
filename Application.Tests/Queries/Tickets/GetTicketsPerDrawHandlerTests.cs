namespace Application.Tests.Queries.Tickets
{
    public class GetTicketsPerDrawHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetTicketsPerDrawQueryValidSeed))]
        public async Task GetTicketsPerDrawHandler_ValidParameters(
            int id,
            double price,
            double profit,
            int drawId,
            int updateAsyncTimesCalled
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
            var drawRepository = new Mock<IRepository<Draw>>();
            var query = new GetTicketsPerDrawQuery();
            var handler = new GetTicketsPerDrawQueryHandler(ticketRepository.Object, drawRepository.Object);
            var existingTickets = new List<Ticket>() { existingTicket };

            ticketRepository.Setup(x => x.ListAsync(It.IsAny<GetTicketsPerDrawSpecification>(), default)).ReturnsAsync(existingTickets);

            drawRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetLatestDrawSpecification>(), default)).ReturnsAsync(existingDraw);

            var queryHandlerType = typeof(GetTicketsPerDrawQueryHandler);
            var queryHandlerConstructor = queryHandlerType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(IRepository<Ticket>), typeof(IRepository<Draw>) },
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
            ticketRepository.Verify(x => x.UpdateAsync(It.IsAny<Ticket>(), default), Times.Exactly(updateAsyncTimesCalled));
        }
    }
}
