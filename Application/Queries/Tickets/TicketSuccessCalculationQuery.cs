namespace Application.Queries.Tickets
{
    public class TicketSuccessCalculationQuery : IRequest
    {
        public TicketSuccessCalculationQuery(List<Ticket> ticketsPerDraw, Draw latestDraw) 
        {
            TicketsPerDraw = ticketsPerDraw;
            LatestDraw = latestDraw;
        }
        public List<Ticket> TicketsPerDraw { get; set; }
        public Draw LatestDraw { get; set; }
    }
}
