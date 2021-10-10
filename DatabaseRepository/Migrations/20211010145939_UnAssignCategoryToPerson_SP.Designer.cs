﻿// <auto-generated />
using System;
using DatabaseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseRepository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211010145939_UnAssignCategoryToPerson_SP")]
    partial class UnAssignCategoryToPerson_SP
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseRepository.Gender", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gender1")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Gender");

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("DatabaseRepository.Organizations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("DatabaseRepository.PersonOrganizations", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonId", "OrganizationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("PersonOrganizations");
                });

            modelBuilder.Entity("DatabaseRepository.Persons", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte>("GenderId")
                        .HasColumnType("tinyint");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nchar(8)")
                        .IsFixedLength(true);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PersonalNumber" }, "UI_Persons_PersonalNumberId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("DatabaseRepository.PersonOrganizations", b =>
                {
                    b.HasOne("DatabaseRepository.Organizations", "Organization")
                        .WithMany("PersonOrganizations")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK_PersonOrganizations_Organizations")
                        .IsRequired();

                    b.HasOne("DatabaseRepository.Persons", "Person")
                        .WithMany("PersonOrganizations")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_PersonOrganizations_Persons")
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("DatabaseRepository.Organizations", b =>
                {
                    b.Navigation("PersonOrganizations");
                });

            modelBuilder.Entity("DatabaseRepository.Persons", b =>
                {
                    b.Navigation("PersonOrganizations");
                });
#pragma warning restore 612, 618
        }
    }
}