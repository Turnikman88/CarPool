﻿// <auto-generated />
using System;
using CarPool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarPool.Data.Migrations
{
    [DbContext(typeof(CarPoolDBContext))]
    partial class CarPoolDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(2334),
                            IsDeleted = false,
                            Latitude = 42.698334m,
                            Longitude = 23.319941m,
                            StreetName = "Vasil Levski 14"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(6303),
                            IsDeleted = false,
                            Latitude = 42.682073m,
                            Longitude = 23.326622m,
                            StreetName = "blv. Iztochen 23"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(6361),
                            IsDeleted = false,
                            Latitude = 42.698334m,
                            Longitude = 23.254942m,
                            StreetName = "blv. Halic 12"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 4,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(6366),
                            IsDeleted = false,
                            Latitude = 42.711242m,
                            Longitude = 23.316655m,
                            StreetName = "blv. Zeus 12"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 5,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(6433),
                            IsDeleted = false,
                            Latitude = 42.625045m,
                            Longitude = 23.400539m,
                            StreetName = "blv. Romunska Morava 1"
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(8129),
                            IsDeleted = false,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(8614),
                            IsDeleted = false,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("ApplicationRoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ApplicationRoleId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("ApplicationUsers");

                    b.HasCheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");

                    b.HasData(
                        new
                        {
                            Id = new Guid("59523a60-751a-4372-ba59-aacdeb081974"),
                            AddressId = 1,
                            ApplicationRoleId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 641, DateTimeKind.Utc).AddTicks(6379),
                            Email = "mishkov@misho.com",
                            EmailConfirmed = true,
                            FirstName = "Misho",
                            LastName = "Mishkov",
                            Password = "$2a$11$.PCa98YMVPMn8mVJIfjwdu3FYp.7Livu6lyCaMQQ/njColgDby9Vi",
                            PhoneNumber = "+35920768005",
                            Username = "misha_m"
                        },
                        new
                        {
                            Id = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            AddressId = 2,
                            ApplicationRoleId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 862, DateTimeKind.Utc).AddTicks(9059),
                            Email = "petio@mvc.net",
                            EmailConfirmed = true,
                            FirstName = "Peter",
                            LastName = "Petrov",
                            Password = "$2a$11$Cks7zGNMUGH9LAPhhAraYO3KaSGUvcl7nU6OwDLchKfTBs/6HM3lu",
                            PhoneNumber = "+35924492877",
                            Username = "petio_p"
                        },
                        new
                        {
                            Id = new Guid("62c2711a-c063-4ce8-9ebd-ae8c9c61124c"),
                            AddressId = 3,
                            ApplicationRoleId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 54, DateTimeKind.Utc).AddTicks(5767),
                            Email = "koksal@asd.tr",
                            EmailConfirmed = true,
                            FirstName = "Koksal",
                            LastName = "Baba",
                            Password = "$2a$11$Vw1VOB1GI.Htz095ltksfOJx3DiKTzFSexEgRzb8Tg/TB98jrGoCW",
                            PhoneNumber = "+35922649764",
                            Username = "koksal"
                        },
                        new
                        {
                            Id = new Guid("68dfedaa-6f8c-4162-a3c3-d4aebd2f948b"),
                            AddressId = 4,
                            ApplicationRoleId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 245, DateTimeKind.Utc).AddTicks(7471),
                            Email = "indebt@greece.gov",
                            EmailConfirmed = true,
                            FirstName = "Nikolaos",
                            LastName = "Tsitsibaris",
                            Password = "$2a$11$d.tyO/jLbYzb2Uj6mTM8Huse/Etda75xwzjVX.FSBMRP.AnoQNiTq",
                            PhoneNumber = "+35924775508",
                            Username = "cicibar"
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BlockedDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BlockedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Ban");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            BlockedDue = new DateTime(2021, 11, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            BlockedOn = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 439, DateTimeKind.Utc).AddTicks(8200)
                        },
                        new
                        {
                            Id = 2,
                            ApplicationUserId = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            BlockedOn = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 443, DateTimeKind.Utc).AddTicks(8583)
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique();

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(8220),
                            IsDeleted = false,
                            Name = "Sofia"
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9920),
                            IsDeleted = false,
                            Name = "Plovdiv"
                        },
                        new
                        {
                            Id = 3,
                            CountryId = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9965),
                            IsDeleted = false,
                            Name = "Varna"
                        },
                        new
                        {
                            Id = 4,
                            CountryId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9968),
                            IsDeleted = false,
                            Name = "Istanbul"
                        },
                        new
                        {
                            Id = 5,
                            CountryId = 3,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9971),
                            IsDeleted = false,
                            Name = "Athens"
                        },
                        new
                        {
                            Id = 6,
                            CountryId = 3,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9978),
                            IsDeleted = false,
                            Name = "Thessaloniki"
                        },
                        new
                        {
                            Id = 7,
                            CountryId = 3,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9981),
                            IsDeleted = false,
                            Name = "Patras"
                        },
                        new
                        {
                            Id = 8,
                            CountryId = 4,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9983),
                            IsDeleted = false,
                            Name = "Yash"
                        },
                        new
                        {
                            Id = 9,
                            CountryId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9985),
                            IsDeleted = false,
                            Name = "Odrin"
                        },
                        new
                        {
                            Id = 10,
                            CountryId = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9988),
                            IsDeleted = false,
                            Name = "Ankara"
                        },
                        new
                        {
                            Id = 11,
                            CountryId = 4,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9990),
                            IsDeleted = false,
                            Name = "Bucharest"
                        },
                        new
                        {
                            Id = 12,
                            CountryId = 4,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 639, DateTimeKind.Utc).AddTicks(9992),
                            IsDeleted = false,
                            Name = "Craiova"
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 637, DateTimeKind.Utc).AddTicks(8807),
                            IsDeleted = false,
                            Name = "Bulgaria"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 638, DateTimeKind.Utc).AddTicks(792),
                            IsDeleted = false,
                            Name = "Turkey"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 638, DateTimeKind.Utc).AddTicks(821),
                            IsDeleted = false,
                            Name = "Greece"
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 638, DateTimeKind.Utc).AddTicks(824),
                            IsDeleted = false,
                            Name = "Romania"
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.ProfilePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("ProfilePictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = new Guid("59523a60-751a-4372-ba59-aacdeb081974"),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 444, DateTimeKind.Utc).AddTicks(8015),
                            ImageTitle = "(No title)",
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AddedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedByUserId = new Guid("59523a60-751a-4372-ba59-aacdeb081974"),
                            ApplicationUserId = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 640, DateTimeKind.Utc).AddTicks(9901),
                            Feedback = "Nice car",
                            Value = 4
                        },
                        new
                        {
                            Id = 2,
                            AddedByUserId = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            ApplicationUserId = new Guid("59523a60-751a-4372-ba59-aacdeb081974"),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 641, DateTimeKind.Utc).AddTicks(1951),
                            Feedback = "Bad person",
                            Value = 1
                        },
                        new
                        {
                            Id = 3,
                            AddedByUserId = new Guid("62c2711a-c063-4ce8-9ebd-ae8c9c61124c"),
                            ApplicationUserId = new Guid("68dfedaa-6f8c-4162-a3c3-d4aebd2f948b"),
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 7, 641, DateTimeKind.Utc).AddTicks(1991),
                            Feedback = "(No feedback)",
                            Value = 5
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationAddressId")
                        .HasColumnType("int");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("FreeSeats")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PassengersCount")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StartAddressId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAddressId");

                    b.HasIndex("DriverId");

                    b.HasIndex("StartAddressId");

                    b.ToTable("Trips");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalComment = "(No comment)",
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 444, DateTimeKind.Utc).AddTicks(2389),
                            DepartureTime = new DateTime(2021, 11, 18, 0, 21, 8, 444, DateTimeKind.Local).AddTicks(4755),
                            DestinationAddressId = 2,
                            Distance = 340,
                            DriverId = new Guid("59523a60-751a-4372-ba59-aacdeb081974"),
                            DurationInMinutes = 90,
                            FreeSeats = 2,
                            PassengersCount = 2,
                            Price = 0m,
                            StartAddressId = 1
                        },
                        new
                        {
                            Id = 2,
                            AdditionalComment = "NO SMOKEING",
                            CreatedOn = new DateTime(2021, 11, 17, 22, 21, 8, 444, DateTimeKind.Utc).AddTicks(6672),
                            DepartureTime = new DateTime(2021, 11, 18, 0, 21, 8, 444, DateTimeKind.Local).AddTicks(6718),
                            DestinationAddressId = 3,
                            Distance = 240,
                            DriverId = new Guid("30b9d616-60c3-4df0-a8ac-31a0cf7c7e08"),
                            DurationInMinutes = 120,
                            FreeSeats = 2,
                            PassengersCount = 1,
                            Price = 0m,
                            StartAddressId = 2
                        });
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.TripPassenger", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplicationUserId", "TripId");

                    b.HasIndex("TripId");

                    b.ToTable("TripPassengers");
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.UserVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("UserVehicles");
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Address", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.ApplicationUser", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.Address", "Address")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationRole", "ApplicationRole")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("ApplicationRoleId");
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Ban", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "ApplicationUser")
                        .WithOne("Ban")
                        .HasForeignKey("CarPool.Data.Models.DatabaseModels.Ban", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.City", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.ProfilePicture", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "ApplicationUser")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("CarPool.Data.Models.DatabaseModels.ProfilePicture", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Rating", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "ApplicationUser")
                        .WithMany("Ratings")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.Trip", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.Address", "DestinationAddress")
                        .WithMany("TripsDestinationAddress")
                        .HasForeignKey("DestinationAddressId")
                        .HasConstraintName("FK_Trips_DestinationAddresses")
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("DriverId")
                        .HasConstraintName("FK_Trips_ApplicationUsers")
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.DatabaseModels.Address", "StartAddress")
                        .WithMany("TripsStartAddress")
                        .HasForeignKey("StartAddressId")
                        .HasConstraintName("FK_Trips_StartAddresses")
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.TripPassenger", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "ApplicationUser")
                        .WithMany("TripsAsPassenger")
                        .HasForeignKey("ApplicationUserId")
                        .HasConstraintName("FK_TripPassengerRelation_ApplicationUsers")
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.DatabaseModels.Trip", "Trip")
                        .WithMany("Passengers")
                        .HasForeignKey("TripId")
                        .HasConstraintName("FK_TripPassengerRelation_Trips")
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.DatabaseModels.UserVehicle", b =>
                {
                    b.HasOne("CarPool.Data.Models.DatabaseModels.ApplicationUser", "ApplicationUser")
                        .WithOne("Vehicle")
                        .HasForeignKey("CarPool.Data.Models.DatabaseModels.UserVehicle", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
