namespace Application.DTO.TicketDto
{
    public class TicketDto : BaseEntity<int>, IAggregateRoot
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Profit { get; set; }
        public int DrawId { get; set; }
        public List<ColumnDto.ColumnDto> Columns { get; set; }
        private TicketDto() { }
        public TicketDto(
            int id,
            double price,
            double profit,
            int drawId,
            List<ColumnDto.ColumnDto> columns,
            DateTime createdOn,
            DateTime? lastModifiedOn
            )
        {
            Id = Guard.Against.Null(id, nameof(id));
            Price = price;
            Profit = profit;
            DrawId = drawId;
            Columns = columns;
            CreatedOn = createdOn;
            LastModifiedOn = lastModifiedOn;
        }

        public static TicketDto Create(
            int id,
            double price,
            double profit,
            int drawId,
            List<ColumnDto.ColumnDto> columns,
            DateTime createdOn,
            DateTime? lastModifiedOn
            )
        {
            var ticket = new TicketDto(
                id: id,
                price: price,
                profit: profit,
                drawId: drawId,
                columns: columns,
                createdOn: createdOn,
                lastModifiedOn: lastModifiedOn  
                );

            return ticket;
        }
    }
}
