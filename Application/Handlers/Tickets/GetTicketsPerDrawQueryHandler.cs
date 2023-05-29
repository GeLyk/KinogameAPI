namespace Application.Handlers.Tickets
{
    public class GetTicketsPerDrawQueryHandler : IRequestHandler<GetTicketsPerDrawQuery, Result<List<TicketDto>>>
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Draw> _drawRepository;
        public GetTicketsPerDrawQueryHandler(IRepository<Ticket> ticketRepository, IRepository<Draw> drawRepository)
        {
            _ticketRepository = ticketRepository;
            _drawRepository = drawRepository;
        }
        public async Task<Result<List<TicketDto>>> Handle(GetTicketsPerDrawQuery request, CancellationToken cancellationToken)
        {
            var ticketsPerDraw = await _ticketRepository.ListAsync(new GetTicketsPerDrawSpecification());
            var draw = await _drawRepository.FirstOrDefaultAsync(new GetLatestDrawSpecification());

            ticketsPerDraw = await TicketSuccessCalculation(ticketsPerDraw, draw);

            var newTicketsPerDrawDto = new List<TicketDto>();
            foreach (var ticket in ticketsPerDraw)
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
                        lastModifiedOn: ticket.LastModifiedOn
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
        public async Task<List<Ticket>> TicketSuccessCalculation(List<Ticket> ticketsPerDraw, Draw draw)
        {
            var listOfAllDrawNumbers = new List<int>();
            var updatedTicketsPerDraw = new List<Ticket>();

            listOfAllDrawNumbers.AddRange(draw.DrawNumbers.Split('/').Select(Int32.Parse));
            foreach (var ticket in ticketsPerDraw)
            {
                if (ticket.DrawId == 0)
                {
                    foreach (var column in ticket.Columns)
                    {
                        foreach (var number in ConvertSelectionNumbersPerColumnToList(column.SelectionNumbers))
                        {
                            if (listOfAllDrawNumbers.Contains(number))
                            {
                                column.UpdateColumnSuccess(column.Success.Success + 1);
                            }
                        }

                        var columnProfit = ColumnProfitCalculation(column, listOfAllDrawNumbers);

                        column.UpdateColumnProfit(columnProfit);
                        column.UpdateColumnProfit(column.Profit.Profit * column.Multiplier);
                        ticket.UpdateTicketProfit(ticket.Profit.Profit + column.Profit.Profit);
                    }
                    ticket.UpdateDrawId(draw.Id);

                    await _ticketRepository.UpdateAsync(ticket);
                }
                updatedTicketsPerDraw.Add(ticket);
            }
            return updatedTicketsPerDraw;
        }
        public double ColumnProfitCalculation(Column column, List<int> numbersOfDraw)
        {
            if (numbersOfDraw[numbersOfDraw.Count - 1] > 0)
            {
                var numbersPerColumn = new List<int>();

                numbersPerColumn.AddRange(column.SelectionNumbers.Split(',').Select(Int32.Parse).ToList());
                numbersPerColumn.Sort();

                switch (numbersPerColumn.Count)
                {
                    case (int)Category.firstCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.firstCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2000000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1000000);
                                }
                                break;
                            case (int)SuccessfulNumbers.secondCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(75000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(25000);
                                }
                                break;
                            case (int)SuccessfulNumbers.thirdCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(5500);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2500);
                                }
                                break;
                            case (int)SuccessfulNumbers.fourthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2200);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1000);
                                }
                                break;
                            case (int)SuccessfulNumbers.fifthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(350);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(150);
                                }
                                break;
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(50);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(25);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(10);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(5);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3.5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2.5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.thirteenthCategory:
                                if (column.KinoBonus)
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                break;
                        }
                        break;
                    case (int)Category.secondCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.secondCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(1200000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(500000);
                                }
                                break;
                            case (int)SuccessfulNumbers.thirdCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(31000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(15000);
                                }
                                break;
                            case (int)SuccessfulNumbers.fourthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3500);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1500);
                                }
                                break;
                            case (int)SuccessfulNumbers.fifthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(550);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(250);
                                }
                                break;
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(105);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(50);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(25);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(10);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(6);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2.5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.thirteenthCategory:
                                if (column.KinoBonus)
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                break;
                        }
                        break;
                    case (int)Category.thirdCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.thirdCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(250000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(100000);
                                }
                                break;
                            case (int)SuccessfulNumbers.fourthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(25000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(10000);
                                }
                                break;
                            case (int)SuccessfulNumbers.fifthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(1500);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(500);
                                }
                                break;
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(180);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(80);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(60);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(20);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(7);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2.5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.thirteenthCategory:
                                if (column.KinoBonus)
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                break;
                        }
                        break;
                    case (int)Category.fourthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.fourthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(100000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(40000);
                                }
                                break;
                            case (int)SuccessfulNumbers.fifthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(10000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(4000);
                                }
                                break;
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(500);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(200);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(70);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(25);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(15);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(5);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(6);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.fifthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.fifthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(40000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(15000);
                                }
                                break;
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1000);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(200);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(50);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(30);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(10);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(7);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.sixthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.sixthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(15000);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(5000);
                                }
                                break;
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(400);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(100);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(80);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(20);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(13);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(8);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.seventhCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.seventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(4100);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1600);
                                }
                                break;
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(300);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(50);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(27);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(7);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(9);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.eighthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.eighthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(1350);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(450);
                                }
                                break;
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(90);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(20);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(17);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(3);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.ninthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.ninthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(600);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(100);
                                }
                                break;
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(24);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(4);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(8);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.tenthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.tenthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(175);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(25);
                                }
                                break;
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(18);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2.5);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(8);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(0);
                                }
                                break;
                        }
                        break;
                    case (int)Category.eleventhCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.eleventhCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(70);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(5);
                                }
                                break;
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(16);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(1);
                                }
                                break;
                        }
                        break;
                    case (int)Category.twelfthCategory:
                        switch (column.Success.Success)
                        {
                            case (int)SuccessfulNumbers.twelfthCategory:
                                if (column.KinoBonus && numbersPerColumn.Contains(numbersOfDraw[numbersOfDraw.Count - 1]))
                                {
                                    column.UpdateColumnProfit(52.5);
                                }
                                else
                                {
                                    column.UpdateColumnProfit(2.5);
                                }
                                break;
                        }
                        break;
                }
            }
            return column.Profit.Profit; 
        }
        public List<int> ConvertSelectionNumbersPerColumnToList(string selectionNumbers)
        {
            return selectionNumbers.Split(',').Select(Int32.Parse).ToList();
        }
    }
}
