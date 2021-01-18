using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainTravel.DataAcess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "TicketHolders",
                columns: table => new
                {
                    TicketHolderID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CNP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketHolders", x => x.TicketHolderID);
                });

            migrationBuilder.CreateTable(
                name: "TrainSchedules",
                columns: table => new
                {
                    TrainScheduleID = table.Column<Guid>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DepartureCity = table.Column<string>(nullable: true),
                    DepartureHour = table.Column<string>(nullable: true),
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    ArrivalCity = table.Column<string>(nullable: true),
                    ArrivalHour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainSchedules", x => x.TrainScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "TrainTypes",
                columns: table => new
                {
                    TrainTypeID = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    CapacityClass1 = table.Column<int>(nullable: false),
                    CapacityClass2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainTypes", x => x.TrainTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TrainRoutes",
                columns: table => new
                {
                    TrainRouteID = table.Column<Guid>(nullable: false),
                    TrainRouteNo = table.Column<string>(nullable: true),
                    AdminID = table.Column<Guid>(nullable: true),
                    TrainScheduleID = table.Column<Guid>(nullable: true),
                    Class1Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Class2Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketsLeftC1 = table.Column<int>(nullable: false),
                    TicketsLeftC2 = table.Column<int>(nullable: false),
                    TrainTypeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainRoutes", x => x.TrainRouteID);
                    table.ForeignKey(
                        name: "FK_TrainRoutes_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainRoutes_TrainSchedules_TrainScheduleID",
                        column: x => x.TrainScheduleID,
                        principalTable: "TrainSchedules",
                        principalColumn: "TrainScheduleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainRoutes_TrainTypes_TrainTypeID",
                        column: x => x.TrainTypeID,
                        principalTable: "TrainTypes",
                        principalColumn: "TrainTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: true),
                    TicketHolderID = table.Column<Guid>(nullable: true),
                    Seat = table.Column<int>(nullable: false),
                    TicketType = table.Column<string>(nullable: true),
                    TrainRouteID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_TicketHolders_TicketHolderID",
                        column: x => x.TicketHolderID,
                        principalTable: "TicketHolders",
                        principalColumn: "TicketHolderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_TrainRoutes_TrainRouteID",
                        column: x => x.TrainRouteID,
                        principalTable: "TrainRoutes",
                        principalColumn: "TrainRouteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Code",
                table: "Admins",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Username",
                table: "Admins",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Username",
                table: "Clients",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientID",
                table: "Reservations",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TicketHolderID",
                table: "Reservations",
                column: "TicketHolderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TrainRouteID",
                table: "Reservations",
                column: "TrainRouteID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRoutes_AdminID",
                table: "TrainRoutes",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRoutes_TrainScheduleID",
                table: "TrainRoutes",
                column: "TrainScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRoutes_TrainTypeID",
                table: "TrainRoutes",
                column: "TrainTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "TicketHolders");

            migrationBuilder.DropTable(
                name: "TrainRoutes");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "TrainSchedules");

            migrationBuilder.DropTable(
                name: "TrainTypes");
        }
    }
}
