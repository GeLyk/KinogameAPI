namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected IActionResult OkOrBadRequest<U>(Result<U> businessResult) => !businessResult.Succeeded ? BadRequestSerialized(businessResult)
            : Ok(businessResult.Data);
        private IActionResult BadRequestSerialized(Result businessResult) => BadRequest(businessResult.Messages);

        protected IMediator _mediator;
        //protected readonly ILogger<T> _logger;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
            //_logger = logger;
        }
    }
}
