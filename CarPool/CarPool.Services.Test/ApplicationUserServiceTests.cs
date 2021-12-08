using CarPool.Common;
using CarPool.Data;
using CarPool.Services.Data.Services;
using CarPool.Services.Mapping.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class ApplicationUserServiceTests
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
        public async Task FilterUsersAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.FilterUsersAsync(0, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public async Task FilterUsersAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.FilterUsersAsync(0, "@@@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task FilterUsersAsync_NotFound_Page()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.FilterUsersAsync(100, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();
            var count = await context.ApplicationUsers.CountAsync();
            var service = new ApplicationUserService(context);
            var result = await service.DeleteAsync("kalin@telerik.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("kalin@telerik.com", result.Email);
            Assert.AreEqual(count - 1, await context.ApplicationUsers.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.DeleteAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetUserByEmailOrIdAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.GetUserByEmailOrIdAsync("kalin@telerik.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Kalin", result.FirstName);
        }

        [TestMethod]
        public async Task GetUserByEmailOrIdAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);
            await context.AddRangeAsync(Helper.Ratings);
            await context.AddRangeAsync(Helper.Trips);
            await context.AddRangeAsync(Helper.UserVehicles);
            await context.AddRangeAsync(Helper.Bans);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);
            var result = await service.GetUserByEmailOrIdAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);
            
            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);

            var obj = new ApplicationUserDTO
            {
                Username = "test",
                FirstName = "pesho",
                LastName = "peshev",
                Email = "jwr@mm.nh",
                PhoneNumber = "+3590759089",
                Password = "User123$",
                AddressId = 1
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Username);
        }

        [TestMethod]
        public async Task PostAsync_MissingData()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);

            var obj = new ApplicationUserDTO
            {
                Username = "test",
                FirstName = "pesho",
                LastName = "peshev",
                Email = "jwr@mm.nh",
                PhoneNumber = "+3590759089",
                Password = "User123$",
            };

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_ExistingData()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);

            var obj = new ApplicationUserDTO
            {
                Username = "test",
                FirstName = "pesho",
                LastName = "peshev",
                Email = "kalin@telerik.com",
                PhoneNumber = "+3590759089",
                Password = "User123$",
                AddressId = 1
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_UserNotFound()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);

            var obj = new ApplicationUserDTO
            {
                Username = "test"
            };


            var result = await service.UpdateAsync("", obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_UserEmailAlreadyExists()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);
            await context.AddRangeAsync(Helper.Addresses);

            await context.SaveChangesAsync();

            var service = new ApplicationUserService(context);

            var obj = new ApplicationUserDTO
            {
                Username = "test",
                Email = "kalin@telerik.com"
            };


            var result = await service.UpdateAsync("e", obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdatePasswordAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
            await context.AddRangeAsync(Helper.ApplicationRoles);

            await context.SaveChangesAsync();
            var pass = context.ApplicationUsers
                .Where(x => x.Email == "kalin@telerik.com")
                .Select(x => x.Password)
                .FirstOrDefaultAsync();

            var service = new ApplicationUserService(context);

            var result = await service.UpdatePasswordAsync("kalin@telerik.com", "Test123$");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(pass, result.Password);
        }

        [TestMethod]
        public async Task UsersCountAsync()
        {
            await context.AddRangeAsync(Helper.ApplicationUsers);
           
            await context.SaveChangesAsync();
           
            var service = new ApplicationUserService(context);

            var result = await service.UsersCountAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(await context.ApplicationUsers.CountAsync(), result);
        }
    }

}
