using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Address
{
    public class Address
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
        public virtual Country Country { get; set; }
    }
}
