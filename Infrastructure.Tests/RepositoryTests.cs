namespace Infrastructure.Tests
{
    public abstract class RepositoryTests
    {
        protected readonly AppDbContext _dbContext;
        protected readonly Mock<IMediator> _mediator;
        public RepositoryTests()
        {
            _mediator = new Mock<IMediator>();

            var dbName = $"KinoGameDb_{DateTime.Now.ToLocalTime()}";
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            _dbContext = new AppDbContext(dbContextOptions, _mediator.Object);
        }
    }
}
