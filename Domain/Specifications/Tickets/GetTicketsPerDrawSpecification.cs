namespace Domain.Specifications.Tickets
{
    public class GetTicketsPerDrawSpecification : Specification<Ticket>
    {
        public GetTicketsPerDrawSpecification()
        {
            var moment = DateTime.Now.ToLocalTime();
            var temp = moment.Minute / 5;
            var start = temp * 5;
            var stop = start + 5;

            Query
                .Where(ticket => ticket.CreatedOn.Year == moment.Year
            && ticket.CreatedOn.Month == moment.Month
            && ticket.CreatedOn.Day == moment.Day
            && ticket.CreatedOn.Hour == moment.Hour
            && ticket.CreatedOn.Minute >= start
            && ticket.CreatedOn.Minute <= stop)
                .Include(c => c.Columns);
        }
    }
}
