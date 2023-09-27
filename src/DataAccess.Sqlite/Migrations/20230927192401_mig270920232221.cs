using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Sqlite.Migrations
{
    public partial class mig270920232221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CountryIsoCode = table.Column<string>(type: "TEXT", nullable: true),
                    RegionIsoCode = table.Column<string>(type: "TEXT", nullable: true),
                    RegionFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    RegionKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    RegionWithType = table.Column<string>(type: "TEXT", nullable: true),
                    RegionType = table.Column<string>(type: "TEXT", nullable: true),
                    RegionTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    AreaFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    AreaKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    AreaWithType = table.Column<string>(type: "TEXT", nullable: true),
                    AreaType = table.Column<string>(type: "TEXT", nullable: true),
                    AreaTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Area = table.Column<string>(type: "TEXT", nullable: true),
                    CityFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    CityKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    CityWithType = table.Column<string>(type: "TEXT", nullable: true),
                    CityType = table.Column<string>(type: "TEXT", nullable: true),
                    CityTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    CityArea = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictWithType = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictType = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    CityDistrictArea = table.Column<string>(type: "TEXT", nullable: true),
                    SettlementFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    SettlementKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    SettlementWithType = table.Column<string>(type: "TEXT", nullable: true),
                    SettlementType = table.Column<string>(type: "TEXT", nullable: true),
                    SettlementTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Settlement = table.Column<string>(type: "TEXT", nullable: true),
                    StreetFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    StreetKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    StreetWithType = table.Column<string>(type: "TEXT", nullable: true),
                    StreetType = table.Column<string>(type: "TEXT", nullable: true),
                    StreetTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    HouseFiasId = table.Column<string>(type: "TEXT", nullable: true),
                    HouseKladrId = table.Column<string>(type: "TEXT", nullable: true),
                    HouseType = table.Column<string>(type: "TEXT", nullable: true),
                    HouseTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    House = table.Column<string>(type: "TEXT", nullable: true),
                    BlockType = table.Column<string>(type: "TEXT", nullable: true),
                    BlockTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Block = table.Column<string>(type: "TEXT", nullable: true),
                    FlatType = table.Column<string>(type: "TEXT", nullable: true),
                    FlatTypeFull = table.Column<string>(type: "TEXT", nullable: true),
                    Flat = table.Column<string>(type: "TEXT", nullable: true),
                    FlatArea = table.Column<string>(type: "TEXT", nullable: true),
                    SquareMeterPrice = table.Column<string>(type: "TEXT", nullable: true),
                    FlatPrice = table.Column<string>(type: "TEXT", nullable: true),
                    PostalBox = table.Column<string>(type: "TEXT", nullable: true),
                    FiasId = table.Column<string>(type: "TEXT", nullable: true),
                    FiasLevel = table.Column<string>(type: "TEXT", nullable: true),
                    FiasActualityState = table.Column<string>(type: "TEXT", nullable: true),
                    KladrId = table.Column<string>(type: "TEXT", nullable: true),
                    CapitalMarker = table.Column<string>(type: "TEXT", nullable: true),
                    Okato = table.Column<string>(type: "TEXT", nullable: true),
                    Oktmo = table.Column<string>(type: "TEXT", nullable: true),
                    TaxOffice = table.Column<string>(type: "TEXT", nullable: true),
                    TaxOfficeLegal = table.Column<string>(type: "TEXT", nullable: true),
                    Timezone = table.Column<string>(type: "TEXT", nullable: true),
                    GeoLat = table.Column<string>(type: "TEXT", nullable: true),
                    GeoLon = table.Column<string>(type: "TEXT", nullable: true),
                    BeltwayHit = table.Column<string>(type: "TEXT", nullable: true),
                    BeltwayDistance = table.Column<string>(type: "TEXT", nullable: true),
                    QcGeo = table.Column<string>(type: "TEXT", nullable: true),
                    QcComplete = table.Column<string>(type: "TEXT", nullable: true),
                    QcHouse = table.Column<string>(type: "TEXT", nullable: true),
                    UnparsedParts = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    Qc = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    IsSended = table.Column<bool>(type: "INTEGER", nullable: false),
                    Attempts = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
