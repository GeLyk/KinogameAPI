namespace Infrastructure.Tests.Seeds.Column
{
    public class UpdateColumnValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                2.3423,
                2,
                4.1234,
                4
            };
        }
    }
}
