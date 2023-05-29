namespace Application.Queries.Statistics
{
    public record GetProfitStatisticsQuery() : IRequest<Result<List<object>>> { }
}
