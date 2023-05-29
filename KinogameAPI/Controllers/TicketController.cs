namespace KinoApiGeorge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Ticket/CreateTicket
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket(TicketDto ticket)
        {
            var query = await _mediator.Send(new CreateTicketCommand(ticket), default);
            return Ok(query);
        }

        //GET: api/Ticket/GetClippingsPerDraw
        [HttpGet, Route("GetClippingsPerDraw")]
        public async Task<IActionResult> GetClippingsPerDraw()
        {
            var query = await _mediator.Send(new GetClippingsPerDrawQuery(), default);
            return Ok(query);
        }

        //GET: api/Ticket/GetTicketsPerDraw
        [HttpGet, Route("GetTicketsPerDraw")]
        public async Task<IActionResult> GetTicketsPerDraw()
        {
            var query = await _mediator.Send(new GetTicketsPerDrawQuery(), default);

            return Ok(query);
        }

        //GET: api/Ticket/GetRangeDateTicketStatisticsQuery
        [HttpGet, Route("GetRangeDateTicketStatisticsQuery")]
        public async Task<IActionResult> GetStatisticsCalculation(string startDate, string endDate)
        {
            var query = await _mediator.Send(new GetRangeDateTicketStatisticsQuery(startDate, endDate), default);
            return Ok(query);
        }

        //GET: api/Ticket/GetAnnualStatisticsCalculation
        [HttpGet, Route("GetAnnualStatisticsCalculation")]
        public async Task<IActionResult> GetAnnualStatisticsCalculation()
        {
            var query = await _mediator.Send(new GetProfitStatisticsQuery(), default);
            return Ok(query);
        }
    }
}
