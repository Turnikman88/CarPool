using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class initiale : Migration
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
                    { 1, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8796), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8982), null, false, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 18, 22, 10, 46, 707, DateTimeKind.Utc).AddTicks(2121), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 18, 22, 10, 46, 707, DateTimeKind.Utc).AddTicks(3041), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 18, 22, 10, 46, 707, DateTimeKind.Utc).AddTicks(3060), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 18, 22, 10, 46, 707, DateTimeKind.Utc).AddTicks(3062), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(4314), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5045), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5070), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5072), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5089), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5142), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5078), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5083), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5085), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5087), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5143), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(5145), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(6606), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8054), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8094), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8097), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(8099), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), 1, 2, new DateTime(2021, 11, 18, 22, 10, 46, 709, DateTimeKind.Utc).AddTicks(2392), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$YTEEQfpkZtrTRgH/ljHRG.BlOxbwV7L3gol.7Z0aGD5BK.jRCgMca", "+35920768005", "misha_m" },
                    { new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), 2, 2, new DateTime(2021, 11, 18, 22, 10, 46, 848, DateTimeKind.Utc).AddTicks(2077), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$QY5WoNy84nqS7GFhA7PaEeQwiupFMqi7nQ9GY9VeZybbofC79egSS", "+35924492877", "petio_p" },
                    { new Guid("ae4965ed-62fa-45c9-aaf9-336a6f9a3f7e"), 3, 2, new DateTime(2021, 11, 18, 22, 10, 46, 980, DateTimeKind.Utc).AddTicks(1096), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$wQuR9XuihNZCriYmnckfD.iDh8VlnrHJL3gBhBrjjTSp5Tk8iRr9y", "+35922649764", "koksal" },
                    { new Guid("bd3cc1c1-0a6a-4446-8165-89584d2b5af0"), 4, 2, new DateTime(2021, 11, 18, 22, 10, 47, 111, DateTimeKind.Utc).AddTicks(8969), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$Anv8VN28HX.GhWwylPlp3u6gpb8WVD9ODVgY8DfnwUi40.mc8lSIi", "+35924775508", "cicibar" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), null, new DateTime(2021, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 18, 22, 10, 47, 252, DateTimeKind.Utc).AddTicks(3434), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageData", "ImageTitle", "IsDeleted", "ModifiedOn" },
                values: new object[] { 1, new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), new DateTime(2021, 11, 18, 22, 10, 47, 252, DateTimeKind.Utc).AddTicks(8160), null, null, "(No title)", false, null });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 2, new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), new DateTime(2021, 11, 18, 22, 10, 46, 709, DateTimeKind.Utc).AddTicks(323), "Bad person", null, 1 },
                    { 1, new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), new DateTime(2021, 11, 18, 22, 10, 46, 708, DateTimeKind.Utc).AddTicks(9665), "Nice car", null, 4 },
                    { 3, new Guid("ae4965ed-62fa-45c9-aaf9-336a6f9a3f7e"), new Guid("bd3cc1c1-0a6a-4446-8165-89584d2b5af0"), new DateTime(2021, 11, 18, 22, 10, 46, 709, DateTimeKind.Utc).AddTicks(349), "(No feedback)", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 1, "(No comment)", new DateTime(2021, 11, 18, 22, 10, 47, 252, DateTimeKind.Utc).AddTicks(5796), new DateTime(2021, 11, 19, 0, 10, 47, 252, DateTimeKind.Local).AddTicks(6680), 2, 340, new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), 90, 2, null, 2, 0m, 1 },
                    { 2, "NO SMOKEING", new DateTime(2021, 11, 18, 22, 10, 47, 252, DateTimeKind.Utc).AddTicks(7311), new DateTime(2021, 11, 19, 0, 10, 47, 252, DateTimeKind.Local).AddTicks(7339), 3, 240, new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), 120, 2, null, 1, 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("9b1ddbb6-e853-46df-850b-0b901861a125"), "Red", new DateTime(2021, 11, 18, 22, 10, 47, 249, DateTimeKind.Utc).AddTicks(6849), 12.0, "Ferrari", null },
                    { 2, new Guid("efba965d-6d26-467e-9169-6b4a4f26187e"), "Blue", new DateTime(2021, 11, 18, 22, 10, 47, 250, DateTimeKind.Utc).AddTicks(568), 8.0, "Alfa Romeo", null },
                    { 3, new Guid("ae4965ed-62fa-45c9-aaf9-336a6f9a3f7e"), "Black", new DateTime(2021, 11, 18, 22, 10, 47, 250, DateTimeKind.Utc).AddTicks(765), 10.0, "Mercedes S Class", null },
                    { 4, new Guid("bd3cc1c1-0a6a-4446-8165-89584d2b5af0"), "Silver", new DateTime(2021, 11, 18, 22, 10, 47, 250, DateTimeKind.Utc).AddTicks(843), 15.0, "BMW M5", null }
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
