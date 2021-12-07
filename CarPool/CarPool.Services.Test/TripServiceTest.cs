using CarPool.Data;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class TripServiceTest
    {
        private CarPoolDBContext mockDB;
        private IApplicationUserService mockAU;
        private IAddressService mockAds;
        private IBingApiService mockBing;
        private ICheckExistenceService mockCheck;
        private IFuelService mockFus;
        private IInboxService mockInbs;

        [TestInitialize]
        public void Init()
        {
            var mockDbContext = new Mock<CarPoolDBContext>();
            var mockDbSetTrips = Helper.Trips.AsQueryable().BuildMockDbSet();
            mockDbContext.Setup(db => db.Trips).Returns(mockDbSetTrips.Object);

            mockAU = new Mock<IApplicationUserService>().Object;
            mockDB = mockDbContext.Object;
            mockAds = new Mock<IAddressService>().Object;
            mockBing = new Mock<IBingApiService>().Object;
            mockCheck = new Mock<ICheckExistenceService>().Object;
            mockFus = new Mock<IFuelService>().Object;
            mockInbs = new Mock<IInboxService>().Object;
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            var service = new TripService(mockDB, mockCheck, mockAU, mockBing, mockAds, mockFus, mockInbs);

            var result = await service.GetAsync(0);
            var trips = mockDB.Trips.Take(10);
            Assert.IsNotNull(result);
            Assert.AreEqual(trips.Count(), result.Count());
        }
    }
}
