﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motion.Database;

#nullable disable

namespace Motion.Migrations
{
    [DbContext(typeof(MotionDBContext))]
    partial class MotionDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Motion.Models.Domain.DLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("lat")
                        .HasColumnType("float");

                    b.Property<double>("lng")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("DLocations");
                });

            modelBuilder.Entity("Motion.Models.Domain.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License_plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicle_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Motion.Models.Domain.Request", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Did")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("ELatitude")
                        .HasColumnType("real");

                    b.Property<float>("ELongitude")
                        .HasColumnType("real");

                    b.Property<Guid>("Rid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("SLatitude")
                        .HasColumnType("real");

                    b.Property<float>("SLongitude")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Motion.Models.Domain.Ride", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Did")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Distance")
                        .HasColumnType("real");

                    b.Property<float>("ELatitude")
                        .HasColumnType("real");

                    b.Property<float>("ELongitude")
                        .HasColumnType("real");

                    b.Property<DateTime>("ETime")
                        .HasColumnType("datetime2");

                    b.Property<float>("Fare")
                        .HasColumnType("real");

                    b.Property<Guid>("Rid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("SLatitude")
                        .HasColumnType("real");

                    b.Property<float>("SLongitude")
                        .HasColumnType("real");

                    b.Property<DateTime>("Stime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("Motion.Models.Domain.Rider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Riders");
                });
#pragma warning restore 612, 618
        }
    }
}
