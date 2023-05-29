namespace Infrastructure.EF.Mappings
{
    class TicketMapping : IEntityTypeConfiguration<Domain.Aggregates.Tickets.Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Profit, p =>
            {
                p.Property(x => x.Profit).HasColumnName("Profit");
            });

            builder
                .HasMany(p => p.Columns)
                .WithOne()
                .HasForeignKey("TicketId")
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Property(p => p.CreatedOn)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(p => p.LastModifiedOn)
                .HasColumnName("UpdatedAt")
                .IsRequired(false);
        }
    }
}
