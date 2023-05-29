﻿namespace Application.Handlers.Statistics
{
    public class GetRangeDateTicketStatisticsQueryHandler : IRequestHandler<GetRangeDateTicketStatisticsQuery, Result<List<TicketDto>>>
    {
        private readonly IRepository<Ticket> _repository;
        public GetRangeDateTicketStatisticsQueryHandler(IRepository<Ticket> repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<TicketDto>>> Handle(GetRangeDateTicketStatisticsQuery request, CancellationToken cancellationToken)
        {
            var getRangeDateTicket = await _repository.ListAsync(new GetRangeDateTicketStatisticsSpecification(request.StartDate, request.EndDate));

            var newTicketsPerDrawDto = new List<TicketDto>();
            foreach (var ticket in getRangeDateTicket)
            {
                var newColumnsDto = new List<ColumnDto>();
                foreach (var column in ticket.Columns)
                {
                    newColumnsDto.Add(ColumnDto.Create(
                        id: column.Id,
                        selectionNumbers: column.SelectionNumbers,
                        selectionGame: column.SelectionGame,
                        multiplier: column.Multiplier,
                        price: column.Price,
                        kinoBonus: column.KinoBonus,
                        selectionRandom: column.SelectionRandom,
                        cancel: column.Cancel,
                        profit: column.Profit.Profit,
                        success: column.Success.Success,
                        createdOn: column.CreatedOn,
                        lastModifiedOn: column.LastModifiedOn
                        ));
                }
                newTicketsPerDrawDto.Add(TicketDto.Create(
                    id: ticket.Id,
                    price: ticket.Price,
                    profit: ticket.Profit.Profit,
                    drawId: ticket.DrawId,
                    columns: newColumnsDto,
                    createdOn: ticket.CreatedOn,
                    lastModifiedOn: ticket.LastModifiedOn
                    ));
            }
            return Result<List<TicketDto>>.Success(newTicketsPerDrawDto);
        }
    }
}
