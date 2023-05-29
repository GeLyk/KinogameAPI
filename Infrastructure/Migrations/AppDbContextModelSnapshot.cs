﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Aggregates.Columns.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Cancel")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<bool>("KinoBonus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<int>("Multiplier")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SelectionGame")
                        .HasColumnType("int");

                    b.Property<string>("SelectionNumbers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SelectionRandom")
                        .HasColumnType("bit");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Columns", (string)null);
                });

            modelBuilder.Entity("Domain.Aggregates.Draws.Draw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DrawNumbers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Draws");
                });

            modelBuilder.Entity("Domain.Aggregates.Tickets.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<int>("DrawId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("Domain.Aggregates.Columns.Column", b =>
                {
                    b.HasOne("Domain.Aggregates.Tickets.Ticket", null)
                        .WithMany("Columns")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.ClientNoAction);

                    b.OwnsOne("Domain.ValueObjects.Columns.ColumnProfit", "Profit", b1 =>
                        {
                            b1.Property<int>("ColumnId")
                                .HasColumnType("int");

                            b1.Property<double>("Profit")
                                .HasColumnType("float")
                                .HasColumnName("Profit");

                            b1.HasKey("ColumnId");

                            b1.ToTable("Columns");

                            b1.WithOwner()
                                .HasForeignKey("ColumnId");
                        });

                    b.OwnsOne("Domain.ValueObjects.Columns.ColumnSuccess", "Success", b1 =>
                        {
                            b1.Property<int>("ColumnId")
                                .HasColumnType("int");

                            b1.Property<int>("Success")
                                .HasColumnType("int")
                                .HasColumnName("Success");

                            b1.HasKey("ColumnId");

                            b1.ToTable("Columns");

                            b1.WithOwner()
                                .HasForeignKey("ColumnId");
                        });

                    b.Navigation("Profit")
                        .IsRequired();

                    b.Navigation("Success")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.Tickets.Ticket", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.Tickets.TicketProfit", "Profit", b1 =>
                        {
                            b1.Property<int>("TicketId")
                                .HasColumnType("int");

                            b1.Property<double>("Profit")
                                .HasColumnType("float")
                                .HasColumnName("Profit");

                            b1.HasKey("TicketId");

                            b1.ToTable("Tickets");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });

                    b.Navigation("Profit")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.Tickets.Ticket", b =>
                {
                    b.Navigation("Columns");
                });
#pragma warning restore 612, 618
        }
    }
}
