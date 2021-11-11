using System.Collections.Generic;

namespace CarPool.Services.Mapping.DTOs
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string> Cities = new List<string>();
    }
}
