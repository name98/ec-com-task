﻿// <auto-generated />
using System;
using EdCom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EdCom.Data.Migrations
{
    [DbContext(typeof(EdComDbContext))]
    [Migration("20241217210509_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EdCom.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<int>("Order")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("ix_categories_title");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d14e7c25-5351-47d6-a614-96efa7497dc2"),
                            DateCreated = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Order = 100,
                            Title = "Food"
                        },
                        new
                        {
                            Id = new Guid("94efe18c-e82c-48a1-b4ee-96921b3f2ea0"),
                            DateCreated = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Order = 200,
                            Title = "Transport"
                        },
                        new
                        {
                            Id = new Guid("e2a18bf7-01e3-47cf-baf7-944e8b759198"),
                            DateCreated = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Order = 300,
                            Title = "Mobile Network"
                        },
                        new
                        {
                            Id = new Guid("d5d150a6-0b9b-4d44-aed9-ae42cca848b5"),
                            DateCreated = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Order = 400,
                            Title = "Internet"
                        },
                        new
                        {
                            Id = new Guid("6548bc25-0cb2-4fec-be40-d296ab261ec9"),
                            DateCreated = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Order = 500,
                            Title = "Entertainment"
                        });
                });

            modelBuilder.Entity("EdCom.Domain.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_purchase");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("pk_purchases");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_purchases_category_id");

                    b.ToTable("purchases", (string)null);
                });

            modelBuilder.Entity("EdCom.Domain.Purchase", b =>
                {
                    b.HasOne("EdCom.Domain.Category", "Category")
                        .WithMany("Purchases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_purchases_categories_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EdCom.Domain.Category", b =>
                {
                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}