using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.Address
{
    public class CountryDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(3)]
        public string IsoCode { get; set; }
    }
}
