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
                    { 1, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(5566), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(5992), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(6005), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(6007), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 25, 8, 46, 9, 691, DateTimeKind.Utc).AddTicks(8383), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 25, 8, 46, 9, 692, DateTimeKind.Utc).AddTicks(285), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 25, 8, 46, 9, 692, DateTimeKind.Utc).AddTicks(315), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 25, 8, 46, 9, 692, DateTimeKind.Utc).AddTicks(317), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(7322), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8867), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8908), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8912), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8929), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8932), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8914), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8923), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8925), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8927), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8934), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 25, 8, 46, 9, 693, DateTimeKind.Utc).AddTicks(8936), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(989), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 2, 2, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(4479), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 3, 3, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(4538), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 4, 4, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(4543), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 5, 5, new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(4546), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), 1, 2, new DateTime(2021, 11, 25, 8, 46, 9, 695, DateTimeKind.Utc).AddTicks(1932), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$x3yqGyD78v4iwFV.ZKT08ODzNQglXFENp.7vBSGS6nnUsp7r9OwGG", "+35920768005", "misha_m" },
                    { new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), 1, 1, new DateTime(2021, 11, 25, 8, 46, 10, 280, DateTimeKind.Utc).AddTicks(9850), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$zNUmT7mAUt44ugXmP.BiReSeD5gxH3SKdlSvV3aPw6teqfOJMr1i2", "+35924775508", "cicibar" },
                    { new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 2, 2, new DateTime(2021, 11, 25, 8, 46, 9, 889, DateTimeKind.Utc).AddTicks(100), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$yhEriLi0Be/19MynB8YBxu58mCeZmC/ceTyXcLcCaHrWWa0t6QVB.", "+35924492877", "petio_p" },
                    { new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 3, 2, new DateTime(2021, 11, 25, 8, 46, 10, 91, DateTimeKind.Utc).AddTicks(4834), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$3s22j5fpjIPV38MCBZKZueWbCWfk9X851tJSztx5YRBxLWYY6wjPC", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), null, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 25, 8, 46, 10, 502, DateTimeKind.Utc).AddTicks(6035), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(4440), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(5277), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(5257), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(5279), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(8755), "(No feedback)", null, 5 },
                    { 1, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(7149), "Nice car", null, 4 },
                    { 2, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), new DateTime(2021, 11, 25, 8, 46, 9, 694, DateTimeKind.Utc).AddTicks(8719), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3256), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3258), 2, 240, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3237), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3241), 4, 240, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2623), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2625), 3, 240, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2520), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2522), 4, 240, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2513), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2515), 1, 240, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2504), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2507), 2, 210, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3262), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3264), 1, 240, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3249), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3251), 3, 240, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2617), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2619), 1, 240, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 120, 2, null, 1, 0m, 3 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2415), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2452), 3, 240, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3268), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3270), 3, 240, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 11, 25, 8, 46, 10, 502, DateTimeKind.Utc).AddTicks(8681), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(745), 2, 340, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), 90, 2, null, 2, 0m, 1 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2599), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2602), 2, 240, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), 120, 2, null, 1, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(3081), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(3091), 3, 240, new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2534), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2536), 4, 240, new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), 120, 2, null, 1, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2608), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2610), 3, 240, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), 120, 2, null, 1, 0m, 2 },
                    { 7, "NO EATING", new DateTime(2021, 11, 25, 8, 46, 10, 503, DateTimeKind.Utc).AddTicks(2541), new DateTime(2021, 11, 25, 10, 46, 10, 503, DateTimeKind.Local).AddTicks(2542), 2, 240, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), 120, 2, null, 1, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "FuelConsumptionPerHundredKilometers", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("f9b561c0-4c9b-4878-8a1f-b88d6af0bf84"), "Red", new DateTime(2021, 11, 25, 8, 46, 10, 499, DateTimeKind.Utc).AddTicks(2616), 12.0, "Ferrari", null },
                    { 2, new Guid("b0739d4a-9ded-4f5e-9dda-5864fa3e47ea"), "Blue", new DateTime(2021, 11, 25, 8, 46, 10, 499, DateTimeKind.Utc).AddTicks(4662), 8.0, "Alfa Romeo", null },
                    { 4, new Guid("d64b0ea8-754e-48e5-9a81-8c58d8f51c4a"), "Silver", new DateTime(2021, 11, 25, 8, 46, 10, 499, DateTimeKind.Utc).AddTicks(4796), 15.0, "BMW M5", null },
                    { 3, new Guid("3f583de5-cd62-4d83-a995-659204277af5"), "Black", new DateTime(2021, 11, 25, 8, 46, 10, 499, DateTimeKind.Utc).AddTicks(4767), 10.0, "Mercedes S Class", null }
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
