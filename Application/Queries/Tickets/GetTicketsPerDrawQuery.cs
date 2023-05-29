namespace Application.Queries.Tickets
{
    public record GetTicketsPerDrawQuery() : IRequest<Result<List<TicketDto>>> { }
}
