using System.ComponentModel.DataAnnotations;
using Buddy.WebAPI.Dtos.Address;

namespace Buddy.WebAPI.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string EmailAddress { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual UserGroupDto UserGroup { get; set; }
        public virtual UserLoginDto UserLogin { get; set; }
    }
}
