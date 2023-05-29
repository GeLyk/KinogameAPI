namespace Application.Handlers.Draws
{
    public class GetDrawQueryHandler : IRequestHandler<GetDrawQuery, Result<Draw>>
    {
        private readonly IRepository<Draw> _repository;
        public GetDrawQueryHandler(IRepository<Draw> drawRepository)
        {
            _repository = drawRepository;
        }
        public async Task<Result<Draw>> Handle(GetDrawQuery request, CancellationToken cancellationToken)
        {
            var newDraw = Draw.Create();
            await _repository.AddAsync(newDraw);

            var existedDraw = await _repository.FirstOrDefaultAsync(new GetLatestDrawSpecification());

            return Result<Draw>.Success(existedDraw);
        }
    }
}
