using System;
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
                    { 1, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(8173), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(8614), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(8628), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(8630), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 26, 10, 27, 44, 671, DateTimeKind.Utc).AddTicks(21), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 26, 10, 27, 44, 671, DateTimeKind.Utc).AddTicks(2011), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 26, 10, 27, 44, 671, DateTimeKind.Utc).AddTicks(2037), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 26, 10, 27, 44, 671, DateTimeKind.Utc).AddTicks(2040), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 26, 10, 27, 44, 672, DateTimeKind.Utc).AddTicks(9640), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1292), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1335), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1337), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1351), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1354), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1340), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1346), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1348), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1349), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1356), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(1358), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(3652), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(7117), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(7178), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(7183), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(7186), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), 1, 2, new DateTime(2021, 11, 26, 10, 27, 44, 674, DateTimeKind.Utc).AddTicks(4736), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$G30vv6KZPruTxR0AAp7vFuEGdVCBSRpaM22Pnlh18..OLBIjn83hC", "+35920768005", "misha_m" },
                    { new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), 1, 1, new DateTime(2021, 11, 26, 10, 27, 45, 287, DateTimeKind.Utc).AddTicks(6908), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$otoO9AJdeatyUflmpbYFluEVsDB2KfTC/m/8aVQdYikU0kIiaPSYK", "+35924775508", "cicibar" },
                    { new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 2, 2, new DateTime(2021, 11, 26, 10, 27, 44, 879, DateTimeKind.Utc).AddTicks(9863), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$gg.q0RtQzob3YBpL28tpZuL/ve0Wq4Fj0uYTgPIjwvB5fKMeCH.du", "+35924492877", "petio_p" },
                    { new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 3, 2, new DateTime(2021, 11, 26, 10, 27, 45, 71, DateTimeKind.Utc).AddTicks(3301), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$PqzjCX0giDoA4zdbcfHOGut0Tcjx4RaL8YNBoLCB.gRYK.ZO2EpYS", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), null, new DateTime(2021, 11, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(2524), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(1362), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(2247), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(2226), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(2249), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), new DateTime(2021, 11, 26, 10, 27, 44, 674, DateTimeKind.Utc).AddTicks(1384), "(No feedback)", null, 5 },
                    { 1, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), new DateTime(2021, 11, 26, 10, 27, 44, 673, DateTimeKind.Utc).AddTicks(9709), "Nice car", null, 4 },
                    { 2, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), new DateTime(2021, 11, 26, 10, 27, 44, 674, DateTimeKind.Utc).AddTicks(1347), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(235), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(237), 2, 240, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(218), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(221), 4, 240, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9674), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9677), 3, 240, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9625), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9627), 4, 240, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9618), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9620), 1, 240, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9610), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9613), 2, 210, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(241), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(243), 1, 240, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(228), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(230), 3, 240, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9668), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9670), 1, 240, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 120, 2, null, 1, 0m, 3 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9515), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9558), 3, 240, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(247), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(249), 3, 240, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(5474), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(7706), 2, 340, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), 90, 2, null, 2, 0m, 1 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9654), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9656), 2, 240, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), 120, 2, null, 1, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 26, 10, 27, 45, 507, DateTimeKind.Utc).AddTicks(147), new DateTime(2021, 11, 26, 12, 27, 45, 507, DateTimeKind.Local).AddTicks(158), 3, 240, new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9641), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9643), 4, 240, new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), 120, 2, null, 1, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9660), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9662), 3, 240, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), 120, 2, null, 1, 0m, 2 },
                    { 7, "NO EATING", new DateTime(2021, 11, 26, 10, 27, 45, 506, DateTimeKind.Utc).AddTicks(9648), new DateTime(2021, 11, 26, 12, 27, 45, 506, DateTimeKind.Local).AddTicks(9650), 2, 240, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), 120, 2, null, 1, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("5e356646-097c-40f9-b324-9eb8da7dc391"), "Red", new DateTime(2021, 11, 26, 10, 27, 45, 502, DateTimeKind.Utc).AddTicks(7412), 12.0, "Ferrari", null },
                    { 2, new Guid("1cade753-8bc4-4264-a2dd-3b35631c4d14"), "Blue", new DateTime(2021, 11, 26, 10, 27, 45, 502, DateTimeKind.Utc).AddTicks(9518), 8.0, "Alfa Romeo", null },
                    { 4, new Guid("afec1c2d-29ee-4947-910f-8928014f6843"), "Silver", new DateTime(2021, 11, 26, 10, 27, 45, 502, DateTimeKind.Utc).AddTicks(9645), 15.0, "BMW M5", null },
                    { 3, new Guid("969c18d1-dfa2-4f6a-990f-318e4a8dc3b1"), "Black", new DateTime(2021, 11, 26, 10, 27, 45, 502, DateTimeKind.Utc).AddTicks(9616), 10.0, "Mercedes S Class", null }
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
