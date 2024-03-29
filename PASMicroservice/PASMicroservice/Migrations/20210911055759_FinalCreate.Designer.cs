﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PASMicroservice.DBContexts;

namespace PASMicroservice.Migrations
{
    [DbContext(typeof(PASContext))]
    [Migration("20210911055759_FinalCreate")]
    partial class FinalCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PASMicroservice.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"),
                            Name = "PC komponente"
                        },
                        new
                        {
                            CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                            Name = "Grafičke kartice",
                            ParentCategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804")
                        },
                        new
                        {
                            CategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"),
                            Name = "Usluge | IT"
                        },
                        new
                        {
                            CategoryId = new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"),
                            Name = "Usluge | Web development",
                            ParentCategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772")
                        });
                });

            modelBuilder.Entity("PASMicroservice.Entities.Listing", b =>
                {
                    b.Property<Guid>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<bool?>("PriceContact")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool?>("PriceDeal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ListingId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ListingTypeId");

                    b.ToTable("Listings");

                    b.HasData(
                        new
                        {
                            ListingId = new Guid("accbc9e4-5705-4683-b30a-40b6e5758a73"),
                            CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                            Description = "polovna graficka kartica",
                            ListingTypeId = 1,
                            Name = "Sapphire Nitro+ RX 580 8GB",
                            Price = 150.0,
                            PriceContact = false,
                            PriceDeal = false,
                            UserId = 1337
                        },
                        new
                        {
                            ListingId = new Guid("a6a07aeb-86bb-4a15-9c70-ce4e439fb965"),
                            CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                            Description = "nova graficka kartica",
                            ListingTypeId = 1,
                            Name = "Gigabyte RTX 3080 Ti",
                            Price = 2499.9499999999998,
                            PriceContact = false,
                            PriceDeal = false,
                            UserId = 1338
                        },
                        new
                        {
                            ListingId = new Guid("ae63dcce-07d0-468e-9940-e840ee895aac"),
                            CategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"),
                            ListingTypeId = 1,
                            Name = "Cooler Master CPU Hladnjak",
                            PriceDeal = true,
                            UserId = 1339
                        },
                        new
                        {
                            ListingId = new Guid("794a9f38-8e36-4f94-901f-76c395727fc5"),
                            CategoryId = new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"),
                            ListingTypeId = 2,
                            Name = "Izrada Wordpress veb sajta",
                            Price = 150.0,
                            PriceContact = false,
                            PriceDeal = false,
                            UserId = 1337
                        },
                        new
                        {
                            ListingId = new Guid("5d6c5c19-d166-41c3-ba84-112a542c4b0c"),
                            CategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"),
                            Description = "Za vise informacija, kontaktirati",
                            ListingTypeId = 2,
                            Name = "SEO optimizacija",
                            PriceContact = true,
                            UserId = 1337
                        });
                });

            modelBuilder.Entity("PASMicroservice.Entities.ListingType", b =>
                {
                    b.Property<int>("ListingTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ListingTypeId");

                    b.ToTable("ListingTypes");

                    b.HasData(
                        new
                        {
                            ListingTypeId = 1,
                            Name = "product"
                        },
                        new
                        {
                            ListingTypeId = 2,
                            Name = "service"
                        });
                });

            modelBuilder.Entity("PASMicroservice.Entities.Category", b =>
                {
                    b.HasOne("PASMicroservice.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("PASMicroservice.Entities.Listing", b =>
                {
                    b.HasOne("PASMicroservice.Entities.Category", "Category")
                        .WithMany("Listings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PASMicroservice.Entities.ListingType", "ListingType")
                        .WithMany("Listings")
                        .HasForeignKey("ListingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ListingType");
                });

            modelBuilder.Entity("PASMicroservice.Entities.Category", b =>
                {
                    b.Navigation("Listings");
                });

            modelBuilder.Entity("PASMicroservice.Entities.ListingType", b =>
                {
                    b.Navigation("Listings");
                });
#pragma warning restore 612, 618
        }
    }
}
