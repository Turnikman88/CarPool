using CarPool.Services.Mapping.Contracts;
using System.Collections.Generic;

namespace CarPool.Services.Mapping.DTOs
{
    public class CountryDTO : IErrorMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ErrorMessage { get; set; }


        public List<string> Cities = new List<string>();
    }
}
