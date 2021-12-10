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
                name: "Bans",
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
                    table.PrimaryKey("PK_Bans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bans_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inboxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inboxes_ApplicationUsers_ApplicationUserId",
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
                    { 1, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(8051), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(8278), null, false, null, "User" },
                    { 3, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(8286), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(8288), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 10, 13, 29, 12, 96, DateTimeKind.Utc).AddTicks(218), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 12, 10, 13, 29, 12, 96, DateTimeKind.Utc).AddTicks(1392), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 12, 10, 13, 29, 12, 96, DateTimeKind.Utc).AddTicks(1412), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 12, 10, 13, 29, 12, 96, DateTimeKind.Utc).AddTicks(1414), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(2564), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3488), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3513), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3515), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3526), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3528), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3517), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3521), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3523), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3524), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3530), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(3531), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(5184), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7350), null, false, 42.1382815m, 24.7604295m, null, "bulevard Iztochen 23" },
                    { 3, 3, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7389), null, false, 43.2126824m, 27.9168517m, null, "bulevard Tsar Osvoboditel 83b" },
                    { 4, 4, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7392), null, false, 41.0403314m, 28.939206m, null, "Defterdar, Ayvansaray Cd., 34050 Eyüpsultan" },
                    { 9, 9, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7403), null, false, 41.669344m, 26.568406m, null, null },
                    { 5, 5, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7394), null, false, 37.981142m, 23.732380m, null, "Ippokratous 1" },
                    { 6, 6, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7397), null, false, 40.640014m, 22.944397m, null, null },
                    { 7, 7, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7399), null, false, 38.232467m, 21.736326m, null, null },
                    { 8, 8, new DateTime(2021, 12, 10, 13, 29, 12, 97, DateTimeKind.Utc).AddTicks(7401), null, false, 47.151716m, 27.587696m, null, null }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 1, 2, new DateTime(2021, 12, 10, 13, 29, 12, 98, DateTimeKind.Utc).AddTicks(579), "kalin@telerik.com", true, "Kalin", "Balimezov", null, "$2a$11$/GxNc841gGq2iT57lT1XWu9piehx8Di//OiQ/.93HnOSLVGPNRzOi", "+35920768005", "kalin" },
                    { new Guid("60f19e07-2473-4533-8f00-d7c8f5dd8fa3"), 1, 2, new DateTime(2021, 12, 10, 13, 29, 12, 566, DateTimeKind.Utc).AddTicks(3616), "joro@telerik.com", true, "georgi", "petrov", null, "$2a$11$q6zhXX/6BeKAKV1pNolPSOouwffp3hm0AEnmHYnOeXDhedEA3qjA.", "+35920768015", "georgi" },
                    { new Guid("4128f5ba-d1df-4d45-9a23-001e1fb79b02"), 1, 1, new DateTime(2021, 12, 10, 13, 29, 12, 683, DateTimeKind.Utc).AddTicks(6255), "admin@admin.com", true, "admin", "admin", null, "$2a$11$cu888TZita15heEwpoM21.E4CfhzWF06VGUdLIYooU67qsAad1qI.", "+35920738011", "admin" },
                    { new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 2, 2, new DateTime(2021, 12, 10, 13, 29, 12, 220, DateTimeKind.Utc).AddTicks(8397), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$be2NgGfz.37bFdDqWfRe8.3vi2jw7X6zN8qkTIp3F47jIqePvlzvG", "+35924492877", "petio_p" },
                    { new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 3, 2, new DateTime(2021, 12, 10, 13, 29, 12, 335, DateTimeKind.Utc).AddTicks(9961), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$zQBfF3EUcMtvA0MITD1az.lKUk59pqLNtwS/mwBfaPTaYqJrCpnki", "+35922649764", "koksal" },
                    { new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 3, 1, new DateTime(2021, 12, 10, 13, 29, 12, 451, DateTimeKind.Utc).AddTicks(1590), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$fKZzUGLhKXB/ess2fl6oSOT8WnfZzlIPatz0lDPdXAQFh0NoDfZCm", "+35924775508", "Tsitsibaris" },
                    { new Guid("22c78551-a84e-49c4-b705-c615698ac610"), 4, 2, new DateTime(2021, 12, 10, 13, 29, 12, 803, DateTimeKind.Utc).AddTicks(1387), "merkez@grece.com", true, "Carlos", "Merkez", null, "$2a$11$LyGJ2Y/zPNKKBOoKsmRA6.TLe1Qin.c4S5Ijtju2MU6fcs0AJvtEu", "+32920728011", "Carlitos" },
                    { new Guid("ea3aa14d-2240-430e-b29e-54df0c920b4c"), 9, 2, new DateTime(2021, 12, 10, 13, 29, 13, 44, DateTimeKind.Utc).AddTicks(8827), "pewdie@yt.com", true, "Felix", "Kjellberg ", null, "$2a$11$qSbJwE8PPepClvUpyC.FB.LQD4GO5d1dz8mTiwtRT6bnE13zuX9Au", "+3291238015", "PewDie" },
                    { new Guid("773e6f71-e337-4522-b1c2-d27f0e2b8ae0"), 9, 2, new DateTime(2021, 12, 10, 13, 29, 13, 165, DateTimeKind.Utc).AddTicks(5502), "christopher@nip.se", true, "Christopher", "Alesund", null, "$2a$11$R83Ci5qGuzeOV9kzpH7GROZX2VUBHKlRlSUrv7wGCegoRuGfWEDXy", "+3292233015", "Get_RighT" },
                    { new Guid("4c2c484f-909a-401a-9237-1b073af6be85"), 5, 2, new DateTime(2021, 12, 10, 13, 29, 12, 925, DateTimeKind.Utc).AddTicks(348), "ramen@aoc.com", true, "Ramos", "Enerto", null, "$2a$11$ZPzZr6We8iY.d2ROIodq0.FbZZIJnHt5X36Y8IhZhJqdPb5SUtx4K", "+3292234215", "Ramen" },
                    { new Guid("fb66c8d5-a994-42eb-a5be-6f606f95736a"), 8, 2, new DateTime(2021, 12, 10, 13, 29, 13, 284, DateTimeKind.Utc).AddTicks(5959), "Kiro123$", true, "Kiril", "Stanoev", null, "$2a$11$X/Yp3aFHo79rO5.7JUFVDuYYqmJWBtROlVCMQCp0c5uMxWjRt31mu", "+3298883015", "KiroVijan" }
                });

            migrationBuilder.InsertData(
                table: "Bans",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("22c78551-a84e-49c4-b705-c615698ac610"), null, new DateTime(2021, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 12, 10, 13, 29, 13, 405, DateTimeKind.Utc).AddTicks(8549), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5023), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5594), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5595), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 7, new Guid("22c78551-a84e-49c4-b705-c615698ac610"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5599), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 6, new Guid("4128f5ba-d1df-4d45-9a23-001e1fb79b02"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5598), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5578), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 9, new Guid("ea3aa14d-2240-430e-b29e-54df0c920b4c"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5601), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 10, new Guid("773e6f71-e337-4522-b1c2-d27f0e2b8ae0"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5602), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 5, new Guid("60f19e07-2473-4533-8f00-d7c8f5dd8fa3"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5596), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 8, new Guid("4c2c484f-909a-401a-9237-1b073af6be85"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(5600), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "IsReport", "ModifiedOn", "TripId", "Value" },
                values: new object[,]
                {
                    { 4, new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), new Guid("60f19e07-2473-4533-8f00-d7c8f5dd8fa3"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7665), "dirty car, good person", false, null, 4, 4 },
                    { 6, new Guid("4128f5ba-d1df-4d45-9a23-001e1fb79b02"), new Guid("22c78551-a84e-49c4-b705-c615698ac610"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7671), "safe driver", false, null, 5, 5 },
                    { 5, new Guid("60f19e07-2473-4533-8f00-d7c8f5dd8fa3"), new Guid("4128f5ba-d1df-4d45-9a23-001e1fb79b02"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7666), "Great trip", false, null, 5, 3 },
                    { 8, new Guid("4c2c484f-909a-401a-9237-1b073af6be85"), new Guid("ea3aa14d-2240-430e-b29e-54df0c920b4c"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7678), "Good friend", false, null, 5, 5 },
                    { 7, new Guid("22c78551-a84e-49c4-b705-c615698ac610"), new Guid("4c2c484f-909a-401a-9237-1b073af6be85"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7672), "Bad driver", true, null, 5, 0 },
                    { 1, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(6276), "Nice car", false, null, 1, 4 },
                    { 3, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7653), "Great trip", false, null, 3, 5 },
                    { 9, new Guid("ea3aa14d-2240-430e-b29e-54df0c920b4c"), new Guid("773e6f71-e337-4522-b1c2-d27f0e2b8ae0"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7679), "Best driver", false, null, 5, 5 },
                    { 2, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), new Guid("1e85174b-c240-4120-805b-9eae89e53161"), new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(7462), "Bad person", true, null, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 13, "Good looking and friendly", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2780), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2781), 4, 240, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 120, 2, null, 1, 10.13m, 3 },
                    { 12, "NO STOPS", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2710), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2762), 3, 240, new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2447), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2449), 4, 240, new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2795), new DateTime(2021, 12, 8, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2796), 3, 240, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 120, 2, null, 1, 12.23m, 1 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2466), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2467), 3, 240, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 120, 2, null, 1, 19.11m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2437), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2438), 1, 240, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2432), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2434), 2, 210, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 110, 2, null, 2, 0m, 4 },
                    { 1, "(No comment)", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(474), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(1548), 2, 340, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 90, 2, null, 2, 0m, 1 },
                    { 16, "Im not alone", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2791), new DateTime(2021, 12, 8, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2792), 1, 240, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2784), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2785), 3, 240, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2462), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2464), 1, 240, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 120, 2, null, 1, 0m, 3 },
                    { 7, "NO EATING", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2451), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2452), 2, 240, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 120, 2, null, 1, 0m, 4 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2455), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2456), 2, 240, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 120, 2, null, 1, 0m, 1 },
                    { 2, "NO SMOKING", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2352), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2399), 3, 240, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 120, 2, null, 2, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2458), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2459), 3, 240, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 120, 2, null, 1, 0m, 2 },
                    { 5, "Additional comments below", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2441), new DateTime(2021, 12, 12, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2442), 4, 240, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 120, 2, null, 1, 0m, 1 },
                    { 15, "High price", new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(2787), new DateTime(2021, 12, 10, 15, 29, 13, 406, DateTimeKind.Local).AddTicks(2788), 2, 240, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 120, 2, null, 1, 15.21m, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "DeletedOn", "FuelConsumptionPerHundredKilometers", "IsDeleted", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 10, new Guid("773e6f71-e337-4522-b1c2-d27f0e2b8ae0"), "Silver", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5510), null, 16.0, false, "Mercedes-Benz S Coupe", null },
                    { 9, new Guid("ea3aa14d-2240-430e-b29e-54df0c920b4c"), "Carbon Black", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5486), null, 2.0, false, "Tesla Model S", null },
                    { 5, new Guid("60f19e07-2473-4533-8f00-d7c8f5dd8fa3"), "Green", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5393), null, 11.0, false, "Lambo", null },
                    { 7, new Guid("22c78551-a84e-49c4-b705-c615698ac610"), "Orange", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5440), null, 10.0, false, "Dacia", null },
                    { 6, new Guid("4128f5ba-d1df-4d45-9a23-001e1fb79b02"), "Black", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5421), null, 9.0, false, "Golf4", null },
                    { 4, new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), "Silver", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5375), null, 15.0, false, "BMW M5", null },
                    { 3, new Guid("1e85174b-c240-4120-805b-9eae89e53161"), "Black", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5356), null, 10.0, false, "Mercedes S Class", null },
                    { 2, new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), "Blue", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5308), null, 8.0, false, "Alfa Romeo", null },
                    { 1, new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), "Red", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(4107), null, 12.0, false, "Ferrari", null },
                    { 8, new Guid("4c2c484f-909a-401a-9237-1b073af6be85"), "Silver", new DateTime(2021, 12, 10, 13, 29, 13, 403, DateTimeKind.Utc).AddTicks(5457), null, 6.0, false, "BMW M5", null }
                });

            migrationBuilder.InsertData(
                table: "TripPassengers",
                columns: new[] { "ApplicationUserId", "TripId", "CreatedOn" },
                values: new object[,]
                {
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 1, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(3749) },
                    { new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 1, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4264) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 8, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4273) },
                    { new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 15, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4273) },
                    { new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 15, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4275) },
                    { new Guid("ede21b4c-a93d-461e-bd0d-6bad5e8ede5f"), 2, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4248) },
                    { new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 2, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4265) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 7, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4272) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 16, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4274) },
                    { new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 16, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4277) },
                    { new Guid("1e85174b-c240-4120-805b-9eae89e53161"), 3, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4263) },
                    { new Guid("2de14c4e-99c5-4d56-a1be-ce0eeb40d879"), 3, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4267) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 3, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4268) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 4, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4269) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 5, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4269) },
                    { new Guid("910ad796-caf7-4c98-8f81-0d75cc69414f"), 6, new DateTime(2021, 12, 10, 13, 29, 13, 406, DateTimeKind.Utc).AddTicks(4271) }
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
                name: "IX_Bans_ApplicationUserId",
                table: "Bans",
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
                name: "IX_Inboxes_ApplicationUserId",
                table: "Inboxes",
                column: "ApplicationUserId");

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
                name: "Bans");

            migrationBuilder.DropTable(
                name: "GoogleAccount");

            migrationBuilder.DropTable(
                name: "Inboxes");

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
