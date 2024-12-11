﻿// <auto-generated />
using System;
using Egzaminas_ZmogausRegistravimoSistema.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    [DbContext(typeof(PersonRegistrationContext))]
    partial class PersonRegistrationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<long>("PersonalId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

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

                    b.Property<long>("PersonInfoId")
                        .HasColumnType("bigint");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonInfoId")
                        .IsUnique();

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

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", b =>
                {
                    b.HasOne("Egzaminas_ZmogausRegistravimoSistema.Entities.User", "User")
                        .WithOne("PersonInfo")
                        .HasForeignKey("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.Residence", b =>
                {
                    b.HasOne("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", "PersonInfo")
                        .WithOne("Residence")
                        .HasForeignKey("Egzaminas_ZmogausRegistravimoSistema.Entities.Residence", "PersonInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonInfo");
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.PersonInfo", b =>
                {
                    b.Navigation("Residence")
                        .IsRequired();
                });

            modelBuilder.Entity("Egzaminas_ZmogausRegistravimoSistema.Entities.User", b =>
                {
                    b.Navigation("PersonInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
