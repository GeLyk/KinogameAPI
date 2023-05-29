namespace Application.Tests.Queries.Draws
{
    public class GetDrawHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetDrawQueryHandlerValidSeed))]
        public async Task GetDrawQueryHandlerTests_ValidParameters(int addAsyncTimesCalled, int firstOrDefaultAsyncTimesCalled)
        {
            // Arrange
            var expectedDraw = new DrawBuilder()
                .Build();

            var mockRepository = new Mock<IRepository<Draw>>();
            mockRepository.Setup(x => x.AddAsync(It.IsAny<Draw>(), default)).Verifiable();
            mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Draw>>(), default)).ReturnsAsync(expectedDraw);

            var handler = new GetDrawQueryHandler(mockRepository.Object);

            // Act
            var result = await handler.Handle(new GetDrawQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Draw>(), default), Times.Exactly(addAsyncTimesCalled));
            mockRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetLatestDrawSpecification>(), default), Times.Exactly(firstOrDefaultAsyncTimesCalled));
        }
    }
}
