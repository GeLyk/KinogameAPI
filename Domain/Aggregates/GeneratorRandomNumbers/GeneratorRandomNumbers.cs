namespace Domain.Aggregates.GeneratorRandomNumbers
{
    public class GeneratorRandomNumbers
    {
        public const int listSize = 20;
        public const int lowerValueNumbersOfDraw = 1;
        public const int upperValueNumbersOfDraw = 81;
        public const int lowerValueSelectionKinoBonus = 1;
        public const int upperValueSelectionKinoBonus = 52;
        public const int limitNumberOfSelectionKinoBonus = 25;
        public const int lowerIDdrawNumber = 1000;
        public const int upperIDdrawNumber = 10000;
        public const int lowerIDkinoTicket = 100000;
        public const int upperIDkinoTicket = 1000000;

        public static List<int> GenerateNumbers()
        {
            Random random = new Random();
            List<int> ListRandomNumbers = new List<int>();

            while (ListRandomNumbers.Count < listSize)
            {
                int randomNumber = random.Next(lowerValueNumbersOfDraw, upperValueNumbersOfDraw);
                if (!ListRandomNumbers.Contains(randomNumber))
                    ListRandomNumbers.Add(randomNumber);
            }

            return ListRandomNumbers;
        }
        public bool SelectionKinoBonus(Random random)
        {
            bool KinoBonus;
            int randomNumber = random.Next(lowerValueSelectionKinoBonus, upperValueSelectionKinoBonus);
            KinoBonus = randomNumber > limitNumberOfSelectionKinoBonus ? true : false;
            return KinoBonus;
        }
        public int Create_ID_Numbers_Of_Draw(Random random)
        {
            int randomNumber = random.Next(lowerIDdrawNumber, upperIDdrawNumber);
            return randomNumber;
        }
        public int Create_ID_Numbers_Of_KinoTIcket(Random random)
        {
            int randomNumber = random.Next(lowerIDkinoTicket, upperIDkinoTicket);
            return randomNumber;
        }
    }
}
