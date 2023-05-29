namespace Application.Handlers.Tickets
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Result<Ticket>>
    {
        private readonly IRepository<Ticket> _repository;
        public CreateTicketCommandHandler(IRepository<Ticket> repository)
        {
            _repository = repository;
        }
        public async Task<Result<Ticket>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var newColumns = new List<Column>();

            foreach (var column in request.Ticket.Columns)
            {
                newColumns.Add(
                        Column.Create(
                        id: column.Id,
                        selectionNumbers: column.SelectionNumbers,
                        selectionGame: column.SelectionGame,
                        multiplier: column.Multiplier,
                        price: column.Price,
                        kinoBonus: column.KinoBonus,
                        selectionRandom: column.SelectionRandom,
                        cancel: column.Cancel,
                        profit: column.Profit,
                        success: column.Success
                        ));
            }

            var ticket = Ticket.Create(
                id: request.Ticket.Id,
                price: request.Ticket.Price,
                profit: request.Ticket.Profit,
                drawId: request.Ticket.DrawId,
                columns: newColumns
                );

            if ( ticket != null )
                await _repository.AddAsync(ticket);

            return Result<Ticket>.Success(ticket);
        }
    }
}
