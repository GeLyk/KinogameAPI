namespace Application.Handlers.Draws
{
    public class GetAllDrawQueryHandler : IRequestHandler<GetAllDrawsQuery, List<Draw>>
    {
        private readonly IRepository<Draw> _repository;
        public GetAllDrawQueryHandler(IRepository<Draw> repository)
        {
            _repository = repository;
        }
        public async Task<List<Draw>> Handle(GetAllDrawsQuery request, CancellationToken cancellationToken)
        {
            var draws = await _repository.ListAsync(cancellationToken).ConfigureAwait(false);

            return draws;
        }
    }
}
