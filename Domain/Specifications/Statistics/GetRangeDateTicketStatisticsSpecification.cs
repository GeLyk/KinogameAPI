namespace Domain.Specifications.Statistics
{
    public class GetRangeDateTicketStatisticsSpecification : Specification<Ticket>
    {
        public GetRangeDateTicketStatisticsSpecification(string startDate, string endDate)
        {
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            var test = start.Date;
            var test2 = start.DayOfYear;
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;

            Query
                .Where(ticket => ticket.CreatedOn.DayOfYear >= start.DayOfYear && ticket.CreatedOn.DayOfYear <= end.DayOfYear)
                .Include(ticket => ticket.Columns);
        }
    }
}


