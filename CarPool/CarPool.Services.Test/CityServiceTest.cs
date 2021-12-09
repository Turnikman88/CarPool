using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data;
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
    public class CityServiceTest
    {
        private Mock<ICountryService> cs;
        private Mock<ICheckExistenceService> check;
        private CarPoolDBContext context;

        [TestInitialize]
        public void Init()
        {
            cs = new Mock<ICountryService>();
            check = new Mock<ICheckExistenceService>();

            var options = new DbContextOptionsBuilder<CarPoolDBContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

            CarPoolDBContext carPoolDBContext = new CarPoolDBContext(options);
            context = carPoolDBContext;
        }

        [TestMethod]
        public async Task GetAllCities()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public async Task GetAllCitiesZeroReturn()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetAsync(20);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetCityByIdAsync()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCityByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Sofia", result.Name);
        }

        [TestMethod]
        public async Task GetCityByIdAsync_NotExisting()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCityByIdAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.CITY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetCityByNameAsync()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCityByNameAsync("Sofia");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public async Task GetCityByNameAsync_NotExisting()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCityByNameAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.CITY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetCitiesByPartNameAsync()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCitiesByPartNameAsync(0, "s");

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
        }

        [TestMethod]
        public async Task GetCitiesByPartNameAsync_NotFound()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCitiesByPartNameAsync(0, "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetCitiesByCountryNameAsync()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);

            var result = await service.GetCitiesByCountryNameAsync(0, "bul");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            var service = new CityService(context, check.Object, cs.Object);
            var citiesCount = await context.Cities.CountAsync();
            var obj = new CityDTO
            {
                Name = "New City",
                CountryId = 1
            };
            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("New City", result.Name);
            Assert.AreEqual(1, result.CountryId);
            Assert.AreEqual(citiesCount + 1, await context.Cities.CountAsync());
        }

        [TestMethod]
        public async Task PostAsync_EmptyName()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);
            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            var service = new CityService(context, check.Object, cs.Object);
            var obj = new CityDTO
            {
                Name = "",
                CountryId = 1
            };
            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.INCORRECT_DATA, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_DeletedCity()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            var deletedCity = await context.Cities.FirstOrDefaultAsync(x => x.Id == 1);
            deletedCity.IsDeleted = true;

            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            var service = new CityService(context, check.Object, cs.Object);
            var obj = new CityDTO
            {
                Name = "Sofia",
                CountryId = 1
            };
            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("Sofia", result.Name);
        }

        [TestMethod]
        public async Task UpdatedCity()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            var service = new CityService(context, check.Object, cs.Object);
            var obj = new CityDTO
            {
                Name = "Sofia1",
                CountryId = 1
            };
            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("Sofia1", result.Name);
        }

        [TestMethod]
        public async Task UpdatedCity_CityExists()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            check.Setup(x => x.CityExistsAsync("Sofia", 1)).ReturnsAsync(true);

            var service = new CityService(context, check.Object, cs.Object);

            var obj = new CityDTO
            {
                Name = "Sofia",
                CountryId = 1
            };

            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.CITY_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdatedCity_WrongId()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            cs.Setup(x => x.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(new CountryDTO { Id = 1, Name = "Bulgaria" });

            check.Setup(x => x.CheckId(0)).Throws<AppException>();

            var service = new CityService(context, check.Object, cs.Object);

            var obj = new CityDTO
            {
                Name = "Sofia",
                CountryId = 1
            };

            await Assert.ThrowsExceptionAsync<AppException>(async () => await service.UpdateAsync(0, obj));
        }

        [TestMethod]
        public async Task DeleteCity()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            var count = await context.Cities.CountAsync();
            var service = new CityService(context, check.Object, cs.Object);
            var result = await service.DeleteAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Sofia", result.Name);
            Assert.AreEqual(count - 1, await context.Cities.CountAsync());
        }

        [TestMethod]
        public async Task DeleteCity_NotFound()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            var service = new CityService(context, check.Object, cs.Object);
            var result = await service.DeleteAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.CITY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task DeleteCity_WrongId()
        {
            await context.AddRangeAsync(Helper.Countries);
            await context.AddRangeAsync(Helper.Cities);

            await context.SaveChangesAsync();

            check.Setup(x => x.CheckId(0)).Throws<AppException>();

            var service = new CityService(context, check.Object, cs.Object);

            await Assert.ThrowsExceptionAsync<AppException>(async () => await service.DeleteAsync(0));
        }
    }
}
