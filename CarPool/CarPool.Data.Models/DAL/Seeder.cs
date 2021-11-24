using CarPool.Common;
using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CarPool.Data.Models.DAL
{
    public static class Seeder
    {
        public static void Seed(this ModelBuilder db)
        {
            var userId0 = Guid.NewGuid();

            var userId1 = Guid.NewGuid();

            var userId2 = Guid.NewGuid();

            var userId3 = Guid.NewGuid();

            var userId4 = Guid.NewGuid();

            var countries = new List<Country>()
            {
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

            db.Entity<Country>().HasData(countries);

            var cities = new List<City>()
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

            db.Entity<City>().HasData(cities);

            var addresses = new List<Address>()
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
                    StreetName = "blv. Iztochen 23",
                    Latitude = 42.1382775M,
                    Longitude = 24.7604295M
                },
                new Address
                {
                    Id = 3,
                    CityId = 3,
                    StreetName = "blv. Halic 12",
                    Latitude = 41.022079M,
                    Longitude = 28.9483964M
                },
                new Address
                {
                    Id = 4,
                    CityId = 4,
                    StreetName = "blv. Zeus 12",
                    Latitude = 37.9916167M,
                    Longitude = 23.7363294M
                },
                new Address
                {
                    Id = 5,
                    CityId = 5,
                    StreetName = "blv. Romunska Morava 1",
                    Latitude = 44.432558M,
                    Longitude = 26.111871M
                }
            };

            db.Entity<Address>().HasData(addresses);

            var roles = new List<ApplicationRole>()
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

            db.Entity<ApplicationRole>().HasData(roles);

            var ratings = new List<Rating>()
            {
                new Rating
                {
                    Id = 1,
                    AddedByUserId = userId0,
                    ApplicationUserId = userId1,
                    Value = 4,
                    Feedback = "Nice car"
                },
                new Rating
                {
                    Id = 2,
                    AddedByUserId = userId1,
                    ApplicationUserId = userId0,
                    Value = 1,
                    Feedback = "Bad person"
                },
                new Rating
                {
                    Id = 3,
                    AddedByUserId = userId2,
                    ApplicationUserId = userId3,
                    Value = 5
                }
            };


            db.Entity<Rating>().HasData(ratings);



            var users = new List<ApplicationUser>()
            {
               new ApplicationUser
                {
                    Id = userId0,
                    Username = "misha_m",
                    FirstName = "Misho",
                    LastName = "Mishkov",
                    Email = "mishkov@misho.com",
                    EmailConfirmed = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                    PhoneNumber = "+35920768005",
                    //Rating = 1,
                    ApplicationRoleId = 2,
                    AddressId = 1
                },
                new ApplicationUser
                {
                    Id = userId1,
                    Username = "petio_p",
                    EmailConfirmed = true,
                    PhoneNumber = "+35924492877",
                    //Rating = 3,
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
                    //Rating = 4,
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
                    Username = "cicibar",
                    EmailConfirmed = true,
                    PhoneNumber = "+35924775508",
                    //Rating = 5,
                    ApplicationRoleId = 1,
                    FirstName = "Nikolaos",
                    LastName = "Tsitsibaris",
                    Email = "indebt@greece.gov",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                    AddressId = 1
                }
            };

            db.Entity<ApplicationUser>().HasData(users);

            var vehicles = new List<UserVehicle>()
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

            };

            db.Entity<UserVehicle>().HasData(vehicles);

            var bans = new List<Ban>()
            {
                new Ban
                {
                    Id = 1,
                    ApplicationUserId = userId1,
                    BlockedOn = System.DateTime.Today,
                    BlockedDue = System.DateTime.Today.AddDays(5)
                },
                new Ban
                {
                    Id = 2,
                    ApplicationUserId = userId1,
                    BlockedOn = System.DateTime.Today
                }
            };

            db.Entity<Ban>().HasData(bans);

            var trips = new List<Trip>()
            {
                new Trip
                {
                    Id = 1,
                    DriverId = userId0,
                    StartAddressId = 1,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 90,
                    Distance = 340,
                    PassengersCount = 2,
                    FreeSeats = 2
                },
                new Trip
                {
                    Id = 2,
                    DriverId = userId1,
                    StartAddressId = 2,
                    DestinationAddressId = 3,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKING"
                },
                new Trip
                {
                    Id = 3,
                    DriverId = userId2,
                    StartAddressId = 4,
                    DestinationAddressId = 2,
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 110,
                    Distance = 210,
                    PassengersCount = 2,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKING"
                },
                new Trip
                {
                    Id = 4,
                    DriverId = userId2,
                    StartAddressId = 4,
                    DestinationAddressId = 1,
                    DepartureTime = DateTime.Now,
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
                    DepartureTime = DateTime.Now,
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
                    DepartureTime = DateTime.Now,
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
                    DepartureTime = DateTime.Now,
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
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "CHEAP AND FAST"
                },
                new Trip
                {
                    Id = 9,
                    DriverId = userId0,
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
                    DriverId = userId1,
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
                    DriverId = userId2,
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
                    DepartureTime = DateTime.Now,
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
                    DepartureTime = DateTime.Now,
                    DurationInMinutes = 120,
                    Distance = 240,
                    Price = 12.23m,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "No kids"
                }
            };

            db.Entity<Trip>().HasData(trips);

            var pictures = new List<ProfilePicture>()
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
                }
            };

            db.Entity<ProfilePicture>().HasData(pictures);
        }
    }
}
