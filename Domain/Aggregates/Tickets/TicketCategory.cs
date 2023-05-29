namespace Domain.Aggregates.Tickets
{
    public sealed class TicketCategory : SmartEnum<TicketCategory>
    {
        public static readonly TicketCategory ThirteenthCategory = new TicketCategory(nameof(ThirteenthCategory), 0);
        public static readonly TicketCategory TwelfthCategory = new TicketCategory(nameof(TwelfthCategory), 1);
        public static readonly TicketCategory EleventhCategory = new TicketCategory(nameof(EleventhCategory), 2);
        public static readonly TicketCategory TenthCategory = new TicketCategory(nameof(TenthCategory), 3);
        public static readonly TicketCategory NinthCategory = new TicketCategory(nameof(NinthCategory), 4);
        public static readonly TicketCategory EighthCategory = new TicketCategory(nameof(EighthCategory), 5);
        public static readonly TicketCategory SeventhCategory = new TicketCategory(nameof(SeventhCategory), 6);
        public static readonly TicketCategory SixthCategory = new TicketCategory(nameof(SixthCategory), 7);
        public static readonly TicketCategory FifthCategory = new TicketCategory(nameof(FifthCategory), 8);
        public static readonly TicketCategory FourthCategory = new TicketCategory(nameof(FourthCategory), 9);
        public static readonly TicketCategory ThirdCategory = new TicketCategory(nameof(ThirdCategory), 10);
        public static readonly TicketCategory SecondCategory = new TicketCategory(nameof(SecondCategory), 11);
        public static readonly TicketCategory FirstCategory = new TicketCategory(nameof(FirstCategory), 12);
        private TicketCategory() : base(default, default) { }
        public TicketCategory(string name, int value) : base(name, value) { }
    }
}
