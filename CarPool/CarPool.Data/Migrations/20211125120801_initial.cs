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
                    { 1, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(3807), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(4420), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(4448), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(4451), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 25, 12, 7, 59, 974, DateTimeKind.Utc).AddTicks(92), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 25, 12, 7, 59, 974, DateTimeKind.Utc).AddTicks(3045), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 25, 12, 7, 59, 974, DateTimeKind.Utc).AddTicks(3092), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 25, 12, 7, 59, 974, DateTimeKind.Utc).AddTicks(3099), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(919), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3414), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3483), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3585), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3620), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3627), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3591), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3608), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3612), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3616), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3631), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(3635), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 12, 7, 59, 977, DateTimeKind.Utc).AddTicks(7060), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(2168), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(2268), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(2280), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(2287), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), 1, 2, new DateTime(2021, 11, 25, 12, 7, 59, 979, DateTimeKind.Utc).AddTicks(3255), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$ldqfKBXtSVfuBzA3Xv93KexvLQzryFNYy0KBuUe4a4vh.XPIL89wC", "+35920768005", "misha_m" },
                    { new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), 1, 1, new DateTime(2021, 11, 25, 12, 8, 0, 613, DateTimeKind.Utc).AddTicks(5284), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$ZVhMxCi.kX1cmt8HyYBtYeLW6FzZIQ3ZVQ7s/d84mUatZuVfHUgeq", "+35924775508", "cicibar" },
                    { new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 2, 2, new DateTime(2021, 11, 25, 12, 8, 0, 217, DateTimeKind.Utc).AddTicks(5589), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$LdW158Lrgf3/7LBxMlGZ.u1e2Z1FE6NK7s4HlMHLjgH70LrLDS47K", "+35924492877", "petio_p" },
                    { new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 3, 2, new DateTime(2021, 11, 25, 12, 8, 0, 421, DateTimeKind.Utc).AddTicks(3621), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$stQFFJ3XvVgdt9.sQQ9g5.rnuGsRZIW9yI5vNmETKZFGLesL2TTsy", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), null, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 25, 12, 8, 0, 830, DateTimeKind.Utc).AddTicks(5670), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(4542), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(5451), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(5425), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(5453), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(8369), "(No feedback)", null, 5 },
                    { 1, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(6026), "Nice car", null, 4 },
                    { 2, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), new DateTime(2021, 11, 25, 12, 7, 59, 978, DateTimeKind.Utc).AddTicks(8311), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3326), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3328), 2, 240, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3311), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3314), 4, 240, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2766), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2768), 3, 240, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2719), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2722), 4, 240, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2713), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2715), 1, 240, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2700), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2705), 2, 210, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3332), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3334), 1, 240, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3320), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3322), 3, 240, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2760), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2762), 1, 240, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 120, 2, null, 1, 0m, 3 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2570), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2614), 3, 240, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3339), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3404), 3, 240, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 11, 25, 12, 8, 0, 830, DateTimeKind.Utc).AddTicks(8646), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(812), 2, 340, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), 90, 2, null, 2, 0m, 1 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2746), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2748), 2, 240, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), 120, 2, null, 1, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(3269), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(3280), 3, 240, new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2734), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2736), 4, 240, new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), 120, 2, null, 1, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2752), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2754), 3, 240, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), 120, 2, null, 1, 0m, 2 },
                    { 7, "NO EATING", new DateTime(2021, 11, 25, 12, 8, 0, 831, DateTimeKind.Utc).AddTicks(2740), new DateTime(2021, 11, 25, 14, 8, 0, 831, DateTimeKind.Local).AddTicks(2742), 2, 240, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), 120, 2, null, 1, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("3c9b5865-7bc7-4a62-91e8-a5cff349c252"), "Red", new DateTime(2021, 11, 25, 12, 8, 0, 827, DateTimeKind.Utc).AddTicks(4), 12.0, "Ferrari", null },
                    { 2, new Guid("b1b7dd0b-a7c2-45a6-ba69-c9d54a6f8c8b"), "Blue", new DateTime(2021, 11, 25, 12, 8, 0, 827, DateTimeKind.Utc).AddTicks(2087), 8.0, "Alfa Romeo", null },
                    { 4, new Guid("28868abc-172a-488d-9b03-4a05e01d0128"), "Silver", new DateTime(2021, 11, 25, 12, 8, 0, 827, DateTimeKind.Utc).AddTicks(2194), 15.0, "BMW M5", null },
                    { 3, new Guid("68f1e53a-0c23-4c4c-a41d-7e0d107b15a8"), "Black", new DateTime(2021, 11, 25, 12, 8, 0, 827, DateTimeKind.Utc).AddTicks(2164), 10.0, "Mercedes S Class", null }
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
