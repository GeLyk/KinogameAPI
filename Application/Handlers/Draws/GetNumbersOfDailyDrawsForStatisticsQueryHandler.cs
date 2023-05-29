namespace Application.Handlers.Draws
{
    public class GetNumbersOfDailyDrawsForStatisticsQueryHandler : IRequestHandler<GetNumbersOfDailyDrawsForStatisticsQuery, Result<List<int>>>
    {
        private readonly IRepository<Draw> _repository;
        public GetNumbersOfDailyDrawsForStatisticsQueryHandler(IRepository<Draw> repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<int>>> Handle(GetNumbersOfDailyDrawsForStatisticsQuery request, CancellationToken cancellationToken)
        {
            var listOfAllDrawNumbers = new List<int>();

            var dailyDraws = await _repository.ListAsync(new GetDailyDrawsSpecification());

            dailyDraws.ForEach(draw => listOfAllDrawNumbers.AddRange(draw.DrawNumbers.Split('/').Select(Int32.Parse).ToList()));
            listOfAllDrawNumbers.Sort();

            return Result<List<int>>.Success(listOfAllDrawNumbers);
        }
    }
}
