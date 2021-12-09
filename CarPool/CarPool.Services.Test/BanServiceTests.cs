using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class BanServiceTests
    {
        private CarPoolDBContext context;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

            CarPoolDBContext carPoolDBContext = new CarPoolDBContext(options);
            context = carPoolDBContext;
        }

        [TestMethod]
        public async Task BanUserAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);
            var result = await service.BanUserAsync("kalin@telerik.com", "knows too much", null);

            Assert.IsNotNull(result);
            Assert.AreEqual("knows too much", result.Reason);
        }

        [TestMethod]
        public async Task BanUserAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);
            var result = await service.BanUserAsync("", "knows too much", null);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAllBannedUsersAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ProfilePictures);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            await service.BanUserAsync("kalin@telerik.com", "knows too much", null);

            var result = await service.GetAllBannedUsersAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetAllBannedUsersAsync_WrongPage()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ProfilePictures);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.GetAllBannedUsersAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetTopReportedUsersAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ProfilePictures);
            await context.AddRangeAsync(Helper.Ratings);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.GetTopReportedUsersAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetReportedUserByEmailAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ProfilePictures);
            await context.AddRangeAsync(Helper.Ratings);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.GetReportedUserByEmailAsync("koksal@asd.tr");

            Assert.IsNotNull(result);
            Assert.AreEqual("Bad person", result.Reason);
        }

        [TestMethod]
        public async Task GetReportedUserByEmailAsync_NotReported()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ProfilePictures);
            await context.AddRangeAsync(Helper.Ratings);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.GetReportedUserByEmailAsync("petio@mvc.net");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Message);
        }

        [TestMethod]
        public async Task IgnoreReportAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Ratings);

            await context.SaveChangesAsync();

            var service = new BanService(context);
            var report = await context.Ratings
                .Where(x => x.ApplicationUser.Email == "koksal@asd.tr")
                .Select(x => x.IsReport)
                .FirstOrDefaultAsync();

            await service.IgnoreReportAsync("koksal@asd.tr");

            var reportIgnored = await context.Ratings
                .Where(x => x.ApplicationUser.Email == "koksal@asd.tr")
                .Select(x => x.IsReport)
                .FirstOrDefaultAsync();

            Assert.AreNotEqual(report, reportIgnored);
        }

        [TestMethod]
        public async Task UnbanUserAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.UnbanUserAsync("merkez@grece.com");

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Parse("9ef2aed3-8e58-4292-845b-ee59177499bb"), result.ApplicationUserId);
        }

        [TestMethod]
        public async Task UnbanUserAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.UnbanUserAsync("");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetMaxPageAsync()
        {
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new BanService(context);

            var result = await service.GetMaxPageAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }
    }
}
