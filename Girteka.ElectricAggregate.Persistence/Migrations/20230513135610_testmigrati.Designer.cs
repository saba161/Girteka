﻿// <auto-generated />
using System;
using Girteka.ElectricAggregate.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Girteka.ElectricAggregate.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230513135610_testmigrati")]
    partial class testmigrati
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Girteka.ElectricAggregate.Domain.Models.Electricity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Numeris")
                        .HasColumnType("int");

                    b.Property<decimal?>("PMinus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PPlus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Pavadinimas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PlT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tinklas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipas")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Electricities");
                });
#pragma warning restore 612, 618
        }
    }
}
