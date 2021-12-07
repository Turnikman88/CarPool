using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using Moq;
using System;
using System.Collections.Generic;

namespace CarPool.Services.Data.Test
{
    public static class Helper
    {
        static Guid userId0 = Guid.Parse("bb23505b-05d4-485e-80eb-ac7295281398");
        static Guid userId1 = Guid.Parse("56cb6af1-5172-4873-ad97-1365298b94db");
        static Guid userId2 = Guid.Parse("1e227b3e-64eb-4ecf-9155-5e4f994ffcc6");
        static Guid userId3 = Guid.Parse("85b2a941-9a45-4d97-a821-58aec861dab8");
        static Guid userId4 = Guid.Parse("9a9bc4b8-add8-423d-a2de-9f4ce1176c1a");
        static Guid userId5 = Guid.Parse("3181be47-fe62-4980-b8f0-c2636d2894b5");
        static Guid userId6 = Guid.Parse("9ef2aed3-8e58-4292-845b-ee59177499bb");
        static Guid userId7 = Guid.Parse("b474befe-251c-4a92-ab6f-57e940d76363");
        static Guid userId8 = Guid.Parse("8d86d20b-59b1-46e1-9d66-db4216bada8c");
        static Guid userId9 = Guid.Parse("0c5104d7-d971-4f58-a2cb-915533023b91");
        public static List<Address> Addresses
        {
            get
            {
                return new List<Address>()
                {
                new Address
                {
                    Id = 1,
                    CityId = 1,
                    StreetName = "Vasil Levski 14",
                    Latitude = 42.6860436M,
                    Longitude = 23.320311M
                },
                new Address
                {
                    Id = 2,
                    CityId = 2,
                    StreetName = "bulevard Iztochen 23",
                    Latitude = 42.1382815M,
                    Longitude = 24.7604295M
                },
                new Address
                {
                    Id = 3,
                    CityId = 3,
                    StreetName = "bulevard Tsar Osvoboditel 83b",
                    Latitude = 43.2126824M,
                    Longitude = 27.9168517M
                },
                new Address
                {
                    Id = 4,
                    CityId = 4,
                    StreetName = "Defterdar, Ayvansaray Cd., 34050 Eyüpsultan",
                    Latitude = 41.0403314M,
                    Longitude = 28.939206M
                },
                new Address
                {
                    Id = 5,
                    CityId = 5,
                    StreetName = "Ippokratous 1",
                    Latitude = 37.981142M,
                    Longitude = 23.732380M
                },
                new Address
                {
                    Id = 6,
                    CityId = 6,
                    StreetName = null,
                    Latitude = 40.640014M,
                    Longitude = 22.944397M
                },
                new Address
                {
                    Id = 7,
                    CityId = 7,
                    StreetName = null,
                    Latitude = 38.232467M,
                    Longitude = 21.736326M
                },
                new Address
                {
                    Id = 8,
                    CityId = 8,
                    StreetName = null,
                    Latitude = 47.151716M,
                    Longitude = 27.587696M
                },
                new Address
                {
                    Id = 9,
                    CityId = 9,
                    StreetName = null,
                    Latitude = 41.669344M,
                    Longitude = 26.568406M
                }
                };
            }
        }
        public static List<ApplicationRole> ApplicationRoles
        {
            get
            {
                return new List<ApplicationRole>()
            {
                new ApplicationRole
                {
                    Id = 1,
                    Name = "Admin"
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "User"
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "Banned"
                },
                new ApplicationRole
                {
                    Id = 4,
                    Name = "NotConfirmed"
                }
            };
            }
        }
        public static List<ApplicationUser> ApplicationUsers
        {
            get
            {

                return new List<ApplicationUser>()
                {
               new ApplicationUser
                {
                    Id = userId0,
                    Username = "kalin",
                    FirstName = "Kalin",
                    LastName = "Balimezov",
                    Email = "kalin@telerik.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123$"),
                    PhoneNumber = "+35920768005",
                    ApplicationRoleId = 2,
                    AddressId = 1
                },
                new ApplicationUser
                {
                    Id = userId1,
                    Username = "petio_p",
                    EmailConfirmed = true,
                    PhoneNumber = "+35924492877",
                    ApplicationRoleId = 2,
                    FirstName = "Peter",
                    LastName = "Petrov",
                    Email = "petio@mvc.net",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                    AddressId = 2
                },
                new ApplicationUser
                {
                    Id = userId2,
                    Username = "koksal",
                    EmailConfirmed = true,
                    PhoneNumber = "+35922649764",
                    ApplicationRoleId = 2,
                    FirstName = "Koksal",
                    LastName = "Baba",
                    Email = "koksal@asd.tr",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                    AddressId = 3
                },
                new ApplicationUser
                {
                    Id = userId3,
                    Username = "Tsitsibaris",
                    EmailConfirmed = true,
                    PhoneNumber = "+35924775508",
                    ApplicationRoleId = 1,
                    FirstName = "Nikolaos",
                    LastName = "Tsitsibaris",
                    Email = "indebt@greece.gov",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                    AddressId = 3
                },
                new ApplicationUser
                {
                    Id = userId4,
                    Username = "georgi",
                    FirstName = "georgi",
                    LastName = "petrov",
                    Email = "joro@telerik.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123$"),
                    PhoneNumber = "+35920768015",
                    ApplicationRoleId = 2,
                    AddressId = 1
                },
                new ApplicationUser
                {
                    Id = userId5,
                    Username = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123$"),
                    PhoneNumber = "+35920738011",
                    ApplicationRoleId = 1,
                    AddressId = 1
                },
                new ApplicationUser
                {
                    Id = userId6,
                    Username = "Carlitos",
                    FirstName = "Carlos",
                    LastName = "Merkez",
                    Email = "merkez@grece.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Merkez123$"),
                    PhoneNumber = "+32920728011",
                    ApplicationRoleId = 2,
                    AddressId = 4
                },
                new ApplicationUser
                {
                    Id = userId7,
                    Username = "Ramen",
                    FirstName = "Ramos",
                    LastName = "Enerto",
                    Email = "ramen@aoc.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Ramen123$"),
                    PhoneNumber = "+3292234215",
                    ApplicationRoleId = 2,
                    AddressId = 5
                },
                new ApplicationUser
                {
                    Id = userId8,
                    Username = "PewDie",
                    FirstName = "Felix",
                    LastName = "Kjellberg ",
                    Email = "pewdie@yt.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Pewd123$"),
                    PhoneNumber = "+3291238015",
                    ApplicationRoleId = 2,
                    AddressId = 9
                },
                new ApplicationUser
                {
                    Id = userId9,
                    Username = "Get_RighT",
                    FirstName = "Christopher",
                    LastName = "Alesund",
                    Email = "christopher@nip.se",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("Christopher123$"),
                    PhoneNumber = "+3292233015",
                    ApplicationRoleId = 2,
                    AddressId = 9
                }
            };
            }
        }
        public static List<Country> Countries
        {
            get
            {
                return new List<Country>() {
                    new Country
                    {
                        Id = 1,
                        Name = "Bulgaria"
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "Turkey"
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Greece"
                    },
                    new Country
                    {
                        Id = 4,
                        Name = "Romania"
                    }
                };
            }
        }
        public static List<City> Cities
        {
            get
            {
                return new List<City>
                {
                    new City
                {
                    Id = 1,
                    Name = "Sofia",
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Plovdiv",
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Varna",
                    CountryId = 1
                },
                new City
                {
                    Id = 4,
                    Name = "Istanbul",
                    CountryId = 2
                },
                new City
                {
                    Id = 5,
                    Name = "Athens",
                    CountryId = 3
                },
                new City
                {
                    Id = 6,
                    Name = "Thessaloniki",
                    CountryId = 3
                },
                new City
                {
                    Id = 7,
                    Name = "Patras",
                    CountryId = 3
                },
                new City
                {
                    Id = 8,
                    Name = "Yash",
                    CountryId = 4
                },
                new City
                {
                    Id = 9,
                    Name = "Odrin",
                    CountryId = 2
                },
                new City
                {
                    Id = 10,
                    Name = "Ankara",
                    CountryId = 2
                },
                new City
                {
                    Id = 11,
                    Name = "Bucharest",
                    CountryId = 4
                },
                new City
                {
                    Id = 12,
                    Name = "Craiova",
                    CountryId = 4
                }
                };
            }
        }
        public static List<UserVehicle> UserVehicles
        {
            get
            {
                return new List<UserVehicle>()
                {
                    new UserVehicle
                    {
                        Id = 1,
                        ApplicationUserId = userId0,
                        Color = "Red",
                        FuelConsumptionPerHundredKilometers = 12,
                        Model = "Ferrari"
                    },
                    new UserVehicle
                    {
                        Id = 2,
                        ApplicationUserId = userId1,
                        Color = "Blue",
                        FuelConsumptionPerHundredKilometers = 8,
                        Model = "Alfa Romeo"
                    },
                    new UserVehicle
                    {
                        Id = 3,
                        ApplicationUserId = userId2,
                        Color = "Black",
                        FuelConsumptionPerHundredKilometers = 10,
                        Model = "Mercedes S Class"
                    },
                    new UserVehicle
                    {
                        Id = 4,
                        ApplicationUserId = userId3,
                        Color = "Silver",
                        FuelConsumptionPerHundredKilometers = 15,
                        Model = "BMW M5"
                    },
                    new UserVehicle
                    {
                        Id = 5,
                        ApplicationUserId = userId4,
                        Color = "Green",
                        FuelConsumptionPerHundredKilometers = 11,
                        Model = "Lambo"
                    },
                    new UserVehicle
                    {
                        Id = 6,
                        ApplicationUserId = userId5,
                        Color = "Black",
                        FuelConsumptionPerHundredKilometers = 9,
                        Model = "Golf4"
                    },
                    new UserVehicle
                    {
                        Id = 7,
                        ApplicationUserId = userId6,
                        Color = "Orange",
                        FuelConsumptionPerHundredKilometers = 10,
                        Model = "Dacia"
                    },
                    new UserVehicle
                    {
                        Id = 8,
                        ApplicationUserId = userId7,
                        Color = "Silver",
                        FuelConsumptionPerHundredKilometers = 6,
                        Model = "BMW M5"
                    },
                    new UserVehicle
                    {
                        Id = 9,
                        ApplicationUserId = userId8,
                        Color = "Carbon Black",
                        FuelConsumptionPerHundredKilometers = 2,
                        Model = "Tesla Model S"
                    },
                    new UserVehicle
                    {
                        Id = 10,
                        ApplicationUserId = userId9,
                        Color = "Silver",
                        FuelConsumptionPerHundredKilometers = 16,
                        Model = "Mercedes-Benz S Coupe"
                    }

                };
            }
        }
        public static List<Ban> Bans
        {
            get
            {
                return new List<Ban>()
            {
                new Ban
                {
                    Id = 1,
                    ApplicationUserId = userId6,
                    BlockedOn = System.DateTime.Today,
                    BlockedDue = System.DateTime.Today.AddDays(5)
                },
                new Ban
                {
                    Id = 2,
                    ApplicationUserId = userId6,
                    BlockedOn = System.DateTime.Today
                }
            };
            }
        }
        public static List<Trip> Trips
        {
            get
            {
                return new List<Trip>()
            {
                new Trip
                {
                    Id = 1,
                    DriverId =  userId0,
                    StartAddressId = 1,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 90,
                    Distance = 340,
                    PassengersCount = 2,
                    FreeSeats = 2
                },
                new Trip
                {
                    Id = 2,
                    DriverId =  userId1,
                    StartAddressId = 2,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 2,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKING"
                },
                new Trip
                {
                    Id = 3,
                    DriverId = userId2,
                    StartAddressId = 4,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 110,
                    Distance = 210,
                    PassengersCount = 2,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKING"
                },
                new Trip
                {
                    Id = 4,
                    DriverId =  userId3,
                    StartAddressId = 4,
                    DestinationAddressId = 1,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "Long comment here"
                },
                new Trip
                {
                    Id = 5,
                    DriverId = userId2,
                    StartAddressId = 1,
                    DestinationAddressId = 4,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "Additional comments below"
                },
                new Trip
                {
                    Id = 6,
                    DriverId = userId3,
                    StartAddressId = 2,
                    DestinationAddressId = 4,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "follow me on twitter"
                },
                new Trip
                {
                    Id = 7,
                    DriverId = userId1,
                    StartAddressId = 4,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "NO EATING"
                },
                new Trip
                {
                    Id = 8,
                    DriverId = userId0,
                    StartAddressId = 1,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now.AddDays(2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "CHEAP AND FAST"
                },
                new Trip
                {
                    Id = 9,
                    DriverId = userId7,
                    StartAddressId = 2,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "FAST FAST FAST"
                },
                new Trip
                {
                    Id = 10,
                    DriverId = userId8,
                    StartAddressId = 3,
                    DestinationAddressId = 1,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "No pets"
                },
                new Trip
                {
                    Id = 11,
                    DriverId = userId1,
                    StartAddressId = 1,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 19.11m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKING NO FOOD"
                },
                new Trip
                {
                    Id = 12,
                    DriverId = userId3,
                    StartAddressId = 4,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 15.55m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "NO STOPS"
                },
                new Trip
                {
                    Id = 13,
                    DriverId = userId2,
                    StartAddressId = 3,
                    DestinationAddressId = 4,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 10.13m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "Good looking and friendly"
                },
                new Trip
                {
                    Id = 14,
                    DriverId = userId1,
                    StartAddressId = 1,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 10.11m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "Fast car"
                },
                new Trip
                {
                    Id = 15,
                    DriverId = userId0,
                    StartAddressId = 3,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 15.21m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "High price"
                },
                new Trip
                {
                    Id = 16,
                    DriverId = userId1,
                    StartAddressId = 2,
                    DestinationAddressId = 1,
                    DepartureTime = DateTime.Now.AddDays(-2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 21.21m,
                    PassengersCount = 2,
                    FreeSeats = 2,
                    AdditionalComment = "Im not alone"
                },
                new Trip
                {
                    Id = 17,
                    DriverId = userId2,
                    StartAddressId = 1,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now.AddDays(-2),
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 12.23m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "No kids"
                }
            };
            }
        }
        public static List<TripPassenger> TripPassengers
        {
            get
            {
                return new List<TripPassenger>()
            {
                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 1
                },
                new TripPassenger
                {
                     ApplicationUserId = userId1,
                     TripId = 2
                },
                new TripPassenger
                {
                     ApplicationUserId = userId2,
                     TripId = 3
                },
                new TripPassenger
                {
                     ApplicationUserId = userId3,
                     TripId = 1
                },

                new TripPassenger
                {
                     ApplicationUserId = userId3,
                     TripId = 2
                },

                new TripPassenger
                {
                     ApplicationUserId = userId3,
                     TripId = 3
                },

                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 3
                },

                new TripPassenger
                {
                     ApplicationUserId = userId4,
                     TripId = 4
                },

                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 5
                },

                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 6
                },

                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 7
                },

                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 8
                },

                new TripPassenger
                {
                     ApplicationUserId = userId1,
                     TripId = 15
                },
                new TripPassenger
                {
                     ApplicationUserId = userId0,
                     TripId = 16
                },
                new TripPassenger
                {
                     ApplicationUserId = userId2,
                     TripId = 15
                },
                new TripPassenger
                {
                     ApplicationUserId = userId2,
                     TripId = 16
                },
            };
            }
        }
        public static List<ProfilePicture> ProfilePictures
        {
            get
            {
                return new List<ProfilePicture>()
            {
                new ProfilePicture
                {
                    Id = 1,
                    ApplicationUserId = userId0,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 2,
                    ApplicationUserId = userId1,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 3,
                    ApplicationUserId = userId2,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 4,
                    ApplicationUserId = userId3,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 5,
                    ApplicationUserId = userId4,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 6,
                    ApplicationUserId = userId5,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 7,
                    ApplicationUserId = userId6,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 8,
                    ApplicationUserId = userId7,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 9,
                    ApplicationUserId = userId8,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 10,
                    ApplicationUserId = userId9,
                    ImageLink = GlobalConstants.DefaultPicture
                }
            };
            }
        }
        public static List<Rating> Ratings
        {
            get
            {
                return new List<Rating>()
            {
                new Rating
                {
                    Id = 1,
                    TripId = 1,
                    AddedByUserId = userId0,
                    ApplicationUserId = userId1,
                    Feedback = "Nice car",
                    Value = 4
                },
                new Rating
                {
                    Id = 2,
                    TripId = 2,
                    AddedByUserId = userId1,
                    ApplicationUserId = userId2,
                    Feedback = "Bad person",
                    IsReport = true
                },
                new Rating
                {
                    Id = 3,
                    TripId = 3,
                    AddedByUserId = userId2,
                    ApplicationUserId = userId3,
                    Feedback = "Great trip",
                    Value = 5
                },
                new Rating
                {
                    Id = 4,
                    TripId = 4,
                    AddedByUserId =userId3,
                    ApplicationUserId = userId4,
                    Feedback = "dirty car, good person",
                    Value = 4
                },
                new Rating
                {
                    Id = 5,
                    TripId = 5,
                    AddedByUserId =userId4,
                    ApplicationUserId = userId5,
                    Feedback = "Great trip",
                    Value = 3
                },
                new Rating
                {
                    Id = 6,
                    TripId = 5,
                    AddedByUserId = userId5,
                    ApplicationUserId = userId6,
                    Feedback = "safe driver",
                    Value = 5
                },
                new Rating
                {
                    Id = 7,
                    TripId = 5,
                    AddedByUserId = userId6,
                    ApplicationUserId = userId7,
                    Feedback = "Bad driver",
                    IsReport = true
                },
                new Rating
                {
                    Id = 8,
                    TripId = 5,
                    AddedByUserId = userId7,
                    ApplicationUserId = userId8,
                    Feedback = "Good friend",
                    Value = 5
                },
                new Rating
                {
                    Id = 9,
                    TripId = 5,
                    AddedByUserId = userId8,
                    ApplicationUserId = userId9,
                    Feedback = "Best driver",
                    Value = 5
                }
            };
            }
        }

        public static Mock<CarPoolDBContext> MockDbContext
        {
            get
            {
                var mockDbContext = new Mock<CarPoolDBContext>();

                //var mockDbSetCountries = Countries.AsQueryable().BuildMockDbSet();
                //var mockDbSetCities = Cities.AsQueryable().BuildMockDbSet();
                //var mockDbSetAddresses = Addresses.AsQueryable().BuildMockDbSet();
                //var mockDbSetAppRoles = ApplicationRoles.AsQueryable().BuildMockDbSet();
                //var mockDbSetAppUsers = ApplicationUsers.AsQueryable().BuildMockDbSet();
                //var mockDbSetVehicles = UserVehicles.AsQueryable().BuildMockDbSet();
                //var mockDbSetBans = Bans.AsQueryable().BuildMockDbSet();
                //var mockDbSetTrips = Trips.AsQueryable().BuildMockDbSet();
                //var mockDbSetTripPassengers = TripPassengers.AsQueryable().BuildMockDbSet();
                //var mockDbSetProfilePictures = ProfilePictures.AsQueryable().BuildMockDbSet();
                //var mockDbSetRatings = Ratings.AsQueryable().BuildMockDbSet();

                //mockDbContext.Setup(db => db.Countries).Returns(mockDbSetCountries.Object);
                //mockDbContext.Setup(db => db.Ratings).Returns(mockDbSetRatings.Object);
                //mockDbContext.Setup(db => db.Cities).Returns(mockDbSetCities.Object);
                //mockDbContext.Setup(db => db.Addresses).Returns(mockDbSetAddresses.Object);
                //mockDbContext.Setup(db => db.ApplicationRoles).Returns(mockDbSetAppRoles.Object);
                //mockDbContext.Setup(db => db.ApplicationUsers).Returns(mockDbSetAppUsers.Object);
                //mockDbContext.Setup(db => db.UserVehicles).Returns(mockDbSetVehicles.Object);
                //mockDbContext.Setup(db => db.Bans).Returns(mockDbSetBans.Object);
                //mockDbContext.Setup(db => db.Trips).Returns(mockDbSetTrips.Object);
                //mockDbContext.Setup(db => db.TripPassengers).Returns(mockDbSetTripPassengers.Object);
                //mockDbContext.Setup(db => db.ProfilePictures).Returns(mockDbSetProfilePictures.Object);

                return mockDbContext;
            }
        }

    }
}
