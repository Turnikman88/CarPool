using CarPool.Services.Data.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Services
{
    public class BingApiService : IBingApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        private HttpClient client;

        private const string locationUrl = "https://dev.virtualearth.net/REST/v1/Locations/{0}?key=AqG6aRdZq-X3Rc948On3Sxvazt4N13KooHtpAnMOmlnbdvUh4ki_DBYwwaIlzexm";
        private const string distanceMatrixUrl = "https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?origins={0}&destinations={1}&travelMode=driving&key=AqG6aRdZq-X3Rc948On3Sxvazt4N13KooHtpAnMOmlnbdvUh4ki_DBYwwaIlzexm";

        public BingApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.client = this.httpClientFactory.CreateClient();
        }
        public async Task<(int, int)> GetTripDataAsync(string origin, string destination)
        {
            var originJson = await (await client.GetAsync(string.Format(locationUrl, origin))).Content.ReadAsStringAsync(); // Get origin JSON
            dynamic originData = JsonConvert.DeserializeObject<ExpandoObject>(originJson, new ExpandoObjectConverter());    // Deserialize
            var originCoords = originData.resourceSets[0].resources[0].point.coordinates;                                   // get latitude and longitude in array
            var originCoordsString = $"{originCoords[0]},{originCoords[1]}";                                                // get it as string so we can use it later

            var destinationJson = await (await client.GetAsync(string.Format(locationUrl, destination))).Content.ReadAsStringAsync();  // exact same procedure
            dynamic destinationData = JsonConvert.DeserializeObject<ExpandoObject>(destinationJson, new ExpandoObjectConverter());     // exact same procedure
            var destinationCoords = destinationData.resourceSets[0].resources[0].point.coordinates;                                    // exact same procedure
            var destinationCoordsString = $"{destinationCoords[0]},{destinationCoords[1]}";                                            // exact same procedure

            var travelDataResult = await client.GetAsync(string.Format(distanceMatrixUrl, originCoordsString, destinationCoordsString)); // to get distance bing api works only with latitude and longituted
            var travelDataJson = await travelDataResult.Content.ReadAsStringAsync();                                                       
            dynamic travelData = JsonConvert.DeserializeObject<ExpandoObject>(travelDataJson, new ExpandoObjectConverter());

            var distance = (int)travelData.resourceSets[0].resources[0].results[0].travelDistance;
            var duration = (int)travelData.resourceSets[0].resources[0].results[0].travelDuration;

            return (distance, duration);
        }
    }
}
