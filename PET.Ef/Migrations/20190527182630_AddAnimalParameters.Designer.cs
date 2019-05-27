﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PET.Ef.DbContexts;

namespace PET.Ef.Migrations
{
    [DbContext(typeof(AnimalDbContext))]
    [Migration("20190527182630_AddAnimalParameters")]
    partial class AddAnimalParameters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PET.Domain.Models.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimalType");

                    b.Property<DateTime>("BDate");

                    b.Property<string>("Description");

                    b.Property<string>("Kind");

                    b.Property<string>("Name");

                    b.Property<bool>("Passport");

                    b.Property<int>("Sex");

                    b.Property<bool>("Sterilization");

                    b.Property<bool>("Vaccination");

                    b.HasKey("Id");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("PET.Domain.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AnimalId");

                    b.Property<string>("WayToFile");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("PET.Domain.Models.File", b =>
                {
                    b.HasOne("PET.Domain.Models.Animal")
                        .WithMany("Files")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
