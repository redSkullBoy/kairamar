using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class mig202404141831 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotherAccounts");

            migrationBuilder.AddColumn<int>(
                name: "LastAddressId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LastAddressId",
                table: "AspNetUsers",
                column: "LastAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_LastAddressId",
                table: "AspNetUsers",
                column: "LastAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_LastAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LastAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastAddressId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AnotherAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AnotherAccounts_AppUserId",
                table: "AnotherAccounts",
                column: "AppUserId");
        }
    }
}
