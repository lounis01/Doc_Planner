﻿// <auto-generated />
using System;
using Doc_Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doc_Planner.Migrations
{
    [DbContext(typeof(AppointmentContext))]
    [Migration("20200424151526_BMIField")]
    partial class BMIField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Doc_Planner.Models.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BMI")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diabete")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ExamenType")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("HDebutRdv")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NISS")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("Poids")
                        .HasColumnType("float");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Sexe")
                        .HasColumnType("bit");

                    b.Property<double>("Taille")
                        .HasColumnType("float");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ID");

                    b.ToTable("appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
