namespace Domain.Tests.Seeds.Ticket
{
    public class TicketValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 2, 2, 2 };
        }
    }
    public class UpdateTicketDrawIdValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2 };
        }
    }
    public class UpdateTicketProfitValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2.234 };
        }
    }
}
