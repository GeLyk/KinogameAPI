using Infrastructure.Tests.Seeds.Draw;

namespace Infrastructure.Tests.EF.Draws
{
    public class DrawRepositoryTests : RepositoryTests
    {
        public DrawRepositoryTests()
        {
            
        }

        [Theory]
        [ClassData(typeof(CreateDrawValidSeed))]
        public async Task Create_Draw_Success_Test()
        {

        }
    }
}
