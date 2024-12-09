﻿// <auto-generated />
using System;
using Egzaminas_ZmogausRegistravimoSistema.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    [DbContext(typeof(PersonRegistrationContext))]
    [Migration("20241204195422_HashPassword")]
    partial class HashPassword
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ResidenceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResidenceId");

                    b.ToTable("PersonInfos");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.Residence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Residences");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long?>("PersonInfoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonInfoId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", b =>
                {
                    b.HasOne("Egzaminas_ZmogausRegistravimoSistema.Entities.Residence", "Residence")
                        .WithMany()
                        .HasForeignKey("ResidenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Residence");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.User", b =>
                {
                    b.HasOne("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", "PersonInfo")
                        .WithMany()
                        .HasForeignKey("PersonInfoId");

                    b.Navigation("PersonInfo");
                });
#pragma warning restore 612, 618
        }
    }
}