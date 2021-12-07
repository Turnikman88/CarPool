using CarPool.Services.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Test
{
    [TestClass]
    public class FuelServiceTest
    {
        [TestMethod]
        public async Task GetPriceTest()
        {

            var service = new FuelService();

            var result = await service.Price(123, 11.5);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(decimal));

        }
    }
}
