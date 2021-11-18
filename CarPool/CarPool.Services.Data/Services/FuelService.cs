using CarPool.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class FuelService : IFuelService
    {
        private string fuellink = "https://bg.fuelo.net/fuel/type/gasoline";

        private HtmlAgilityPack.HtmlWeb _client;
        public FuelService()
        {
            _client = new HtmlAgilityPack.HtmlWeb();
        }

        public async Task<decimal> Price( int distance, double consumptionPer100km)
        {
           HtmlAgilityPack.HtmlDocument totext = await _client.LoadFromWebAsync(fuellink);
           
           var htmlElementAsArray = totext.DocumentNode.SelectSingleNode("//h2[@title='Средна цена за страната']").InnerText.Split();
           var pricePerLiter = double.Parse(htmlElementAsArray[0].Replace(',', '.'));
            decimal price = (decimal)((((double)distance) / 100) * (pricePerLiter * consumptionPer100km));
            return price; 
        }

    }
}
