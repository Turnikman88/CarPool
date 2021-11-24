using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class init : Migration
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
                    StreetName = table.Column<string>(nullable: false),
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
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
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
                    { 1, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(4445), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(4673), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(4683), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(4684), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 24, 11, 27, 13, 125, DateTimeKind.Utc).AddTicks(5777), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 24, 11, 27, 13, 125, DateTimeKind.Utc).AddTicks(7278), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 24, 11, 27, 13, 125, DateTimeKind.Utc).AddTicks(7311), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 24, 11, 27, 13, 125, DateTimeKind.Utc).AddTicks(7314), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 24, 11, 27, 13, 126, DateTimeKind.Utc).AddTicks(8749), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 24, 11, 27, 13, 126, DateTimeKind.Utc).AddTicks(9981), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(15), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(17), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(26), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(28), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(18), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(22), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(24), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(25), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(29), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(30), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(1611), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(3556), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(3598), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(3601), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(3652), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("edead910-0112-4b2a-92af-79c6516d3382"), 1, 2, new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(8127), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$dzJIfvtkiYbCDwso8uN7s.62ru8iBRWQliUKJQtD7nkejKUnAeham", "+35920768005", "misha_m" },
                    { new Guid("a3d60a17-4007-416b-bd94-2bc980efcfbe"), 1, 1, new DateTime(2021, 11, 24, 11, 27, 13, 489, DateTimeKind.Utc).AddTicks(3179), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$zemFgb4MRS.lkcJR1lXPIepvplETwP8f36CG8YdoikCJE6TzXpp4W", "+35924775508", "cicibar" },
                    { new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 2, 2, new DateTime(2021, 11, 24, 11, 27, 13, 255, DateTimeKind.Utc).AddTicks(7494), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$FIFffYM3K79ArNQ7.AOA.uWVPeh.mTV6JBaqo0Deq8hDmLP3q6KrS", "+35924492877", "petio_p" },
                    { new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 3, 2, new DateTime(2021, 11, 24, 11, 27, 13, 373, DateTimeKind.Utc).AddTicks(9563), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$OLunDqCQd48Lynx9R4984.A4DWLYZkXzh5d15WpiTh55ji3KSG...", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), null, new DateTime(2021, 11, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(1230), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageData", "ImageTitle", "IsDeleted", "ModifiedOn" },
                values: new object[] { 1, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5897), null, null, "(No title)", false, null });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), new Guid("a3d60a17-4007-416b-bd94-2bc980efcfbe"), new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(6186), "(No feedback)", null, 5 },
                    { 1, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(5352), "Nice car", null, 4 },
                    { 2, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), new Guid("edead910-0112-4b2a-92af-79c6516d3382"), new DateTime(2021, 11, 24, 11, 27, 13, 127, DateTimeKind.Utc).AddTicks(6161), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4822), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4823), 3, 240, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), 120, 2, null, 1, 0m, 2 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5111), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5112), 4, 240, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4830), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4831), 3, 240, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4806), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4807), 4, 240, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4802), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4803), 1, 240, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4798), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4799), 2, 210, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5122), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5123), 1, 240, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5115), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5116), 3, 240, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4826), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4828), 1, 240, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 120, 2, null, 1, 0m, 3 },
                    { 7, "NO EATING", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4815), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4816), 2, 240, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 120, 2, null, 1, 0m, 4 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4739), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4767), 3, 240, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5125), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5126), 3, 240, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), 120, 2, null, 1, 12.23m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5072), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5077), 3, 240, new Guid("a3d60a17-4007-416b-bd94-2bc980efcfbe"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4812), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4813), 4, 240, new Guid("a3d60a17-4007-416b-bd94-2bc980efcfbe"), 120, 2, null, 1, 0m, 2 },
                    { 1, "(No comment)", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(2884), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(3915), 2, 340, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), 90, 2, null, 2, 0m, 1 },
                    { 15, "High price", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(5118), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(5119), 2, 240, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), 120, 2, null, 1, 15.21m, 3 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 24, 11, 27, 13, 607, DateTimeKind.Utc).AddTicks(4819), new DateTime(2021, 11, 24, 13, 27, 13, 607, DateTimeKind.Local).AddTicks(4820), 2, 240, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), 120, 2, null, 1, 0m, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 4, new Guid("a3d60a17-4007-416b-bd94-2bc980efcfbe"), "Silver", new DateTime(2021, 11, 24, 11, 27, 13, 605, DateTimeKind.Utc).AddTicks(2421), 15.0, "BMW M5", null },
                    { 2, new Guid("fc7aa9d3-9abe-4f51-97de-5623ddddb844"), "Blue", new DateTime(2021, 11, 24, 11, 27, 13, 605, DateTimeKind.Utc).AddTicks(2332), 8.0, "Alfa Romeo", null },
                    { 1, new Guid("edead910-0112-4b2a-92af-79c6516d3382"), "Red", new DateTime(2021, 11, 24, 11, 27, 13, 605, DateTimeKind.Utc).AddTicks(1234), 12.0, "Ferrari", null },
                    { 3, new Guid("bf0b1c0d-9727-452b-9f0c-32d89febd9d5"), "Black", new DateTime(2021, 11, 24, 11, 27, 13, 605, DateTimeKind.Utc).AddTicks(2401), 10.0, "Mercedes S Class", null }
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
