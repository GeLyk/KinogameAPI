namespace Application.Tests.Seeds.Ticket
{
    public class CreateTicketCommandHandlerValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            var columnsDto = new List<ColumnDto>();
            columnsDto.Add(ColumnDto.Create(
                id: 2,
                selectionNumbers: "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20",
                selectionGame: 2,
                multiplier: 2,
                price: 2,
                kinoBonus: true,
                selectionRandom: true,
                cancel: true,
                profit: 2,
                success: 2,
                createdOn: DateTime.Now,
                lastModifiedOn: DateTime.Now
                ));

            yield return new object[] {
                TicketDto.Create(2, 2, 2, 2, columnsDto, DateTime.Now, DateTime.Now),
                2, 2, 2, 2, 1 };
        }
    }
    public class GetTicketsPerDrawQueryValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                2, 2, 2, 0, 1 };
        }
    }
    public class GetClippingsPerDrawValidSeed : Seed, IEnumerable<object[]>
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                2, 2, 2, 0, 1 };
        }
    }
}
