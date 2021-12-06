using CarPool.Common;
using CarPool.Services.Data.Services;
using CarPool.Services.Data.Test;
using CarPool.Services.Mapping.DTOs;
using CarPool.Services.Mapping.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Services.Test
{
    [TestClass]
    public class CountryServiceTest
    {
        [TestMethod]
        public async Task GetAllCountries()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetAsync(0);

            var countryFromDb = mockDbContext.Object.Countries.Take(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(countryFromDb.Count(), result.Count());
        }

        [TestMethod]
        public async Task RenderCountries()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.RenderCountryListAsync();

            var countryFromDb = mockDbContext.Object.Countries;

            Assert.IsNotNull(result);
            Assert.AreEqual(countryFromDb.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetCountryByIdShouldGetCorrectObject()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetCountryByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Bulgaria", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public async Task GetCountryByPartNameShouldGetCorrectObject()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetCountriesByPartNameAsync(0, "Bul");

            var countryFromDb = mockDbContext.Object.Countries.FirstOrDefault(x => x.Name.Contains("Bul"));

            Assert.IsNotNull(result);
            Assert.AreEqual(countryFromDb.Name, result.First().Name);
            Assert.AreEqual(countryFromDb.Id, result.First().Id);
        }
        [TestMethod]
        public async Task GetCountryByWrongIdShouldGetErrorMessage()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetCountryByIdAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetCountryByNameShouldGetCorrectObject()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetCountryByNameAsync("Bulgaria");

            Assert.IsNotNull(result);
            Assert.AreEqual("Bulgaria", result.Name);
        }

        [TestMethod]
        public async Task GetCountryByWrongNameShouldGetErrorMessage()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.GetCountryByNameAsync("WrongCountry");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task DeleteCountryTestSuccessful()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            Assert.IsTrue(mockDbContext.Object.Countries.FirstOrDefault(x => x.Id == 1).DeletedOn == null);

            var result = await service.DeleteAsync(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Bulgaria", result.Name);
            Assert.IsTrue(mockDbContext.Object.Countries.FirstOrDefault(x => x.Id == 1).DeletedOn != null);
        }

        [TestMethod]
        public async Task DeleteCountryTestFail()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            var countBeforeAction = mockDbContext.Object.Countries.Count();
            var result = await service.DeleteAsync(100);
            var countAfterAction = mockDbContext.Object.Countries.Count();
            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_NOT_FOUND, result.ErrorMessage);
            Assert.IsTrue(countAfterAction == countBeforeAction);
        }

        [TestMethod]
        public async Task PostCountryNullObject()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var countBeforeAction = mockDbContext.Object.Countries.Count();
            var result = await service.PostAsync(null);
            var countAfterAction = mockDbContext.Object.Countries.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.INCORRECT_DATA, result.ErrorMessage);
            Assert.IsTrue(countAfterAction == countBeforeAction);
        }

        [TestMethod]
        public async Task PostCountryEmptyNameObject()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var countBeforeAction = mockDbContext.Object.Countries.Count();
            var result = await service.PostAsync(new CountryDTO { Name = "" });
            var countAfterAction = mockDbContext.Object.Countries.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.INCORRECT_DATA, result.ErrorMessage);
            Assert.IsTrue(countAfterAction == countBeforeAction);
        }

        [TestMethod]
        public async Task PostCountryCountryExists()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.PostAsync(new CountryDTO { Name = "Bulgaria" });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostCountrySuccessful()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var testCountry = new CountryDTO { Name = "TestCountry" };
            var result = await service.PostAsync(testCountry);

            Assert.IsNotNull(result);
            Assert.IsNull(result.ErrorMessage);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public async Task PostDeletedCountrySuccessful()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            var countBeforeAction = mockDbContext.Object.Countries.Count();

            var deleteCountryBeforeTest = await service.DeleteAsync(1);


            var testCountry = new CountryDTO { Name = $"{deleteCountryBeforeTest.Name}" };
            var result = await service.PostAsync(testCountry);
            var countAfterAction = mockDbContext.Object.Countries.Count();

            Assert.IsNotNull(result);
            Assert.IsNull(result.ErrorMessage);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(countBeforeAction, countAfterAction);
            Assert.AreEqual(deleteCountryBeforeTest.Name, result.Name);
            Assert.AreEqual(deleteCountryBeforeTest.Id, result.Id);
        }

        [TestMethod]
        public async Task UpdateCountryExists()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            var existingCountry = mockDbContext.Object.Countries.First();

            var result = await service.UpdateAsync(existingCountry.Id, existingCountry.GetDTO());

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateCountryIncorrectDataName()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            var existingCountry = mockDbContext.Object.Countries.First();

            var result = await service.UpdateAsync(existingCountry.Id, new CountryDTO { Name = "" });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.INCORRECT_DATA, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateCountryIncorrectId()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);

            var result = await service.UpdateAsync(10000, new CountryDTO { Name = "UpdateCountryIncorrectId" });

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.COUNTRY_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateCountryCorrectData()
        {
            var mockDbContext = Helper.MockDbContext;

            var service = new CountryService(mockDbContext.Object);
            var existingCountry = mockDbContext.Object.Countries.First();

            var result = await service.UpdateAsync(existingCountry.Id, new CountryDTO { Name = "UpdatedCountry" });

            Assert.IsNotNull(result);
            Assert.AreEqual(existingCountry.Id, result.Id);
            Assert.AreEqual("UpdatedCountry", result.Name);
        }
    }
}
