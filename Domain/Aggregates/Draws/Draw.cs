namespace Domain.Aggregates.Draws
{
    public class Draw : BaseEntity<int>, IAggregateRoot
    {
        public int Id { get; private set; }
        public string DrawNumbers { get; private set; }
        private Draw() { }
        public Draw(
            int id,
            string drawNumbers
            )
        {
            Id = Guard.Against.Null(id, nameof(id));
            DrawNumbers = drawNumbers;
        }
        public static string GenerateNumbersOfDraw()
        {
            var listOfDrawNumbers = GeneratorRandomNumbers.GeneratorRandomNumbers.GenerateNumbers();
            string drawNumbers = String.Join("/", listOfDrawNumbers.Select(p => p.ToString()).ToList());
            return drawNumbers;
        }
        public static Draw Create()
        {
            return new Draw
            {
                DrawNumbers = GenerateNumbersOfDraw()
            };
        }
    }
}
