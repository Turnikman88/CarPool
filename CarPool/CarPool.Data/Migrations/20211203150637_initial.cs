﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoogleAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    ApplicationRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.CheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ban",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    BlockedOn = table.Column<DateTime>(nullable: true),
                    BlockedDue = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ban_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    AddedByUserId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    IsReport = table.Column<bool>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: false),
                    StartAddressId = table.Column<int>(nullable: false),
                    DestinationAddressId = table.Column<int>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    DurationInMinutes = table.Column<int>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PassengersCount = table.Column<int>(nullable: false),
                    FreeSeats = table.Column<int>(nullable: false),
                    AdditionalComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_DestinationAddresses",
                        column: x => x.DestinationAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_ApplicationUsers",
                        column: x => x.DriverId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_StartAddresses",
                        column: x => x.StartAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    FuelConsumptionPerHundredKilometers = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVehicles_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPassengers",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPassengers", x => new { x.ApplicationUserId, x.TripId });
                    table.ForeignKey(
                        name: "FK_TripPassengerRelation_ApplicationUsers",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripPassengerRelation_Trips",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(7919), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(8318), null, false, null, "User" },
                    { 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(8331), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(8332), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 3, 15, 6, 35, 992, DateTimeKind.Utc).AddTicks(1377), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 12, 3, 15, 6, 35, 992, DateTimeKind.Utc).AddTicks(3216), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 12, 3, 15, 6, 35, 992, DateTimeKind.Utc).AddTicks(3242), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 12, 3, 15, 6, 35, 992, DateTimeKind.Utc).AddTicks(3245), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 3, 15, 6, 35, 993, DateTimeKind.Utc).AddTicks(9490), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(990), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1032), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1035), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1138), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1143), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1037), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1042), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1045), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1134), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1145), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(1146), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(3270), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 6, 1, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6937), null, false, 44.432558m, 26.111871m, null, null },
                    { 2, 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6870), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 7, 2, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6940), null, false, 44.432558m, 26.111871m, null, null },
                    { 3, 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6924), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 8, 3, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6943), null, false, 44.432558m, 26.111871m, null, null },
                    { 4, 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6929), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 9, 4, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6945), null, false, 44.432558m, 26.111871m, null, null },
                    { 5, 5, new DateTime(2021, 12, 3, 15, 6, 35, 994, DateTimeKind.Utc).AddTicks(6931), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 1, 2, new DateTime(2021, 12, 3, 15, 6, 35, 995, DateTimeKind.Utc).AddTicks(1280), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$Q5m3z/FM2XIJGaywi1psu.maUA9H0bNRUmLCe2cKbxGr4cNN9Tt2m", "+35920768005", "misha_m" },
                    { new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 1, 1, new DateTime(2021, 12, 3, 15, 6, 36, 616, DateTimeKind.Utc).AddTicks(2423), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$xffsD7vGXDh7lvDGVS0AOuCK0.LmeEkkKfimT1QfJ5zjvONFz.ijK", "+35924775508", "cicibar" },
                    { new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 2, 2, new DateTime(2021, 12, 3, 15, 6, 36, 198, DateTimeKind.Utc).AddTicks(4954), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$szNvvRtL7ywQ56fiPj/vwOURT4JulGOIsY6LBMqaFm/t7d/k.a0Gq", "+35924492877", "petio_p" },
                    { new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 3, 2, new DateTime(2021, 12, 3, 15, 6, 36, 403, DateTimeKind.Utc).AddTicks(3932), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$WAnezMNiOnMFjshCwksJtelNyybd6FEPMrSjYAJ72w9TRMDX83Zcy", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), null, new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 12, 3, 15, 6, 36, 803, DateTimeKind.Utc).AddTicks(6193), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(6965), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(7876), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(7878), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(7855), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "IsReport", "ModifiedOn", "TripId", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(9036), "Nice car", false, null, 1, 4 },
                    { 3, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), new DateTime(2021, 12, 3, 15, 6, 36, 805, DateTimeKind.Utc).AddTicks(1063), "(No feedback)", false, null, 5, 5 },
                    { 2, new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), new DateTime(2021, 12, 3, 15, 6, 36, 805, DateTimeKind.Utc).AddTicks(1022), "Bad person", false, null, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3523), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3525), 2, 240, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3506), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3509), 4, 240, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2986), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2988), 3, 240, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2939), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2942), 4, 240, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2932), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2934), 1, 240, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2923), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2926), 2, 210, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3529), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3531), 1, 240, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3516), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3518), 3, 240, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2980), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2982), 1, 240, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 120, 2, null, 1, 0m, 3 },
                    { 7, "NO EATING", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2961), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2963), 2, 240, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 120, 2, null, 1, 0m, 4 },
                    { 2, "NO SMOKING", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2831), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2873), 3, 240, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 120, 2, null, 2, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3592), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3595), 3, 240, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 12, 3, 15, 6, 36, 803, DateTimeKind.Utc).AddTicks(8868), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(1022), 2, 340, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 90, 2, null, 2, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(3464), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(3474), 3, 240, new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2954), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2956), 4, 240, new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 120, 2, null, 1, 0m, 2 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2967), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2968), 2, 240, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 120, 2, null, 1, 0m, 1 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(2972), new DateTime(2021, 12, 3, 17, 6, 36, 804, DateTimeKind.Local).AddTicks(2974), 3, 240, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 120, 2, null, 1, 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "DeletedOn", "FuelConsumptionPerHundredKilometers", "IsDeleted", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 2, new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), "Blue", new DateTime(2021, 12, 3, 15, 6, 36, 800, DateTimeKind.Utc).AddTicks(3635), null, 8.0, false, "Alfa Romeo", null },
                    { 4, new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), "Silver", new DateTime(2021, 12, 3, 15, 6, 36, 800, DateTimeKind.Utc).AddTicks(3739), null, 15.0, false, "BMW M5", null },
                    { 1, new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), "Red", new DateTime(2021, 12, 3, 15, 6, 36, 800, DateTimeKind.Utc).AddTicks(1650), null, 12.0, false, "Ferrari", null },
                    { 3, new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), "Black", new DateTime(2021, 12, 3, 15, 6, 36, 800, DateTimeKind.Utc).AddTicks(3709), null, 10.0, false, "Mercedes S Class", null }
                });

            migrationBuilder.InsertData(
                table: "TripPassengers",
                columns: new[] { "ApplicationUserId", "TripId", "CreatedOn" },
                values: new object[,]
                {
                    { new Guid("28947e23-5a54-47ab-bd7b-344c4c4bc2c0"), 1, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5045) },
                    { new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 1, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5932) },
                    { new Guid("012e59fb-3f50-4ffc-94b1-6111c8e80338"), 2, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5909) },
                    { new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 2, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5933) },
                    { new Guid("8b287b73-6247-4fab-9e55-56a6ba3a180c"), 3, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5930) },
                    { new Guid("c1da4e73-7237-42ca-8d15-1aeae05ebd5b"), 3, new DateTime(2021, 12, 3, 15, 6, 36, 804, DateTimeKind.Utc).AddTicks(5938) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_AddressId",
                table: "ApplicationUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationRoleId",
                table: "ApplicationUsers",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Email",
                table: "ApplicationUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_PhoneNumber",
                table: "ApplicationUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Username",
                table: "ApplicationUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ban_ApplicationUserId",
                table: "Ban",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_CountryId",
                table: "Cities",
                columns: new[] { "Name", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_ApplicationUserId",
                table: "ProfilePictures",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPassengers_TripId",
                table: "TripPassengers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationAddressId",
                table: "Trips",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StartAddressId",
                table: "Trips",
                column: "StartAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVehicles_ApplicationUserId",
                table: "UserVehicles",
                column: "ApplicationUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ban");

            migrationBuilder.DropTable(
                name: "GoogleAccount");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "TripPassengers");

            migrationBuilder.DropTable(
                name: "UserVehicles");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
