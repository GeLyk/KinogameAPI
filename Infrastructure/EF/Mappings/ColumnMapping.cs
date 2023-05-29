namespace Infrastructure.EF.Mappings
{
    class ColumnMapping : IEntityTypeConfiguration<Domain.Aggregates.Columns.Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("Columns");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Profit, p =>
            {
                p.Property(x => x.Profit).HasColumnName("Profit");
            });

            builder.OwnsOne(p => p.Success, p =>
            {
                p.Property(x => x.Success).HasColumnName("Success");
            });

            builder.Property(p => p.CreatedOn)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(p => p.LastModifiedOn)
                .HasColumnName("UpdatedAt")
                .IsRequired(false);
        }
    }
}
