using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPool.Services.Data.Test
{
    public static class Helper
    {
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
                var userId0 = Guid.NewGuid();

                var userId1 = Guid.NewGuid();

                var userId2 = Guid.NewGuid();

                var userId3 = Guid.NewGuid();

                var userId4 = Guid.NewGuid();

                var userId5 = Guid.NewGuid();

                var userId6 = Guid.NewGuid();

                var userId7 = Guid.NewGuid();

                var userId8 = Guid.NewGuid();

                var userId9 = Guid.NewGuid();

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
                        ApplicationUserId = ApplicationUsers[0].Id,
                        Color = "Red",
                        FuelConsumptionPerHundredKilometers = 12,
                        Model = "Ferrari"
                    },
                    new UserVehicle
                    {
                        Id = 2,
                        ApplicationUserId = ApplicationUsers[1].Id,
                        Color = "Blue",
                        FuelConsumptionPerHundredKilometers = 8,
                        Model = "Alfa Romeo"
                    },
                    new UserVehicle
                    {
                        Id = 3,
                        ApplicationUserId = ApplicationUsers[2].Id,
                        Color = "Black",
                        FuelConsumptionPerHundredKilometers = 10,
                        Model = "Mercedes S Class"
                    },
                    new UserVehicle
                    {
                        Id = 4,
                        ApplicationUserId = ApplicationUsers[3].Id,
                        Color = "Silver",
                        FuelConsumptionPerHundredKilometers = 15,
                        Model = "BMW M5"
                    },
                    new UserVehicle
                    {
                        Id = 5,
                        ApplicationUserId = ApplicationUsers[4].Id,
                        Color = "Green",
                        FuelConsumptionPerHundredKilometers = 11,
                        Model = "Lambo"
                    },
                    new UserVehicle
                    {
                        Id = 6,
                        ApplicationUserId = ApplicationUsers[5].Id,
                        Color = "Black",
                        FuelConsumptionPerHundredKilometers = 9,
                        Model = "Golf4"
                    },
                    new UserVehicle
                    {
                        Id = 7,
                        ApplicationUserId = ApplicationUsers[6].Id,
                        Color = "Orange",
                        FuelConsumptionPerHundredKilometers = 10,
                        Model = "Dacia"
                    },
                    new UserVehicle
                    {
                        Id = 8,
                        ApplicationUserId = ApplicationUsers[7].Id,
                        Color = "Silver",
                        FuelConsumptionPerHundredKilometers = 6,
                        Model = "BMW M5"
                    },
                    new UserVehicle
                    {
                        Id = 9,
                        ApplicationUserId = ApplicationUsers[8].Id,
                        Color = "Carbon Black",
                        FuelConsumptionPerHundredKilometers = 2,
                        Model = "Tesla Model S"
                    },
                    new UserVehicle
                    {
                        Id = 10,
                        ApplicationUserId = ApplicationUsers[9].Id,
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
                    ApplicationUserId = ApplicationUsers[6].Id,
                    BlockedOn = System.DateTime.Today,
                    BlockedDue = System.DateTime.Today.AddDays(5)
                },
                new Ban
                {
                    Id = 2,
                    ApplicationUserId = ApplicationUsers[6].Id,
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
                    DriverId =  ApplicationUsers[0].Id,
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
                    DriverId =  ApplicationUsers[1].Id,
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
                    DriverId =  ApplicationUsers[2].Id,
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
                    DriverId =  ApplicationUsers[3].Id,
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
                    DriverId =  ApplicationUsers[2].Id,
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
                    DriverId =  ApplicationUsers[3].Id,
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
                    DriverId =  ApplicationUsers[1].Id,
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
                    DriverId =  ApplicationUsers[0].Id,
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
                    DriverId =  ApplicationUsers[7].Id,
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
                    DriverId =  ApplicationUsers[8].Id,
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
                    DriverId =  ApplicationUsers[1].Id,
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
                    DriverId =  ApplicationUsers[3].Id,
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
                    DriverId =  ApplicationUsers[2].Id,
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
                    DriverId =  ApplicationUsers[1].Id,
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
                    DriverId =  ApplicationUsers[0].Id,
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
                    DriverId =  ApplicationUsers[1].Id,
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
                    DriverId =  ApplicationUsers[2].Id,
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
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 1
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[1].Id,
                     TripId = 2
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[2].Id,
                     TripId = 3
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[3].Id,
                     TripId = 1
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[3].Id,
                     TripId = 2
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[3].Id,
                     TripId = 3
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 3
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[4].Id,
                     TripId = 4
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 5
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 6
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 7
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 8
                },

                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[1].Id,
                     TripId = 15
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[0].Id,
                     TripId = 16
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[2].Id,
                     TripId = 15
                },
                new TripPassenger
                {
                     ApplicationUserId = ApplicationUsers[2].Id,
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
                    ApplicationUserId = ApplicationUsers[0].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 2,
                    ApplicationUserId = ApplicationUsers[1].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 3,
                    ApplicationUserId = ApplicationUsers[2].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 4,
                    ApplicationUserId = ApplicationUsers[3].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 5,
                    ApplicationUserId = ApplicationUsers[4].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 6,
                    ApplicationUserId = ApplicationUsers[5].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 7,
                    ApplicationUserId = ApplicationUsers[6].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 8,
                    ApplicationUserId = ApplicationUsers[7].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 9,
                    ApplicationUserId = ApplicationUsers[8].Id,
                    ImageLink = GlobalConstants.DefaultPicture
                },
                new ProfilePicture
                {
                    Id = 10,
                    ApplicationUserId = ApplicationUsers[9].Id,
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
                    AddedByUserId = ApplicationUsers[0].Id,
                    ApplicationUserId = ApplicationUsers[1].Id,
                    Feedback = "Nice car",
                    Value = 4
                },
                new Rating
                {
                    Id = 2,
                    TripId = 2,
                    AddedByUserId = ApplicationUsers[1].Id,
                    ApplicationUserId = ApplicationUsers[2].Id,
                    Feedback = "Bad person",
                    Value = 1
                },
                new Rating
                {
                    Id = 3,
                    TripId = 3,
                    AddedByUserId = ApplicationUsers[2].Id,
                    ApplicationUserId = ApplicationUsers[3].Id,
                    Feedback = "Great trip",
                    Value = 5
                },
                new Rating
                {
                    Id = 4,
                    TripId = 4,
                    AddedByUserId = ApplicationUsers[3].Id,
                    ApplicationUserId = ApplicationUsers[4].Id,
                    Feedback = "dirty car, good person",
                    Value = 4
                },
                new Rating
                {
                    Id = 5,
                    TripId = 5,
                    AddedByUserId = ApplicationUsers[4].Id,
                    ApplicationUserId = ApplicationUsers[5].Id,
                    Feedback = "Great trip",
                    Value = 3
                },
                new Rating
                {
                    Id = 6,
                    TripId = 5,
                    AddedByUserId = ApplicationUsers[5].Id,
                    ApplicationUserId = ApplicationUsers[6].Id,
                    Feedback = "safe driver",
                    Value = 5
                },
                new Rating
                {
                    Id = 7,
                    TripId = 5,
                    AddedByUserId = ApplicationUsers[6].Id,
                    ApplicationUserId = ApplicationUsers[7].Id,
                    Feedback = "Bad driver",
                    Value = 3
                },
                new Rating
                {
                    Id = 8,
                    TripId = 5,
                    AddedByUserId = ApplicationUsers[7].Id,
                    ApplicationUserId = ApplicationUsers[8].Id,
                    Feedback = "Good friend",
                    Value = 5
                },
                new Rating
                {
                    Id = 9,
                    TripId = 5,
                    AddedByUserId = ApplicationUsers[8].Id,
                    ApplicationUserId = ApplicationUsers[9].Id,
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

                var mockDbSetCountries = Countries.AsQueryable().BuildMockDbSet();
                var mockDbSetCities = Cities.AsQueryable().BuildMockDbSet();
                var mockDbSetAddresses = Addresses.AsQueryable().BuildMockDbSet();
                var mockDbSetAppRoles = ApplicationRoles.AsQueryable().BuildMockDbSet();
                var mockDbSetAppUsers = ApplicationUsers.AsQueryable().BuildMockDbSet();
                var mockDbSetVehicles = UserVehicles.AsQueryable().BuildMockDbSet();
                var mockDbSetBans = Bans.AsQueryable().BuildMockDbSet();
                var mockDbSetTrips = Trips.AsQueryable().BuildMockDbSet();
                var mockDbSetTripPassengers = TripPassengers.AsQueryable().BuildMockDbSet();
                var mockDbSetProfilePictures = ProfilePictures.AsQueryable().BuildMockDbSet();
                var mockDbSetRatings = Ratings.AsQueryable().BuildMockDbSet();

                mockDbContext.Setup(db => db.Countries).Returns(mockDbSetCountries.Object);
                mockDbContext.Setup(db => db.Cities).Returns(mockDbSetCities.Object);
                mockDbContext.Setup(db => db.Addresses).Returns(mockDbSetAddresses.Object);
                mockDbContext.Setup(db => db.ApplicationRoles).Returns(mockDbSetAppRoles.Object);
                mockDbContext.Setup(db => db.ApplicationUsers).Returns(mockDbSetAppUsers.Object);
                mockDbContext.Setup(db => db.UserVehicles).Returns(mockDbSetVehicles.Object);
                mockDbContext.Setup(db => db.Bans).Returns(mockDbSetBans.Object);
                mockDbContext.Setup(db => db.Trips).Returns(mockDbSetTrips.Object);
                mockDbContext.Setup(db => db.Ratings).Returns(mockDbSetRatings.Object);
                mockDbContext.Setup(db => db.TripPassengers).Returns(mockDbSetTripPassengers.Object);
                mockDbContext.Setup(db => db.ProfilePictures).Returns(mockDbSetProfilePictures.Object);

                return mockDbContext;
            }
        }
    }
}
