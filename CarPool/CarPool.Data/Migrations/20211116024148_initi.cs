using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class initi : Migration
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
                    Username = table.Column<string>(maxLength: 20, nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
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
                    Color = table.Column<string>(nullable: true)
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
                    { 1, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(1194), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(1616), null, false, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 16, 2, 41, 47, 923, DateTimeKind.Utc).AddTicks(1857), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 16, 2, 41, 47, 923, DateTimeKind.Utc).AddTicks(3754), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 16, 2, 41, 47, 923, DateTimeKind.Utc).AddTicks(3784), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 16, 2, 41, 47, 923, DateTimeKind.Utc).AddTicks(3786), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(2422), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4130), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4171), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4175), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4288), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4292), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4177), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4187), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4281), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4286), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4294), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(4296), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 16, 2, 41, 47, 925, DateTimeKind.Utc).AddTicks(6531), null, false, 42.698334m, 23.319941m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(52), null, false, 42.682073m, 23.326622m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(106), null, false, 42.698334m, 23.254942m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(111), null, false, 42.711242m, 23.316655m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(115), null, false, 42.625045m, 23.400539m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("d67df7e7-e98f-44ca-8454-74f5b418fbf1"), 1, 2, new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(7481), "mishkov@misho.com", true, "Misho", "Mishkov", null, "12345678", "+35920768005", "misha_m" },
                    { new Guid("1d0bceb5-f491-456b-9571-44839e439a6d"), 2, 2, new DateTime(2021, 11, 16, 2, 41, 47, 927, DateTimeKind.Utc).AddTicks(3114), "petio@mvc.net", true, "Peter", "Petrov", null, "123456789", "+35924492877", "petio_p" },
                    { new Guid("2c77ba4c-0a99-489b-9d27-e08184857f85"), 3, 2, new DateTime(2021, 11, 16, 2, 41, 47, 927, DateTimeKind.Utc).AddTicks(3199), "koksal@asd.tr", true, "Koksal", "Baba", null, "1234567899", "+35922649764", "koksal" },
                    { new Guid("6dcb348d-1974-4ef8-a35d-0b40d9392c5c"), 4, 2, new DateTime(2021, 11, 16, 2, 41, 47, 927, DateTimeKind.Utc).AddTicks(3206), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "12345678999", "+35924775508", "cicibar" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("1d0bceb5-f491-456b-9571-44839e439a6d"), null, new DateTime(2021, 11, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 16, 2, 41, 47, 930, DateTimeKind.Utc).AddTicks(4914), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageData", "ImageTitle", "IsDeleted", "ModifiedOn" },
                values: new object[] { 1, new Guid("d67df7e7-e98f-44ca-8454-74f5b418fbf1"), new DateTime(2021, 11, 16, 2, 41, 47, 931, DateTimeKind.Utc).AddTicks(2099), null, null, "(No title)", false, null });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 2, new Guid("1d0bceb5-f491-456b-9571-44839e439a6d"), new Guid("d67df7e7-e98f-44ca-8454-74f5b418fbf1"), new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(4298), "Bad person", null, 1 },
                    { 1, new Guid("d67df7e7-e98f-44ca-8454-74f5b418fbf1"), new Guid("1d0bceb5-f491-456b-9571-44839e439a6d"), new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(2736), "Nice car", null, 4 },
                    { 3, new Guid("2c77ba4c-0a99-489b-9d27-e08184857f85"), new Guid("6dcb348d-1974-4ef8-a35d-0b40d9392c5c"), new DateTime(2021, 11, 16, 2, 41, 47, 926, DateTimeKind.Utc).AddTicks(4338), "(No feedback)", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "ArrivalTime", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 1, "(No comment)", new DateTime(2021, 11, 16, 7, 41, 47, 930, DateTimeKind.Local).AddTicks(9199), new DateTime(2021, 11, 16, 2, 41, 47, 930, DateTimeKind.Utc).AddTicks(6653), new DateTime(2021, 11, 16, 4, 41, 47, 930, DateTimeKind.Local).AddTicks(8739), 2, 340.0, new Guid("d67df7e7-e98f-44ca-8454-74f5b418fbf1"), 2, null, 2, 0m, 1 },
                    { 2, "NO SMOKEING", new DateTime(2021, 11, 16, 6, 41, 47, 931, DateTimeKind.Local).AddTicks(877), new DateTime(2021, 11, 16, 2, 41, 47, 931, DateTimeKind.Utc).AddTicks(808), new DateTime(2021, 11, 16, 4, 41, 47, 931, DateTimeKind.Local).AddTicks(854), 3, 240.0, new Guid("1d0bceb5-f491-456b-9571-44839e439a6d"), 2, null, 1, 0m, 2 }
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
