namespace KinoApiGeorge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrawController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DrawController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        // GET: api/Draw/CreateDraw
        [HttpGet, Route("CreateDraw")]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GetDrawQuery(), default);
            return Ok(query);
        }

        [HttpGet, Route("GetNumbersOfDailyDrawsForStatistics")]
        public async Task<IActionResult> StatisticsOfDraws()
        {
            var query = await _mediator.Send(new GetNumbersOfDailyDrawsForStatisticsQuery(), default);

            return Ok(query);
        }
    }
}