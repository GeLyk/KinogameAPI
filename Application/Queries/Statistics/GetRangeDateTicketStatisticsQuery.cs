namespace Application.Queries.Statistics
{
    public record GetRangeDateTicketStatisticsQuery : IRequest<Result<List<TicketDto>>>
    {
        public GetRangeDateTicketStatisticsQuery(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
