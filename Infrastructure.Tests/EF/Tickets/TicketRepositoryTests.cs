namespace Infrastructure.Tests.EF.Tickets
{
    public class TicketRepositoryTests : RepositoryTests
    {
        [Fact]
        public async Task Create_Ticket_Success_Test()
        {
            // Arrange
            var ticket = new TicketBuilder()
                .Build();

            var ticketsRepo = new EfRepository<Ticket>(_dbContext);

            // Act
            await ticketsRepo.AddAsync(ticket);
            var tickets = await ticketsRepo.ListAsync();

            // Assert
            var createdTicket = await _dbContext.Tickets.FindAsync(ticket.Id);
            Assert.NotNull(createdTicket);
            Assert.Equal(ticket, createdTicket);
            Assert.Equal(1, tickets.Count);
        }

        [Theory]
        [ClassData(typeof(UpdateTicketValidSeed))]
        public async Task Update_Ticket_Success_Test(
            double profit,
            int drawId,
            double updatedProfit,
            int updatedDrawId
            )
        {
            // Arrange
            var ticket = new TicketBuilder()
                .WithProfit(profit)
                .WithDrawId(drawId)
                .Build();

            var ticketsRepo = new EfRepository<Ticket>(_dbContext);
            await ticketsRepo.AddAsync(ticket);

            // Act
            ticket.UpdateDrawId(updatedDrawId);
            ticket.UpdateTicketProfit(updatedProfit);

            await ticketsRepo.UpdateAsync(ticket);

            // Assert
            var updatedTicket = await _dbContext.Tickets.FindAsync(ticket.Id);
            Assert.Equal(updatedDrawId, updatedTicket.DrawId);
            Assert.Equal(updatedProfit, updatedTicket.Profit.Profit);
        }
    }
}
