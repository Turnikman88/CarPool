﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class tripstocities : Migration
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
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
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
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
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
                    { 1, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(4005), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(4213), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(4222), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(4223), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 25, 22, 44, 50, 704, DateTimeKind.Utc).AddTicks(7627), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 25, 22, 44, 50, 704, DateTimeKind.Utc).AddTicks(8575), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 25, 22, 44, 50, 704, DateTimeKind.Utc).AddTicks(8592), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 25, 22, 44, 50, 704, DateTimeKind.Utc).AddTicks(8593), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 22, 44, 50, 705, DateTimeKind.Utc).AddTicks(9524), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(339), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(364), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(365), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(373), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(375), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(367), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(370), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(371), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(372), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(377), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(378), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(1625), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 6, 1, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3377), null, false, 44.432558m, 26.111871m, null, null },
                    { 2, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3335), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 7, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3379), null, false, 44.432558m, 26.111871m, null, null },
                    { 3, 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3370), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 8, 3, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3380), null, false, 44.432558m, 26.111871m, null, null },
                    { 4, 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3372), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 9, 4, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3382), null, false, 44.432558m, 26.111871m, null, null },
                    { 5, 5, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(3374), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), 1, 2, new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(7674), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$I37wzEEIWKnpfJxq0W4jIORPbs3TrVftETrkY.Aane.PB15EHSFE6", "+35920768005", "misha_m" },
                    { new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), 1, 1, new DateTime(2021, 11, 25, 22, 44, 51, 65, DateTimeKind.Utc).AddTicks(5266), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$owqXQ0XGx.iidfZxeroX9u0n44VMVintn1RL0TF9Zd9lia9NtIr6i", "+35924775508", "cicibar" },
                    { new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 2, 2, new DateTime(2021, 11, 25, 22, 44, 50, 827, DateTimeKind.Utc).AddTicks(9528), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$PlLQrGmqfQsFHvicwd84sO5a4r5M2E9yrK6tpTo9pUGMzge5tuU8S", "+35924492877", "petio_p" },
                    { new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 3, 2, new DateTime(2021, 11, 25, 22, 44, 50, 950, DateTimeKind.Utc).AddTicks(1986), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$lqtBA0URRV/mrjOaf6oKiu1iNecxpw3ya2Bdu.VjS6SGXKmmnoDQa", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), null, new DateTime(2021, 11, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(3712), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), new DateTime(2021, 11, 25, 22, 44, 51, 189, DateTimeKind.Utc).AddTicks(586), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), new DateTime(2021, 11, 25, 22, 44, 51, 189, DateTimeKind.Utc).AddTicks(1290), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), new DateTime(2021, 11, 25, 22, 44, 51, 189, DateTimeKind.Utc).AddTicks(1270), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), new DateTime(2021, 11, 25, 22, 44, 51, 189, DateTimeKind.Utc).AddTicks(1292), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(5763), "(No feedback)", null, 5 },
                    { 1, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(4934), "Nice car", null, 4 },
                    { 2, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), new DateTime(2021, 11, 25, 22, 44, 50, 706, DateTimeKind.Utc).AddTicks(5741), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9663), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9665), 2, 240, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9649), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9652), 4, 240, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9207), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9209), 3, 240, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9162), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9166), 4, 240, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9101), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9103), 1, 240, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9091), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9094), 2, 210, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9669), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9670), 1, 240, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9658), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9659), 3, 240, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9201), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9203), 1, 240, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 120, 2, null, 1, 0m, 3 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9001), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9040), 3, 240, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9674), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9675), 3, 240, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(5914), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(7594), 2, 340, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), 90, 2, null, 2, 0m, 1 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9188), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9190), 2, 240, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), 120, 2, null, 1, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9594), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9601), 3, 240, new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9177), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9179), 4, 240, new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), 120, 2, null, 1, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9194), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9196), 3, 240, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), 120, 2, null, 1, 0m, 2 },
                    { 7, "NO EATING", new DateTime(2021, 11, 25, 22, 44, 51, 188, DateTimeKind.Utc).AddTicks(9183), new DateTime(2021, 11, 26, 0, 44, 51, 188, DateTimeKind.Local).AddTicks(9184), 2, 240, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), 120, 2, null, 1, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("8d955dd3-b878-40b4-b10d-d726c0c5287f"), "Red", new DateTime(2021, 11, 25, 22, 44, 51, 185, DateTimeKind.Utc).AddTicks(5422), 12.0, "Ferrari", null },
                    { 2, new Guid("cac7e754-af96-42d5-81eb-b06ab9b66cc5"), "Blue", new DateTime(2021, 11, 25, 22, 44, 51, 185, DateTimeKind.Utc).AddTicks(7229), 8.0, "Alfa Romeo", null },
                    { 4, new Guid("cab80604-edf2-4772-8019-9ec792ce01c5"), "Silver", new DateTime(2021, 11, 25, 22, 44, 51, 185, DateTimeKind.Utc).AddTicks(7414), 15.0, "BMW M5", null },
                    { 3, new Guid("00e9fd47-bc00-49e8-bf22-cc47ba3f491d"), "Black", new DateTime(2021, 11, 25, 22, 44, 51, 185, DateTimeKind.Utc).AddTicks(7333), 10.0, "Mercedes S Class", null }
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
