using CarPool.Common;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Data.Services;
using CarPool.Services.Mapping.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class TripServiceTest
    {
        private Mock<IApplicationUserService> mockAU;
        private Mock<IAddressService> mockAds;
        private Mock<IBingApiService> mockBing;
        private Mock<ICheckExistenceService> mockCheck;
        private Mock<IFuelService> mockFus;
        private Mock<IInboxService> mockInbs;
        private CarPoolDBContext context;
        private TripService service;

        [TestInitialize]
        public void Init()
        {
            mockAU = new Mock<IApplicationUserService>();
            mockAds = new Mock<IAddressService>();
            mockBing = new Mock<IBingApiService>();
            mockCheck = new Mock<ICheckExistenceService>();
            mockFus = new Mock<IFuelService>();
            mockInbs = new Mock<IInboxService>();
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetAsyncTest))
                            .Options;
            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var result = await service.GetAsync(0);
            var expectedTripCount = context.Trips.Take(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripCount.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetAllUpcomingTripsAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetAllUpcomingTripsAsyncTest))
                            .Options;
            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var result = await service.GetAllUpcomingTripsAsync(0);
            var expectedTripCount = context.Trips.Where(x => x.DepartureTime >= DateTime.Today.Date).Take(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripCount.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetPageCountAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetPageCountAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.SaveChangesAsync();

            var result = await service.GetPageCountAsync();
            var expectedTripCount = await context.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date).CountAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripCount / GlobalConstants.PageSkip, result);
        }

        [TestMethod]
        public async Task GetPageCountPerUserAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetPageCountPerUserAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var user = await context.ApplicationUsers.FirstAsync();
            var result = await service.GetPageCountPerUserAsync(user.Email);
            var expectedTripsCount = user.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date).Count() / GlobalConstants.PageSkip;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripsCount, result);
        }

        [TestMethod]
        public async Task GetPastByUserTripsTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetPastByUserTripsTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var user = await context.ApplicationUsers.FirstAsync();
            var result = await service.GetPastByUserTrips(0, user.Email);
            var expectedTripsCount = user.Trips.Where(x => x.DepartureTime.Date < DateTime.Today.Date).Take(10).Count() / GlobalConstants.PageSkip;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripsCount, result.Count());
        }

        [TestMethod]
        public async Task GetUpcomingTripsByUserAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetUpcomingTripsByUserAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var user = await context.ApplicationUsers.FirstAsync();
            var result = await service.GetUpcomingTripsByUserAsync(0, user.Email);
            var expectedTripsCount = user.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date).Take(10).Count() / GlobalConstants.PageSkip;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripsCount, result.Count());
        }

        [TestMethod]
        public async Task GetUpcomingTripsByUserAsDriverAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetUpcomingTripsByUserAsDriverAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var user = await context.ApplicationUsers.FirstAsync();
            var result = await service.GetUpcomingTripsByUserAsDriverAsync(0, user.Email);
            var expectedTripsCount = user.Trips.Where(x => x.DepartureTime.Date >= DateTime.Today.Date && x.DriverId == user.Id).Take(10).Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTripsCount, result.Count());
        }

        [TestMethod]
        public async Task GetTripByIDAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(GetTripByIDAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var trip = await context.Trips.FirstAsync();
            var result = await service.GetTripByIDAsync(trip.Id);


            Assert.IsNotNull(result);
            Assert.AreEqual(trip.Id, result.Id);
            Assert.AreEqual(trip.DriverId.ToString(), result.DriverId);
        }

        [TestMethod]
        public async Task PostAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(PostAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            //mockAds.Setup(x => x.AddressToId(It.IsAny<AddressDTO>())).ReturnsAsync(1);
            mockAds.Setup(x => x.AddressToId(It.Is<AddressDTO>(x => x.CityName == "Sofia" && x.CountryName == "Bulgaria"))).ReturnsAsync(1);
            mockAds.Setup(x => x.AddressToId(It.Is<AddressDTO>(x => x.CityName == "Plovdiv" && x.CountryName == "Bulgaria"))).ReturnsAsync(2);

            mockAds.Setup(x => x.GetAddressByIdAsync(It.Is<int>(x => x == 1))).ReturnsAsync(new AddressDTO { AddressId = 1, CityId = 1, CityName = "Sofia", CountryId = 1, CountryName = "Bulgaria", Latitude = 42.697673M, Longitude = 23.321718M });
            mockAds.Setup(x => x.GetAddressByIdAsync(It.Is<int>(x => x == 2))).ReturnsAsync(new AddressDTO { AddressId = 2, CityId = 2, CityName = "Plovdiv", CountryId = 1, CountryName = "Bulgaria", Latitude = 42.143590M, Longitude = 24.751549M });
            mockBing.Setup(x => x.GetTripDataByCoordinatesAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((90, 90));

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();


            var user = await context.ApplicationUsers.FirstAsync();
            var result = await service.PostAsync(
                new TripDTO
                {
                    DriverId = user.Id.ToString(),
                    DepartureTime = DateTime.UtcNow.AddDays(1),
                    StartAddressCity = "Sofia",
                    StartAddressCountry = "Bulgaria",
                    StartAddressStreet = "Vasil Levski 14",
                    DestinationAddressCountry = "Bulgaria",
                    DestinationAddressCity = "Plovdiv",
                    DestinationAddressStreet = "bulevard Iztochen 23",
                    FreeSeats = 2,
                    AdditionalComment = "TestCommentHere"
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.DriverId, user.Id.ToString());
            Assert.AreEqual(result.AdditionalComment, "TestCommentHere");
            Assert.AreEqual(result.DepartureTime.Date, DateTime.UtcNow.Date.AddDays(1));
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(UpdateAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            mockAds.Setup(x => x.AddressToId(It.Is<AddressDTO>(x => x.CityName == "Sofia" && x.CountryName == "Bulgaria"))).ReturnsAsync(1);
            mockAds.Setup(x => x.AddressToId(It.Is<AddressDTO>(x => x.CityName == "Plovdiv" && x.CountryName == "Bulgaria"))).ReturnsAsync(2);

            mockAds.Setup(x => x.GetAddressByIdAsync(It.Is<int>(x => x == 1))).ReturnsAsync(new AddressDTO { AddressId = 1, CityId = 1, CityName = "Sofia", CountryId = 1, CountryName = "Bulgaria", Latitude = 42.697673M, Longitude = 23.321718M });
            mockAds.Setup(x => x.GetAddressByIdAsync(It.Is<int>(x => x == 2))).ReturnsAsync(new AddressDTO { AddressId = 2, CityId = 2, CityName = "Plovdiv", CountryId = 1, CountryName = "Bulgaria", Latitude = 42.143590M, Longitude = 24.751549M });
            mockBing.Setup(x => x.GetTripDataByCityCountryAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((90, 90));

            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.TripPassengers);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var oldTrip = await context.Trips.FirstOrDefaultAsync(x => x.Id == 1);
            var result = await service.UpdateAsync(oldTrip.Id,
                new TripDTO
                {
                    DriverId = oldTrip.DriverId.ToString(),
                    DepartureTime = DateTime.UtcNow.AddDays(10),
                    StartAddressCity = "Sofia",
                    StartAddressCountry = "Bulgaria",
                    StartAddressStreet = "Vasil Levski 14",
                    DestinationAddressCountry = "Bulgaria",
                    DestinationAddressCity = "Plovdiv",
                    DestinationAddressStreet = "bulevard Iztochen 23",
                    FreeSeats = 2,
                    AdditionalComment = "TestCommentHere"
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.DriverId, oldTrip.DriverId.ToString());
            Assert.AreEqual(result.AdditionalComment, "TestCommentHere");
            Assert.AreEqual(result.DepartureTime.Date, DateTime.UtcNow.Date.AddDays(10));

        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(DeleteAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);


            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var tripsBefore = await context.Trips.CountAsync();
            var firstTrip = await context.Trips.FirstAsync();
            var result = await service.DeleteAsync(firstTrip.Id);
            var tripsAfter = await context.Trips.CountAsync();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(tripsBefore, tripsAfter);
            Assert.AreEqual(result.DriverId.ToString(), firstTrip.DriverId.ToString());
            Assert.AreEqual(result.AdditionalComment, firstTrip.AdditionalComment);
        }

        [TestMethod]
        public async Task JoinTripAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(JoinTripAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);

            var appusertotest = new ApplicationUser
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@Test.com",
                EmailConfirmed = true,
                Password = BCrypt.Net.BCrypt.HashPassword("Test123$"),
                PhoneNumber = "+35920761111",
                ApplicationRoleId = 2,
                AddressId = 1
            };
            context.ApplicationUsers.Add(appusertotest);
            mockAU.Setup(x => x.GetUserByEmailOrIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUserDTO { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), FirstName = "Test", LastName = "Test", PhoneNumber = "TestNumber" });

            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var firstTrip = await context.Trips.FirstAsync();
            var result = await service.JoinTripAsync(firstTrip.Id, "TestMail");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.PassengersNameAndPhone.Contains("Test Test +35920761111"));
        }

        [TestMethod]
        public async Task LeaveTripAsynTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(LeaveTripAsynTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);

            var appusertotest = new ApplicationUser
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@Test.com",
                EmailConfirmed = true,
                Password = BCrypt.Net.BCrypt.HashPassword("Test123$"),
                PhoneNumber = "+35920761111",
                ApplicationRoleId = 2,
                AddressId = 1
            };
            mockAU.Setup(x => x.GetUserByEmailOrIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUserDTO { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), FirstName = "Test", LastName = "Test", PhoneNumber = "TestNumber" });
            await context.SaveChangesAsync();

            var firstTrip = await context.Trips.FirstOrDefaultAsync();


            var trippassengertotest = new TripPassenger
            {
                ApplicationUserId = appusertotest.Id,
                TripId = firstTrip.Id,
                CreatedOn = DateTime.UtcNow
            };
            await context.TripPassengers.AddAsync(trippassengertotest);

            await context.ApplicationUsers.AddAsync(appusertotest);
            await context.SaveChangesAsync();

            var result = await service.LeaveTripAsync(firstTrip.Id, "TestMail");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task KickUserAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                            .UseInMemoryDatabase(nameof(KickUserAsyncTest))
                            .Options;

            context = new CarPoolDBContext(options);
            service = new TripService(context, mockCheck.Object, mockAU.Object, mockBing.Object, mockAds.Object, mockFus.Object, mockInbs.Object);


            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Cities);
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.SaveChangesAsync();

            var guidtotest = "0f8fad5b-d9cb-469f-a165-70867728950e";
            var firstTrip = await context.Trips.FirstOrDefaultAsync();

            var appusertotest = new ApplicationUser
            {
                Id = Guid.Parse(guidtotest),
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@Test.com",
                EmailConfirmed = true,
                Password = BCrypt.Net.BCrypt.HashPassword("Test123$"),
                PhoneNumber = "+35920761111",
                ApplicationRoleId = 2,
                AddressId = 1
            };
            mockAU.Setup(x => x.GetUserByEmailOrIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUserDTO { Id = firstTrip.DriverId });
            await context.SaveChangesAsync();


            var trippassengertotest = new TripPassenger
            {
                ApplicationUserId = appusertotest.Id,
                TripId = firstTrip.Id,
                CreatedOn = DateTime.UtcNow
            };
            await context.TripPassengers.AddAsync(trippassengertotest);

            await context.ApplicationUsers.AddAsync(appusertotest);
            await context.SaveChangesAsync();

            var result = await service.KickUserAsync(guidtotest, firstTrip.Id, firstTrip.Driver.Email);

            Assert.IsTrue(result);
        }
    }
}
