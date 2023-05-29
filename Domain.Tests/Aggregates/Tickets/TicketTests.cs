namespace Domain.Tests.Aggregates.Tickets
{
    public class TicketTests
    {
        [Theory]
        [ClassData(typeof(TicketValidSeed))]
        public void Create_ValidParameters(int id, double price, double profit, int drawId)
        {
            var ticket = new TicketBuilder()
                .WithId(id)
                .WithPrice(price)
                .WithProfit(profit)
                .WithDrawId(drawId)
                .Build();

            Assert.NotNull(ticket);
            Assert.Equal(id, ticket.Id);
            Assert.Equal(price, ticket.Price);
            Assert.Equal(profit, ticket.Profit.Profit);
            Assert.Equal(drawId, ticket.DrawId);
        }

        [Theory]
        [ClassData(typeof(UpdateTicketDrawIdValidSeed))]
        public void UpdateDrawId_ValidParameters(int drawId)
        {
            var ticket = new TicketBuilder()
                .WithDrawId(drawId)
                .Build();

            Assert.NotNull(ticket);
            Assert.Equal(drawId, ticket.DrawId);
        }

        [Theory]
        [ClassData(typeof(UpdateTicketProfitValidSeed))]
        public void UpdateTicketProfit_ValidParameters(double profit)
        {
            var ticket = new TicketBuilder()
                .WithProfit(profit)
                .Build();

            Assert.NotNull(ticket);
            Assert.Equal(profit, ticket.Profit.Profit);
        }
    }
}
