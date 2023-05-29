namespace Application.Tests.Queries.Draws
{
    public class GetNumbersOfDailyDrawsForStatisticsQueryHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetNumbersOfDailyDrawsForStatisticsQueryHandlerValidSeed))]
        public async Task GetNumbersOfDailyDrawsForStatisticsQueryHandler_ValidParameters(int listAsyncTimesCalled)
        {
            // Arrange
            var expectedDraw = new DrawBuilder()
                .Build();

            var expectedDraws = new List<Draw>() { expectedDraw };
            var mockRepository = new Mock<IRepository<Draw>>();
          
            mockRepository.Setup(x => x.ListAsync(It.IsAny<GetDailyDrawsSpecification>(), default)).ReturnsAsync(expectedDraws);
            var handler = new GetNumbersOfDailyDrawsForStatisticsQueryHandler(mockRepository.Object);

            // Act
            var result = await handler.Handle(new GetNumbersOfDailyDrawsForStatisticsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            mockRepository.Verify(x => x.ListAsync(It.IsAny<GetDailyDrawsSpecification>(), default), Times.Exactly(listAsyncTimesCalled));
        }
    }
}
