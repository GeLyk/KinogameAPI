namespace Application.Handlers.Statistics
{
    public class GetProfitStatisticsQueryHandler : IRequestHandler<GetProfitStatisticsQuery, Result<List<object>>>
    {
        private readonly IRepository<Ticket> _repository;
        public GetProfitStatisticsQueryHandler(IRepository<Ticket> repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<object>>> Handle(GetProfitStatisticsQuery request, CancellationToken cancellationToken)
        {
            var ticketsPerYear = await _repository.ListAsync(new GetTicketsPerYearSpecification());
            return Result<List<object>>.Success(CategorizationByMonth(ticketsPerYear));
        }
        public List<Object> CategorizationByMonth(List<Ticket> tickets)
        {
            var listOfPercentages = new List<Object>();
            var dictionary = new Dictionary<int, double>();

            var groupTickets = tickets.GroupBy(x => new { Month = x.CreatedOn.Month, Year = x.CreatedOn.Year, Price = x.Price })
                                      .ToDictionary(g => g.Key, g => g.Count());

            var keyList = groupTickets.Keys.ToList();

            foreach (var key in keyList)
            {
                double monthProfit = tickets.Where(x => x.CreatedOn.Month == key.Month).Sum(x => x.Profit.Profit);
                double monthPrice = tickets.Where(x => x.CreatedOn.Month == key.Month).Sum(x => x.Price);

                double percentage = CalculationPercentage(monthProfit, monthPrice);
                var percentageByMonth = new { key.Month, percentage, monthProfit, monthPrice };
                listOfPercentages.Add(percentageByMonth);
            }

            return listOfPercentages;
        }
        public double CalculationPercentage(double totalProfit, double totalPrice)
        {
            var annualProfit = totalProfit - totalPrice;
            var annualProfitPercentage =  (annualProfit / totalPrice) * 100;
            return Math.Round(annualProfitPercentage, 1);
        }
    }
}
