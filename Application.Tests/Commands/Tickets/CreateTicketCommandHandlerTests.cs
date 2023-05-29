namespace Application.Tests.Commands.Tickets
{
    public class CreateTicketCommandHandlerTests
    {
        [Theory]
        [ClassData(typeof(CreateTicketCommandHandlerValidSeed))]
        private async Task CreateTicketCommandHandler_ValidParameters(
            TicketDto ticketDto,
            int id,
            double price,
            double profit,
            int drawId,
            int addAsyncTimesCalled
            )
        {
            var existingTicket = new TicketBuilder()
                .WithId(id)
                .WithPrice(price)
                .WithProfit(profit)
                .WithDrawId(drawId)
                .Build();

            var existingTickets = new List<Ticket>() { existingTicket };
            var mockRepo = new MockRepository<Ticket>(existingTickets);
            var mockCommand = new MockCommand
                <Ticket, CreateTicketCommand, CreateTicketCommandHandler, Result<Ticket>, TicketDto>
                (ticketDto, mockRepo._repository);

            var res =  await mockCommand.Execute();

            Assert.True(res);
            Assert.True(mockRepo.Verify(
                addAsyncTimesCalled: addAsyncTimesCalled)
                );
        }
    }
}
