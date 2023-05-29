namespace Application.Queries
{
    public record GetNumbersOfDailyDrawsForStatisticsQuery() : IRequest<Result<List<int>>> { }
}
