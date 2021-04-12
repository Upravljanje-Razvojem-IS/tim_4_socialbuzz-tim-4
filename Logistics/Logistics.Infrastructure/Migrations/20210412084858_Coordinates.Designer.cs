﻿// <auto-generated />
using System;
using Logistics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logistics.Infrastructure.Migrations
{
    [DbContext(typeof(LogisticsDbContext))]
    [Migration("20210412084858_Coordinates")]
    partial class Coordinates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Logistics.Core.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Logistics.Core.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Logistics.Core.Entities.DistancePrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaximalDistance")
                        .HasColumnType("int");

                    b.Property<int>("MinimalDistance")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("DistancePrices");
                });

            modelBuilder.Entity("Logistics.Core.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DistancePriceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FromAddessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FromAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemMockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Pieces")
                        .HasColumnType("int");

                    b.Property<Guid>("ToAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("TotalPriceWithWeightAndDistance")
                        .HasColumnType("real");

                    b.Property<float>("TotalWeight")
                        .HasColumnType("real");

                    b.Property<Guid>("WeightRangeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DistancePriceId");

                    b.HasIndex("FromAddessId");

                    b.HasIndex("ItemMockId");

                    b.HasIndex("ToAddressId");

                    b.HasIndex("WeightRangeId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Logistics.Core.Entities.WeightRange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("MaximalWeight")
                        .HasColumnType("real");

                    b.Property<float>("MinimalWeight")
                        .HasColumnType("real");

                    b.Property<float>("PriceCoefficient")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("WeightRanges");
                });

            modelBuilder.Entity("Logistics.Core.Mock.ItemMock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PriceOfOne")
                        .HasColumnType("real");

                    b.Property<float>("WeightOfOne")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ItemMock");
                });

            modelBuilder.Entity("Logistics.Core.Entities.Address", b =>
                {
                    b.HasOne("Logistics.Core.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Logistics.Core.Entities.Purchase", b =>
                {
                    b.HasOne("Logistics.Core.Entities.DistancePrice", "DistancePrice")
                        .WithMany()
                        .HasForeignKey("DistancePriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistics.Core.Entities.Address", "FromAddess")
                        .WithMany()
                        .HasForeignKey("FromAddessId");

                    b.HasOne("Logistics.Core.Mock.ItemMock", "ItemMock")
                        .WithMany()
                        .HasForeignKey("ItemMockId");

                    b.HasOne("Logistics.Core.Entities.Address", "ToAddress")
                        .WithMany()
                        .HasForeignKey("ToAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logistics.Core.Entities.WeightRange", "WeightRange")
                        .WithMany()
                        .HasForeignKey("WeightRangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DistancePrice");

                    b.Navigation("FromAddess");

                    b.Navigation("ItemMock");

                    b.Navigation("ToAddress");

                    b.Navigation("WeightRange");
                });
#pragma warning restore 612, 618
        }
    }
}
