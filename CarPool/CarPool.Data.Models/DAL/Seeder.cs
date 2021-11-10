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
        }
    }
}
