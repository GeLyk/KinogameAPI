namespace Application.Tests.Seeds.Draw
{
    public class GetDrawQueryHandlerValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 1 };
        }
    }
    public class GetNumbersOfDailyDrawsForStatisticsQueryHandlerValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1 };
        }
    }
}
