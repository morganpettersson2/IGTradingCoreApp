﻿// <auto-generated />
using System;
using IG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IG.Data.Migrations
{
    [DbContext(typeof(IgContext))]
    [Migration("20181228132415_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IG.Domain.HierarchyMarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Bid");

                    b.Property<int>("DelayTime");

                    b.Property<string>("Epic");

                    b.Property<string>("Expiry");

                    b.Property<long>("HierarchyNodeId");

                    b.Property<decimal?>("High");

                    b.Property<string>("InstrumentName");

                    b.Property<string>("InstrumentType");

                    b.Property<int>("LotSize");

                    b.Property<decimal?>("Low");

                    b.Property<string>("MarketStatus");

                    b.Property<decimal?>("NetChange");

                    b.Property<decimal?>("Offer");

                    b.Property<bool>("OtcTradeable");

                    b.Property<decimal?>("PercentageChange");

                    b.Property<int>("ScalingFactor");

                    b.Property<bool>("StreamingPricesAvailable");

                    b.Property<string>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("HierarchyNodeId")
                        .IsUnique();

                    b.ToTable("HierarchyMarkets");
                });

            modelBuilder.Entity("IG.Domain.HierarchyNode", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("HierarchyNodes");
                });

            modelBuilder.Entity("IG.Domain.TimeFrames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TimeFrameId");

                    b.HasKey("Id");

                    b.ToTable("TimeFrames");
                });

            modelBuilder.Entity("IG.Domain.HierarchyMarket", b =>
                {
                    b.HasOne("IG.Domain.HierarchyNode", "HierarchyNode")
                        .WithMany()
                        .HasForeignKey("HierarchyNodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}