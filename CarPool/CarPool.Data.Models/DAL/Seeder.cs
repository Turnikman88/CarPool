using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
                    Latitude = 42.698334M,
                    Longitude = 23.319941M
                },
                new Address
                {
                    Id = 2,
                    CityId = 2,
                    StreetName = "blv. Iztochen 23",
                    Latitude = 42.682073M, 
                    Longitude = 23.326622M
                },
                new Address
                {
                    Id = 3,
                    CityId = 3,
                    StreetName = "blv. Halic 12",
                    Latitude = 42.698334M,
                    Longitude = 23.254942M
                },
                new Address
                {
                    Id = 4,
                    CityId = 4,
                    StreetName = "blv. Zeus 12",
                    Latitude = 42.711242M, 
                    Longitude = 23.316655M
                },
                new Address
                {
                    Id = 5,
                    CityId = 5,
                    StreetName = "blv. Romunska Morava 1",
                    Latitude = 42.625045M, 
                    Longitude = 23.400539M
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
                    Password = "12345678",
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
                    Password = "123456789",
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
                    Password = "1234567899",
                    AddressId = 3
                },
                new ApplicationUser
                {
                    Id = userId3,
                    Username = "cicibar",
                    EmailConfirmed = true,
                    PhoneNumber = "+35924775508",
                    //Rating = 5,
                    ApplicationRoleId = 2,
                    FirstName = "Nikolaos",
                    LastName = "Tsitsibaris",
                    Email = "indebt@greece.gov",
                    Password = "12345678999",
                    AddressId = 4
                }
            };

            db.Entity<ApplicationUser>().HasData(users);

            var bans = new List<Ban>()
            {
                new Ban
                {
                    Id = 1,                   
                    ApplicationUserId = userId1,
                    IsPermanentBlock = false,
                    BlockedOn = System.DateTime.Today,
                    BlockedDue = System.DateTime.Today.AddDays(5)
                },
                new Ban
                {
                    Id = 2,
                    ApplicationUserId = userId1,
                    IsPermanentBlock = true
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
                    ArrivalTime = DateTime.Now.AddHours(3),
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
                    ArrivalTime = DateTime.Now.AddHours(2),
                    Distance = 240,
                    PassengersCount = 1,
                    FreeSeats = 2,
                    AdditionalComment = "NO SMOKEING"
                }
            };

            db.Entity<Trip>().HasData(trips);

            var pictures = new List<ProfilePicture>()
            {
                new ProfilePicture
                {
                    Id = 1,
                    ApplicationUserId = userId0,
                    //ImageData = 
                }
            };

            db.Entity<ProfilePicture>().HasData(pictures);
        }
    }
}
