﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using guestForm.Data;

#nullable disable

namespace guestForm.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("guestForm.Models.BookingForm", b =>
                {
                    b.Property<string>("FormId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<DateTime>("DateTimeFrom")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("DateTimeUpto")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("GuestNuFemaleSin")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("GuestNumChildren")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("GuestNumCouple")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("GuestNumMaleSin")
                        .HasColumnType("NUMBER(10)");

                    b.Property<bool>("IsAdminApproved")
                        .HasColumnType("BOOLEAN");

                    b.Property<bool>("IsRegistrarApproved")
                        .HasColumnType("BOOLEAN");

                    b.Property<bool>("MealsRequired")
                        .HasColumnType("BOOLEAN");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Relation")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("userId")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("FormId");

                    b.ToTable("BookingForms");
                });

            modelBuilder.Entity("guestForm.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
