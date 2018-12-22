using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Address
{
    public class Country
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(3)]
        public string IsoCode { get; set; }
    }
}
