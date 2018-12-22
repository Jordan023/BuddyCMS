using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        [StringLength(10)]
        public string StreetNumber { get; set; }
        [StringLength(50)]
        public string Postal { get; set; }
        [StringLength(50)]
        public string Province { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        public virtual CountryDto Country { get; set; }
    }
}
