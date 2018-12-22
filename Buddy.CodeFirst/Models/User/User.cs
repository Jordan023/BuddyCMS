using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.User
{
    public class User
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
        public virtual Address.Address Address { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
