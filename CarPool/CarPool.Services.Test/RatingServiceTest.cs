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

            var result = await service.PostReportAsync(
                new RatingDTO
                {
                    TripId = existingTrip.TripId,
                    AddedByUserId = existingTrip.AddedByUserId,
                    ApplicationUserId = existingTrip.ApplicationUserId
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_ALREADY_REPORTED, result.ErrorMessage);
        }

        [TestMethod]
        public async Task TryReportYourself()
        {
            var guid = Guid.Parse("7fed582a-1c94-4741-8a80-9f881e089673");

            var service = new RatingService(mockDB);

            var result = await service.PostReportAsync(
                new RatingDTO
                {
                    TripId = 1,
                    AddedByUserId = guid,
                    ApplicationUserId = guid
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_YOU_CANNOT_REVEIW_YOURSELF, result.ErrorMessage);
        }

        [TestMethod]
        public async Task ReportSuccess()
        {
            var guidAddedBy = Guid.Parse("7fed582a-1c94-4741-8a80-9f881e089673");
            var guidAppUser = Guid.Parse("1a543b94-eaa9-47a4-bc16-fde7189d0384");

            var service = new RatingService(mockDB);

            var result = await service.PostReportAsync(
                new RatingDTO
                {
                    TripId = 1,
                    AddedByUserId = guidAddedBy,
                    ApplicationUserId = guidAppUser
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.AddedByUserId, guidAddedBy);
            Assert.AreEqual(result.ApplicationUserId, guidAppUser);
        }

        [TestMethod]
        public async Task TryRePostExistingFeedback()
        {
            var service = new RatingService(mockDB);

            var existingTrip = mockDB.Ratings.First();

            var result = await service.PostFeedbackAsync(
                new RatingDTO
                {
                    TripId = existingTrip.TripId,
                    AddedByUserId = existingTrip.AddedByUserId,
                    ApplicationUserId = existingTrip.ApplicationUserId
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_ALREADY_REVIEWED, result.ErrorMessage);
        }

        [TestMethod]
        public async Task TryFeedbackForYourself()
        {
            var guid = Guid.Parse("7fed582a-1c94-4741-8a80-9f881e089673");

            var service = new RatingService(mockDB);

            var result = await service.PostFeedbackAsync(
                new RatingDTO
                {
                    TripId = 1,
                    AddedByUserId = guid,
                    ApplicationUserId = guid
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.TRIP_YOU_CANNOT_REVEIW_YOURSELF, result.ErrorMessage);
        }

        [TestMethod]
        public async Task FeedbackSuccess()
        {
            var guidAddedBy = Guid.Parse("7fed582a-1c94-4741-8a80-9f881e089673");
            var guidAppUser = Guid.Parse("1a543b94-eaa9-47a4-bc16-fde7189d0384");
            var feedback = "TestFeedback";
            var service = new RatingService(mockDB);

            var result = await service.PostFeedbackAsync(
                new RatingDTO
                {
                    TripId = 1,
                    AddedByUserId = guidAddedBy,
                    ApplicationUserId = guidAppUser,
                    Value = 5,
                    Feedback = feedback
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.AddedByUserId, guidAddedBy);
            Assert.AreEqual(result.ApplicationUserId, guidAppUser);
            Assert.AreEqual(result.Value, 5);
            Assert.AreEqual(result.Feedback, feedback);
        }

    }
}
