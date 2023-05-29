namespace Application.Queries
{
    public record GetAllDrawsQuery() : IRequest<List<Draw>> { }
}
