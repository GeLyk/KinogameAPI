namespace Application.Commands
{
    public record CreateTicketCommand : IRequest<Result<Ticket>>
    {
        public TicketDto Ticket { get; set; }
        public CreateTicketCommand(TicketDto ticket)
        {
            Ticket = ticket;
        }
    }
}
