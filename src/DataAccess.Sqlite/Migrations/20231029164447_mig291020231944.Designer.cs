﻿// <auto-generated />
using System;
using DataAccess.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Sqlite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231029164447_mig291020231944")]
    partial class mig291020231944
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("DataAccess.Sqlite.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Area")
                        .HasColumnType("TEXT");

                    b.Property<string>("AreaFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AreaKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AreaType")
                        .HasColumnType("TEXT");

                    b.Property<string>("AreaTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("AreaWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("BeltwayDistance")
                        .HasColumnType("TEXT");

                    b.Property<string>("BeltwayHit")
                        .HasColumnType("TEXT");

                    b.Property<string>("Block")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlockType")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlockTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("CapitalMarker")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityArea")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictArea")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictType")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityDistrictWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityType")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("CityWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryIsoCode")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("FiasActualityState")
                        .HasColumnType("TEXT");

                    b.Property<string>("FiasId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FiasLevel")
                        .HasColumnType("TEXT");

                    b.Property<string>("Flat")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlatArea")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlatPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlatType")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlatTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("GeoLat")
                        .HasColumnType("TEXT");

                    b.Property<string>("GeoLon")
                        .HasColumnType("TEXT");

                    b.Property<string>("House")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseType")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("KladrId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("Okato")
                        .HasColumnType("TEXT");

                    b.Property<string>("Oktmo")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalBox")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Qc")
                        .HasColumnType("TEXT");

                    b.Property<string>("QcComplete")
                        .HasColumnType("TEXT");

                    b.Property<string>("QcGeo")
                        .HasColumnType("TEXT");

                    b.Property<string>("QcHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("Region")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionIsoCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionType")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Settlement")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettlementFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettlementKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettlementType")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettlementTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettlementWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("SquareMeterPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetFiasId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetKladrId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetType")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetTypeFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetWithType")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaxOffice")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaxOfficeLegal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Timezone")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnparsedParts")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Domain.Entities.Model.AnotherAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("AnotherAccounts");
                });

            modelBuilder.Entity("Domain.Entities.Model.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDateLocal")
                        .HasColumnType("TEXT");

                    b.Property<int>("FromAddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InitiatorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Locale")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("RadiusInMeters")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RequestedSeats")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDateLocal")
                        .HasColumnType("TEXT");

                    b.Property<int>("ToAddressId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromAddressId");

                    b.HasIndex("InitiatorId");

                    b.HasIndex("ToAddressId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Domain.Entities.Model.TripPassenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TripId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.ToTable("TripPassengers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Model.AnotherAccount", b =>
                {
                    b.HasOne("DataAccess.Sqlite.AppUser", "AppUser")
                        .WithMany("AnotherAccounts")
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Domain.Entities.Model.Trip", b =>
                {
                    b.HasOne("Domain.Entities.Model.Address", "FromAddress")
                        .WithMany()
                        .HasForeignKey("FromAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Sqlite.AppUser", "Initiator")
                        .WithMany()
                        .HasForeignKey("InitiatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Model.Address", "ToAddress")
                        .WithMany()
                        .HasForeignKey("ToAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromAddress");

                    b.Navigation("Initiator");

                    b.Navigation("ToAddress");
                });

            modelBuilder.Entity("Domain.Entities.Model.TripPassenger", b =>
                {
                    b.HasOne("DataAccess.Sqlite.AppUser", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Sqlite.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Sqlite.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Sqlite.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Sqlite.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Sqlite.AppUser", b =>
                {
                    b.Navigation("AnotherAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
