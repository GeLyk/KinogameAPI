namespace TestDefinitions
{
    public abstract class Seed : IEnumerable<object[]>
    {
        public abstract IEnumerator<object[]> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }

    public class MockRepository<T> where T : class, IAggregateRoot
    {
        public readonly IRepository<T> _repository;
        private readonly Mock<IRepository<T>> _mock;

        public MockRepository(List<T> existingValues)
        {
            _mock = new Mock<IRepository<T>>();

            _mock.Setup(x => x.ListAsync(It.IsAny<Ardalis.Specification.ISpecification<T>>(), default))
            .ReturnsAsync(existingValues);

            _mock.Setup(x => x.AddRangeAsync(It.IsAny<List<T>>(), default)).Verifiable();

            _mock.Setup(x => x.UpdateRangeAsync(It.IsAny<List<T>>(), default)).Verifiable();

            _mock.Setup(x => x.AddAsync(It.IsAny<T>(), default)).Verifiable();

            _mock.Setup(x => x.UpdateAsync(It.IsAny<T>(), default)).Verifiable();


            _mock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<T>>(), default))
             .ReturnsAsync((ISpecification<T> spec, CancellationToken _) =>
             {
                 var queryableExistingValues = existingValues.AsQueryable();

                 foreach (var expression in spec.WhereExpressions)
                 {
                     queryableExistingValues = queryableExistingValues.Where(expression.Filter);
                 }

                 return queryableExistingValues.FirstOrDefault();
             });

            _repository = _mock.Object;
        }
        public bool Verify(int listAsyncTimesCalled = 0,
            int addRangeAsyncTimesCalled = 0,
            int updateRangeAsyncTimesCalled = 0,
            int addAsyncTimesCalled = 0,
            int updateAsyncTimesCalled = 0)
        {
            _mock.Verify(x => x.ListAsync(It.IsAny<Ardalis.Specification.ISpecification<T>>(), default), Times.Exactly(listAsyncTimesCalled));
            _mock.Verify(x => x.AddRangeAsync(It.IsAny<List<T>>(), default), Times.Exactly(addRangeAsyncTimesCalled));
            _mock.Verify(x => x.UpdateRangeAsync(It.IsAny<List<T>>(), default), Times.Exactly(updateRangeAsyncTimesCalled));
            _mock.Verify(x => x.AddAsync(It.IsAny<T>(), default), Times.Exactly(addAsyncTimesCalled));
            _mock.Verify(x => x.UpdateAsync(It.IsAny<T>(), default), Times.Exactly(updateAsyncTimesCalled));

            return true;
        }
    }

    public partial class MockCommand<T, TCommand, TCommandHandler, TResponse, TItem>
        where T : class, IAggregateRoot
        where TCommand : IRequest<TResponse>
        where TCommandHandler : IRequestHandler<TCommand, TResponse>
    {
        private TCommandHandler CommandHandler { get; set; }
        private TCommand Command { get; set; }


        public MockCommand(TItem item, IRepository<T> repository)
        {
            Type commandType = typeof(TCommand);

            ConstructorInfo commandConstructor = commandType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(TItem) },
                null
            );

            if (commandConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{commandType.FullName}' that takes a List<{typeof(TItem).FullName}> parameter.");

            Command = (TCommand)commandConstructor.Invoke(new object[] { item });

            Type commandHandlerType = typeof(TCommandHandler);

            ConstructorInfo commandHandlerConstructor = commandHandlerType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(IRepository<T>) },
                null
            );

            if (commandHandlerConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{commandType.FullName}' that takes a List<{typeof(IRepository<T>).FullName}> parameter.");


            CommandHandler = (TCommandHandler)commandHandlerConstructor.Invoke(new object[] { repository });
        }

        public async Task<bool> Execute()
        {
            var res = (await CommandHandler.Handle(Command, default)) as Result;
            return res.Succeeded;
        }
    }
    public partial class MockCommand<T, TCommand, TCommandHandler, TResponse>
        where T : class, IAggregateRoot
        where TCommand : IRequest<TResponse>
        where TCommandHandler : IRequestHandler<TCommand, TResponse>
    {
        private TCommandHandler CommandHandler { get; set; }
        private TCommand Command { get; set; }
        public MockCommand(IRepository<T> repository, params object[] parameters)
        {
            Type commandType = typeof(TCommand);
            Type[] parameterTypes = parameters.Select(o => o.GetType()).ToArray();

            ConstructorInfo commandConstructor = commandType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                parameterTypes,
                null
            );

            if (commandConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{commandType.FullName}' that takes {parameters} ");

            Command = (TCommand)commandConstructor.Invoke(parameters);

            Type commandHandlerType = typeof(TCommandHandler);

            ConstructorInfo commandHandlerConstructor = commandHandlerType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(IRepository<T>) },
                null
            );

            if (commandHandlerConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{commandType.FullName}' that takes a List<{typeof(IRepository<T>).FullName}> parameter.");


            CommandHandler = (TCommandHandler)commandHandlerConstructor.Invoke(new object[] { repository });
        }

        public async Task<bool> Execute()
        {
            var res = (await CommandHandler.Handle(Command, default)) as Result;
            return res.Succeeded;
        }

    }

    public class MockQuery<T, TQuery, TQueryHandler, TResponse, TItem>
        where T : class, IAggregateRoot
    where TQuery : IRequest<TResponse>
    where TQueryHandler : IRequestHandler<TQuery, TResponse>
    {
        private TQueryHandler QueryHandler { get; set; }
        private TQuery Query { get; set; }


        public MockQuery(List<TItem> items, IRepository<T> repository)
        {
            Type queryType = typeof(TQuery);

            ConstructorInfo queryConstructor = queryType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(List<TItem>) },
                null
            );

            if (queryConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{queryType.FullName}' that takes a List<{typeof(TItem).FullName}> parameter.");

            Query = (TQuery)queryConstructor.Invoke(new object[] { items });

            Type queryHandlerType = typeof(TQueryHandler);

            ConstructorInfo queryHandlerConstructor = queryHandlerType.GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new[] { typeof(IRepository<T>) },
                null
            );

            if (queryHandlerConstructor == null)
                throw new InvalidOperationException($"No constructor found for type '{queryType.FullName}' that takes a List<{typeof(TItem).FullName}> parameter.");


            QueryHandler = (TQueryHandler)queryHandlerConstructor.Invoke(new object[] { repository });
        }

        public async Task<bool> Execute()
        {
            var res = (await QueryHandler.Handle(Query, default)) as Result;
            return res.Succeeded;
        }
    }

    //[CollectionDefinition("UniqueId Generator")]
    //public class UniqueIdGeneratorCollection : ICollectionFixture<UniqueIdGeneratorDefinition> { }
    //public class UniqueIdGeneratorDefinition
    //{
    //    public UniqueIdGeneratorDefinition()
    //    {
    //        UniqueIdGenerator.Configure(0);
    //    }
    //}
}