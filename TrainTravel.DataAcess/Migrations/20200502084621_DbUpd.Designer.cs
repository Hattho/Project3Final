﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainTravel.DataAcess;

namespace TrainTravel.DataAcess.Migrations
{
    [DbContext(typeof(TrainTravelDbContext))]
    [Migration("20200502084621_DbUpd")]
    partial class DbUpd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.Admin", b =>
                {
                    b.Property<Guid>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdminID");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.Client", b =>
                {
                    b.Property<Guid>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.Reservation", b =>
                {
                    b.Property<Guid>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Seat")
                        .HasColumnType("int");

                    b.Property<Guid?>("TicketHolderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TicketType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TrainRouteID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReservationID");

                    b.HasIndex("ClientID");

                    b.HasIndex("TicketHolderID");

                    b.HasIndex("TrainRouteID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.TicketHolder", b =>
                {
                    b.Property<Guid>("TicketHolderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CNP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketHolderID");

                    b.ToTable("TicketHolders");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.TrainRoute", b =>
                {
                    b.Property<Guid>("TrainRouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdminID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Class1Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Class2Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TicketsLeftC1")
                        .HasColumnType("int");

                    b.Property<int>("TicketsLeftC2")
                        .HasColumnType("int");

                    b.Property<string>("TrainRouteNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TrainScheduleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TrainTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TrainRouteID");

                    b.HasIndex("AdminID");

                    b.HasIndex("TrainScheduleID");

                    b.HasIndex("TrainTypeID");

                    b.ToTable("TrainRoutes");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.TrainSchedule", b =>
                {
                    b.Property<Guid>("TrainScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArrivalCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ArrivalHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartureHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrainScheduleID");

                    b.ToTable("TrainSchedules");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.TrainType", b =>
                {
                    b.Property<Guid>("TrainTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CapacityClass1")
                        .HasColumnType("int");

                    b.Property<int>("CapacityClass2")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrainTypeID");

                    b.ToTable("TrainTypes");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.Reservation", b =>
                {
                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID");

                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.TicketHolder", "TicketHolder")
                        .WithMany()
                        .HasForeignKey("TicketHolderID");

                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.TrainRoute", null)
                        .WithMany("Reservations")
                        .HasForeignKey("TrainRouteID");
                });

            modelBuilder.Entity("TrainTravel.ApplicationLogic.DataModel.TrainRoute", b =>
                {
                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminID");

                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.TrainSchedule", "TrainSchedule")
                        .WithMany()
                        .HasForeignKey("TrainScheduleID");

                    b.HasOne("TrainTravel.ApplicationLogic.DataModel.TrainType", null)
                        .WithMany("TrainRoutes")
                        .HasForeignKey("TrainTypeID");
                });
#pragma warning restore 612, 618
        }
    }
}
