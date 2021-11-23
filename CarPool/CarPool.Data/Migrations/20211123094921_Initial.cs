using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class Initial : Migration
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
                    { 1, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(5845), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(6287), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(6305), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(6307), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 23, 9, 49, 20, 181, DateTimeKind.Utc).AddTicks(5554), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 23, 9, 49, 20, 181, DateTimeKind.Utc).AddTicks(8370), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 23, 9, 49, 20, 181, DateTimeKind.Utc).AddTicks(8401), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 23, 9, 49, 20, 181, DateTimeKind.Utc).AddTicks(8408), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(6770), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8489), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8531), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8534), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8551), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8554), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8536), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8545), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8547), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8549), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8556), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 23, 9, 49, 20, 183, DateTimeKind.Utc).AddTicks(8557), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(834), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(4687), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(4746), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(4751), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(4755), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), 1, 2, new DateTime(2021, 11, 23, 9, 49, 20, 185, DateTimeKind.Utc).AddTicks(2416), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$lLqp1xqhfJ2l9Lk.Tq5CeuvLYnWD4rgZB2gsz6pKY/jsoamRfAR2y", "+35920768005", "misha_m" },
                    { new Guid("55868b32-a559-4824-b40e-c6c9ad1c7c0d"), 1, 1, new DateTime(2021, 11, 23, 9, 49, 20, 799, DateTimeKind.Utc).AddTicks(526), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$t/nX4UbZJCIRtchbG5y1bOB.ggBgzJu.qPAiglIZz6pX6GXJ5ODe2", "+35924775508", "cicibar" },
                    { new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), 2, 2, new DateTime(2021, 11, 23, 9, 49, 20, 389, DateTimeKind.Utc).AddTicks(8154), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$Ct12JO0fOfmlrw5xKaaYMewQNdBthVD/OKoC748d.Jp.47AvBmxES", "+35924492877", "petio_p" },
                    { new Guid("6b86ef38-af6b-4965-89a0-c5da7d07414b"), 3, 2, new DateTime(2021, 11, 23, 9, 49, 20, 591, DateTimeKind.Utc).AddTicks(9466), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$hidWWMrCY6jldnGn6v/0BOm9NbDMydA0SPtPhs/X4yMBaJ00jf2i.", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), null, new DateTime(2021, 11, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 23, 9, 49, 20, 994, DateTimeKind.Utc).AddTicks(5633), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageData", "ImageTitle", "IsDeleted", "ModifiedOn" },
                values: new object[] { 1, new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), new DateTime(2021, 11, 23, 9, 49, 20, 995, DateTimeKind.Utc).AddTicks(3895), null, null, "(No title)", false, null });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 2, new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(9092), "Bad person", null, 1 },
                    { 3, new Guid("6b86ef38-af6b-4965-89a0-c5da7d07414b"), new Guid("55868b32-a559-4824-b40e-c6c9ad1c7c0d"), new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(9132), "(No feedback)", null, 5 },
                    { 1, new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), new DateTime(2021, 11, 23, 9, 49, 20, 184, DateTimeKind.Utc).AddTicks(7477), "Nice car", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 1, "(No comment)", new DateTime(2021, 11, 23, 9, 49, 20, 994, DateTimeKind.Utc).AddTicks(8432), new DateTime(2021, 11, 23, 11, 49, 20, 995, DateTimeKind.Local).AddTicks(653), 2, 340, new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), 90, 2, null, 2, 0m, 1 },
                    { 2, "NO SMOKEING", new DateTime(2021, 11, 23, 9, 49, 20, 995, DateTimeKind.Utc).AddTicks(2511), new DateTime(2021, 11, 23, 11, 49, 20, 995, DateTimeKind.Local).AddTicks(2555), 3, 240, new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), 120, 2, null, 1, 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("e44c9ee3-a4dd-4148-abd7-7b974ff8a6c1"), "Red", new DateTime(2021, 11, 23, 9, 49, 20, 990, DateTimeKind.Utc).AddTicks(9421), 12.0, "Ferrari", null },
                    { 4, new Guid("55868b32-a559-4824-b40e-c6c9ad1c7c0d"), "Silver", new DateTime(2021, 11, 23, 9, 49, 20, 991, DateTimeKind.Utc).AddTicks(1663), 15.0, "BMW M5", null },
                    { 2, new Guid("7e7a3f8b-5036-4510-9ba4-fba3aa5f92b7"), "Blue", new DateTime(2021, 11, 23, 9, 49, 20, 991, DateTimeKind.Utc).AddTicks(1553), 8.0, "Alfa Romeo", null },
                    { 3, new Guid("6b86ef38-af6b-4965-89a0-c5da7d07414b"), "Black", new DateTime(2021, 11, 23, 9, 49, 20, 991, DateTimeKind.Utc).AddTicks(1634), 10.0, "Mercedes S Class", null }
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
