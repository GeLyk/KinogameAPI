namespace Infrastructure.Tests.Seeds.Ticket
{
    public class UpdateTicketValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 3.12345, 3, 4.12345, 4 };
        }
    }
}
