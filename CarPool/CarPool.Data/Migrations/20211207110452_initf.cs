using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class initf : Migration
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
                    { 1, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(6552), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(6759), null, false, null, "User" },
                    { 3, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(6767), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(6768), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 7, 11, 4, 50, 935, DateTimeKind.Utc).AddTicks(9972), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 12, 7, 11, 4, 50, 936, DateTimeKind.Utc).AddTicks(1163), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 12, 7, 11, 4, 50, 936, DateTimeKind.Utc).AddTicks(1193), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 12, 7, 11, 4, 50, 936, DateTimeKind.Utc).AddTicks(1195), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(1841), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2719), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2744), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2746), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2754), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2756), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2747), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2751), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2752), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2753), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2757), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(2758), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(4077), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5924), null, false, 42.1382815m, 24.7604295m, null, "bulevard Iztochen 23" },
                    { 3, 3, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5962), null, false, 43.2126824m, 27.9168517m, null, "bulevard Tsar Osvoboditel 83b" },
                    { 4, 4, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5964), null, false, 41.0403314m, 28.939206m, null, "Defterdar, Ayvansaray Cd., 34050 Eyüpsultan" },
                    { 9, 9, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5975), null, false, 41.669344m, 26.568406m, null, null },
                    { 5, 5, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5966), null, false, 37.981142m, 23.732380m, null, "Ippokratous 1" },
                    { 6, 6, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5969), null, false, 40.640014m, 22.944397m, null, null },
                    { 7, 7, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5972), null, false, 38.232467m, 21.736326m, null, null },
                    { 8, 8, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(5974), null, false, 47.151716m, 27.587696m, null, null }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 1, 2, new DateTime(2021, 12, 7, 11, 4, 50, 937, DateTimeKind.Utc).AddTicks(8831), "kalin@telerik.com", true, "Kalin", "Balimezov", null, "$2a$11$.nYYAu4S.ilhPxzLquae7OhuELHk0MyvF7HQnX.ziXDkJ3TLv4ft2", "+35920768005", "kalin" },
                    { new Guid("8c25dda0-4d74-4dd6-9726-5e0b5e3cf80a"), 1, 2, new DateTime(2021, 12, 7, 11, 4, 51, 408, DateTimeKind.Utc).AddTicks(2432), "joro@telerik.com", true, "georgi", "petrov", null, "$2a$11$QPwJO8zk/9.GnHzq7zo5a.YquAhTIEBiFI0WqTI5/K7CTbDJ05hW6", "+35920768015", "georgi" },
                    { new Guid("91bc6803-d0da-4749-aeef-5d8626fa44d1"), 1, 1, new DateTime(2021, 12, 7, 11, 4, 51, 524, DateTimeKind.Utc).AddTicks(4825), "admin@admin.com", true, "admin", "admin", null, "$2a$11$Oo.UYUJV3GZauor.j3v2FODTyjrhuIn7NqlYo5i3jn.zUJcPgwjIa", "+35920738011", "admin" },
                    { new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 2, 2, new DateTime(2021, 12, 7, 11, 4, 51, 58, DateTimeKind.Utc).AddTicks(7490), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$cH7FycBcXHdJv2MybSA72u.0aLpusO8wy6AWWcnDInvzzjVdurfXa", "+35924492877", "petio_p" },
                    { new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 3, 2, new DateTime(2021, 12, 7, 11, 4, 51, 174, DateTimeKind.Utc).AddTicks(1518), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$qUYwpTVxDN0zjwAXhR88teF3IdrDvc8H4kGNECBMpEHpvmbF2vIsa", "+35922649764", "koksal" },
                    { new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 3, 1, new DateTime(2021, 12, 7, 11, 4, 51, 289, DateTimeKind.Utc).AddTicks(9717), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$cBknGAXR6/UxqZW68yvUUuOzF/SiWa6lk64AxJQrEG3A30/RWTiYC", "+35924775508", "Tsitsibaris" },
                    { new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), 4, 2, new DateTime(2021, 12, 7, 11, 4, 51, 640, DateTimeKind.Utc).AddTicks(1527), "merkez@grece.com", true, "Carlos", "Merkez", null, "$2a$11$rH1XpmZYNkdmfHWwYSehT.wDrXnNs/KUFdreGLJOUF31wy3vygZUG", "+32920728011", "Carlitos" },
                    { new Guid("435ebf58-8c90-4e46-8bdf-ccf72c20eb1f"), 9, 2, new DateTime(2021, 12, 7, 11, 4, 51, 882, DateTimeKind.Utc).AddTicks(5312), "pewdie@yt.com", true, "Felix", "Kjellberg ", null, "$2a$11$MfbifmkZsQD4PFX3ixN7nuRWKrO.OIIAfsgYKwppMwTyuFsDyWiAm", "+3291238015", "PewDie" },
                    { new Guid("9c3c3086-944d-4d1f-9848-657161d86af7"), 9, 2, new DateTime(2021, 12, 7, 11, 4, 51, 999, DateTimeKind.Utc).AddTicks(2254), "christopher@nip.se", true, "Christopher", "Alesund", null, "$2a$11$7iUmROqYxF9rIxnwnicXeuDZmwcxjh.TRhdjTjEMlDzjkibkRbDay", "+3292233015", "Get_RighT" },
                    { new Guid("46646e15-77ae-46fa-baea-35662317fac2"), 5, 2, new DateTime(2021, 12, 7, 11, 4, 51, 762, DateTimeKind.Utc).AddTicks(6049), "ramen@aoc.com", true, "Ramos", "Enerto", null, "$2a$11$.EP5CIzL0q73DemG9c4ILe3gHkbbxXYcrujamQIOxAg0d34xrtRru", "+3292234215", "Ramen" }
                });

            migrationBuilder.InsertData(
                table: "Bans",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), null, new DateTime(2021, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 12, 7, 11, 4, 52, 117, DateTimeKind.Utc).AddTicks(6918), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3222), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3676), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3677), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 7, new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3681), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 6, new Guid("91bc6803-d0da-4749-aeef-5d8626fa44d1"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3680), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3663), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 9, new Guid("435ebf58-8c90-4e46-8bdf-ccf72c20eb1f"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3683), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 10, new Guid("9c3c3086-944d-4d1f-9848-657161d86af7"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3685), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 5, new Guid("8c25dda0-4d74-4dd6-9726-5e0b5e3cf80a"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3678), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 8, new Guid("46646e15-77ae-46fa-baea-35662317fac2"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(3682), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "IsReport", "ModifiedOn", "TripId", "Value" },
                values: new object[,]
                {
                    { 4, new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), new Guid("8c25dda0-4d74-4dd6-9726-5e0b5e3cf80a"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5719), "dirty car, good person", false, null, 4, 4 },
                    { 6, new Guid("91bc6803-d0da-4749-aeef-5d8626fa44d1"), new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5726), "safe driver", false, null, 5, 5 },
                    { 5, new Guid("8c25dda0-4d74-4dd6-9726-5e0b5e3cf80a"), new Guid("91bc6803-d0da-4749-aeef-5d8626fa44d1"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5721), "Great trip", false, null, 5, 3 },
                    { 8, new Guid("46646e15-77ae-46fa-baea-35662317fac2"), new Guid("435ebf58-8c90-4e46-8bdf-ccf72c20eb1f"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5732), "Good friend", false, null, 5, 5 },
                    { 7, new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), new Guid("46646e15-77ae-46fa-baea-35662317fac2"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5727), "Bad driver", true, null, 5, 0 },
                    { 1, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(4350), "Nice car", false, null, 1, 4 },
                    { 3, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5707), "Great trip", false, null, 3, 5 },
                    { 9, new Guid("435ebf58-8c90-4e46-8bdf-ccf72c20eb1f"), new Guid("9c3c3086-944d-4d1f-9848-657161d86af7"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5733), "Best driver", false, null, 5, 5 },
                    { 2, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(5514), "Bad person", true, null, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 13, "Good looking and friendly", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1095), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1096), 4, 240, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 120, 2, null, 1, 10.13m, 3 },
                    { 12, "NO STOPS", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1074), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1078), 3, 240, new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(795), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(796), 4, 240, new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1110), new DateTime(2021, 12, 5, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1111), 3, 240, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 120, 2, null, 1, 12.23m, 1 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(814), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(815), 3, 240, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 120, 2, null, 1, 19.11m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(730), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(731), 1, 240, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(725), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(726), 2, 210, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 110, 2, null, 2, 0m, 4 },
                    { 1, "(No comment)", new DateTime(2021, 12, 7, 11, 4, 52, 117, DateTimeKind.Utc).AddTicks(8777), new DateTime(2021, 12, 9, 13, 4, 52, 117, DateTimeKind.Local).AddTicks(9817), 2, 340, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 90, 2, null, 2, 0m, 1 },
                    { 16, "Im not alone", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1107), new DateTime(2021, 12, 5, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1108), 1, 240, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1099), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1100), 3, 240, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(810), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(811), 1, 240, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 120, 2, null, 1, 0m, 3 },
                    { 7, "NO EATING", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(799), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(800), 2, 240, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 120, 2, null, 1, 0m, 4 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(802), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(803), 2, 240, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 120, 2, null, 1, 0m, 1 },
                    { 2, "NO SMOKING", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(640), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(670), 3, 240, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 120, 2, null, 2, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(806), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(807), 3, 240, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 120, 2, null, 1, 0m, 2 },
                    { 5, "Additional comments below", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(788), new DateTime(2021, 12, 9, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(790), 4, 240, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 120, 2, null, 1, 0m, 1 },
                    { 15, "High price", new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(1103), new DateTime(2021, 12, 7, 13, 4, 52, 118, DateTimeKind.Local).AddTicks(1104), 2, 240, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 120, 2, null, 1, 15.21m, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "DeletedOn", "FuelConsumptionPerHundredKilometers", "IsDeleted", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 10, new Guid("9c3c3086-944d-4d1f-9848-657161d86af7"), "Silver", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6228), null, 16.0, false, "Mercedes-Benz S Coupe", null },
                    { 9, new Guid("435ebf58-8c90-4e46-8bdf-ccf72c20eb1f"), "Carbon Black", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6202), null, 2.0, false, "Tesla Model S", null },
                    { 5, new Guid("8c25dda0-4d74-4dd6-9726-5e0b5e3cf80a"), "Green", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6120), null, 11.0, false, "Lambo", null },
                    { 7, new Guid("bc5f8082-fd7c-4d24-b56f-cf95e4478778"), "Orange", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6166), null, 10.0, false, "Dacia", null },
                    { 6, new Guid("91bc6803-d0da-4749-aeef-5d8626fa44d1"), "Black", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6148), null, 9.0, false, "Golf4", null },
                    { 4, new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), "Silver", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6102), null, 15.0, false, "BMW M5", null },
                    { 3, new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), "Black", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6083), null, 10.0, false, "Mercedes S Class", null },
                    { 2, new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), "Blue", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6040), null, 8.0, false, "Alfa Romeo", null },
                    { 1, new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), "Red", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(4823), null, 12.0, false, "Ferrari", null },
                    { 8, new Guid("46646e15-77ae-46fa-baea-35662317fac2"), "Silver", new DateTime(2021, 12, 7, 11, 4, 52, 115, DateTimeKind.Utc).AddTicks(6184), null, 6.0, false, "BMW M5", null }
                });

            migrationBuilder.InsertData(
                table: "TripPassengers",
                columns: new[] { "ApplicationUserId", "TripId", "CreatedOn" },
                values: new object[,]
                {
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 1, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2055) },
                    { new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 1, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2502) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 8, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2511) },
                    { new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 15, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2512) },
                    { new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 15, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2513) },
                    { new Guid("a37d567a-58e0-4f85-8ef8-8096d35ae082"), 2, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2488) },
                    { new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 2, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2503) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 7, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2510) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 16, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2513) },
                    { new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 16, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2514) },
                    { new Guid("4df7963a-e527-4f54-ae34-aeb2c1671712"), 3, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2501) },
                    { new Guid("1f21aada-53b7-40c2-ab55-4c36d79dbcdb"), 3, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2505) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 3, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2506) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 4, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2507) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 5, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2507) },
                    { new Guid("19a17487-8cc6-4dbb-871b-ddba1194d713"), 6, new DateTime(2021, 12, 7, 11, 4, 52, 118, DateTimeKind.Utc).AddTicks(2509) }
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
