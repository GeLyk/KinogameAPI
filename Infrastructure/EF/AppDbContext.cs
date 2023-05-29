namespace Infrastructure.EF
{
    public class AppDbContext : DbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Draw> Draws { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Column> Columns { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            ReadDateTimeAsLOCAL(modelBuilder);
        }
        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var modifiedEntries = ChangeTracker.Entries()
                           .Where(x => x.Entity is BaseEntity
                           && (x.State == EntityState.Added
                           || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entity is null)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            entity.CreatedOn = DateTime.Now.ToLocalTime();
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entity.LastModifiedOn = DateTime.Now.ToLocalTime();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }
        private static void ReadDateTimeAsLOCAL(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v.ToLocalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Local));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }
}
