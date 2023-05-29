namespace Domain.Specifications.Statistics
{
    public class GetTicketsPerYearSpecification : Specification<Ticket>
    {
        public GetTicketsPerYearSpecification()
        {
            Query
                .Where(ticket => ticket.CreatedOn.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedOn.Month);
        }
    }
}
