namespace Application.Queries.Tickets
{
    public record GetClippingsPerDrawQuery : IRequest<Result<List<TicketDto>>> { }
}
