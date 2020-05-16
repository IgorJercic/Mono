﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Service.Models;

namespace ProjectMono.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20200512191053_start1")]
    partial class start1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Service.VehicleMake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abrv");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VehicleMake");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abrv = "bmw",
                            Name = "BMW Group"
                        },
                        new
                        {
                            Id = 2,
                            Abrv = "Mercedes",
                            Name = "Mercedes-Benz company"
                        },
                        new
                        {
                            Id = 3,
                            Abrv = "toyota",
                            Name = "Toyota motor"
                        },
                        new
                        {
                            Id = 4,
                            Abrv = "mazda",
                            Name = "Mazda motor"
                        },
                        new
                        {
                            Id = 5,
                            Abrv = "lexus",
                            Name = "Lexus - Rekusasu"
                        });
                });

            modelBuilder.Entity("Project.Service.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abrv");

                    b.Property<int>("MakeId");

                    b.Property<string>("Name");

                    b.Property<int?>("VehicleMakeId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleMakeId");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("Project.Service.VehicleModel", b =>
                {
                    b.HasOne("Project.Service.VehicleMake")
                        .WithMany("vehicleModels")
                        .HasForeignKey("VehicleMakeId");
                });
#pragma warning restore 612, 618
        }
    }
}
