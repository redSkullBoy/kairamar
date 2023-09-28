using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Sqlite.Migrations
{
    public partial class mig280920231308 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnotherAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnotherAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnotherAccounts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TripCompanions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanionId = table.Column<string>(type: "TEXT", nullable: false),
                    AmountSeats = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripCompanions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripCompanions_AspNetUsers_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    Locale = table.Column<string>(type: "TEXT", nullable: false),
                    StartDateLocal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateLocal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequestedSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    RadiusInMeters = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    InitiatorId = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Addresses_FromAddressId",
                        column: x => x.FromAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Addresses_ToAddressId",
                        column: x => x.ToAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_AspNetUsers_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnotherAccounts_AppUserId",
                table: "AnotherAccounts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripCompanions_CompanionId",
                table: "TripCompanions",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FromAddressId",
                table: "Trips",
                column: "FromAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_InitiatorId",
                table: "Trips",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ToAddressId",
                table: "Trips",
                column: "ToAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotherAccounts");

            migrationBuilder.DropTable(
                name: "TripCompanions");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Attempts = table.Column<int>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsSended = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });
        }
    }
}
