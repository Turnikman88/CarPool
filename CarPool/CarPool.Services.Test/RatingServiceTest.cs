using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Services;
using CarPool.Services.Mapping.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class RatingServiceTest
    {
        private CarPoolDBContext mockDB;

        [TestInitialize]
        public void Init()
        {
            var mockDbContext = new Mock<CarPoolDBContext>();
            var mockDbSetRatings = Helper.Ratings.AsQueryable().BuildMockDbSet();
            mockDbContext.Setup(db => db.Ratings).Returns(mockDbSetRatings.Object);
            mockDB = mockDbContext.Object;
        }

        [TestMethod]
        public async Task TryRePostExistingReport()
        {
            var service = new RatingService(mockDB);

            var existingTrip = mockDB.Ratings.First(x => x.IsReport == true);

            var result = await service.PostReportAsync(new RatingDTO { TripId = existingTrip.TripId, AddedByUserId = existingTrip.AddedByUserId, ApplicationUserId = existingTrip.ApplicationUserId });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_ALREADY_REPORTED, result.ErrorMessage);
        }

        [TestMethod]
        public async Task TryReportYourself()
        {
            var guid = new Guid();
            var mockDbContext = Helper.MockDbContext;

            var service = new RatingService(mockDbContext.Object);

            var result = await service.PostReportAsync(new RatingDTO { TripId = 1, AddedByUserId = guid, ApplicationUserId = guid });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_YOU_CANNOT_REVEIW_YOURSELF, result.ErrorMessage);
        }
    }
}
