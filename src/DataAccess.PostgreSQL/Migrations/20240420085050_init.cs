using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fias_id = table.Column<string>(type: "text", nullable: false),
                    fias_level = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    postal_code = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    country_iso_code = table.Column<string>(type: "text", nullable: true),
                    region_iso_code = table.Column<string>(type: "text", nullable: true),
                    region_fias_id = table.Column<string>(type: "text", nullable: true),
                    region_kladr_id = table.Column<string>(type: "text", nullable: true),
                    region_with_type = table.Column<string>(type: "text", nullable: true),
                    region_type = table.Column<string>(type: "text", nullable: true),
                    region_type_full = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "text", nullable: true),
                    area_fias_id = table.Column<string>(type: "text", nullable: true),
                    area_kladr_id = table.Column<string>(type: "text", nullable: true),
                    area_with_type = table.Column<string>(type: "text", nullable: true),
                    area_type = table.Column<string>(type: "text", nullable: true),
                    area_type_full = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<string>(type: "text", nullable: true),
                    city_fias_id = table.Column<string>(type: "text", nullable: true),
                    city_kladr_id = table.Column<string>(type: "text", nullable: true),
                    city_with_type = table.Column<string>(type: "text", nullable: true),
                    city_type = table.Column<string>(type: "text", nullable: true),
                    city_type_full = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    city_area = table.Column<string>(type: "text", nullable: true),
                    city_district_fias_id = table.Column<string>(type: "text", nullable: true),
                    city_district_kladr_id = table.Column<string>(type: "text", nullable: true),
                    city_district_with_type = table.Column<string>(type: "text", nullable: true),
                    city_district_type = table.Column<string>(type: "text", nullable: true),
                    city_district_type_full = table.Column<string>(type: "text", nullable: true),
                    city_district_area = table.Column<string>(type: "text", nullable: true),
                    settlement_fias_id = table.Column<string>(type: "text", nullable: true),
                    settlement_kladr_id = table.Column<string>(type: "text", nullable: true),
                    settlement_with_type = table.Column<string>(type: "text", nullable: true),
                    settlement_type = table.Column<string>(type: "text", nullable: true),
                    settlement_type_full = table.Column<string>(type: "text", nullable: true),
                    settlement = table.Column<string>(type: "text", nullable: true),
                    street_fias_id = table.Column<string>(type: "text", nullable: true),
                    street_kladr_id = table.Column<string>(type: "text", nullable: true),
                    street_with_type = table.Column<string>(type: "text", nullable: true),
                    street_type = table.Column<string>(type: "text", nullable: true),
                    street_type_full = table.Column<string>(type: "text", nullable: true),
                    street = table.Column<string>(type: "text", nullable: true),
                    house_fias_id = table.Column<string>(type: "text", nullable: true),
                    house_kladr_id = table.Column<string>(type: "text", nullable: true),
                    house_type = table.Column<string>(type: "text", nullable: true),
                    house_type_full = table.Column<string>(type: "text", nullable: true),
                    house = table.Column<string>(type: "text", nullable: true),
                    block_type = table.Column<string>(type: "text", nullable: true),
                    block_type_full = table.Column<string>(type: "text", nullable: true),
                    block = table.Column<string>(type: "text", nullable: true),
                    flat_type = table.Column<string>(type: "text", nullable: true),
                    flat_type_full = table.Column<string>(type: "text", nullable: true),
                    flat = table.Column<string>(type: "text", nullable: true),
                    flat_area = table.Column<string>(type: "text", nullable: true),
                    square_meter_price = table.Column<string>(type: "text", nullable: true),
                    flat_price = table.Column<string>(type: "text", nullable: true),
                    postal_box = table.Column<string>(type: "text", nullable: true),
                    fias_actuality_state = table.Column<string>(type: "text", nullable: true),
                    kladr_id = table.Column<string>(type: "text", nullable: true),
                    capital_marker = table.Column<string>(type: "text", nullable: true),
                    okato = table.Column<string>(type: "text", nullable: true),
                    oktmo = table.Column<string>(type: "text", nullable: true),
                    tax_office = table.Column<string>(type: "text", nullable: true),
                    tax_office_legal = table.Column<string>(type: "text", nullable: true),
                    timezone = table.Column<string>(type: "text", nullable: true),
                    geo_lat = table.Column<string>(type: "text", nullable: true),
                    geo_lon = table.Column<string>(type: "text", nullable: true),
                    beltway_hit = table.Column<string>(type: "text", nullable: true),
                    beltway_distance = table.Column<string>(type: "text", nullable: true),
                    qc_geo = table.Column<string>(type: "text", nullable: true),
                    qc_complete = table.Column<string>(type: "text", nullable: true),
                    qc_house = table.Column<string>(type: "text", nullable: true),
                    unparsed_parts = table.Column<string>(type: "text", nullable: true),
                    source = table.Column<string>(type: "text", nullable: true),
                    qc = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    user_type = table.Column<int>(type: "integer", nullable: false),
                    last_address_id = table.Column<int>(type: "integer", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_users_addresses_last_address_id",
                        column: x => x.last_address_id,
                        principalTable: "addresses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    from_address_id = table.Column<int>(type: "integer", nullable: false),
                    to_address_id = table.Column<int>(type: "integer", nullable: false),
                    locale = table.Column<string>(type: "text", nullable: false),
                    start_date_local = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date_local = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    requested_seats = table.Column<int>(type: "integer", nullable: false),
                    radius_in_meters = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    initiator_id = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trips", x => x.id);
                    table.ForeignKey(
                        name: "fk_trips_addresses_from_address_id",
                        column: x => x.from_address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trips_addresses_to_address_id",
                        column: x => x.to_address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trips_users_initiator_id",
                        column: x => x.initiator_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trip_passengers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trip_id = table.Column<int>(type: "integer", nullable: false),
                    passenger_id = table.Column<string>(type: "text", nullable: false),
                    amount_seats = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trip_passengers", x => x.id);
                    table.ForeignKey(
                        name: "fk_trip_passengers_trips_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trip_passengers_users_passenger_id",
                        column: x => x.passenger_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_last_address_id",
                table: "AspNetUsers",
                column: "last_address_id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trip_passengers_passenger_id",
                table: "trip_passengers",
                column: "passenger_id");

            migrationBuilder.CreateIndex(
                name: "ix_trip_passengers_trip_id",
                table: "trip_passengers",
                column: "trip_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trips_from_address_id",
                table: "trips",
                column: "from_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_initiator_id",
                table: "trips",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_to_address_id",
                table: "trips",
                column: "to_address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "trip_passengers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "addresses");
        }
    }
}
