namespace Application.Tests.Seeds.Column
{
    public class GetProfitStatisticsQueryHandlerValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 2, 2, 2, 1 };
        }
    }
    public class GetRangeDateTicketStatisticsQueryHandlerValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 2, 2, 2, DateTime.Now.ToString(), DateTime.Now.ToString(), 1 };
        }
    }
}
