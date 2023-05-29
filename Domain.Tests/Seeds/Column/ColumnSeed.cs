namespace Domain.Tests.Seeds.Column
{
    public class ColumnValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 
                2,
                "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20",
                2,
                2,
                2.1234,
                true,
                true,
                true,
                2.3423,
                2 
            };
        }
    }
    public class UpdateColumnProfitValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2.123 };
        }
    }
    public class UpdateColumnSuccessValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2 };
        }
    }
}
