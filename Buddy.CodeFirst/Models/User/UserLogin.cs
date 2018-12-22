using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.User
{
    public class UserLogin
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string PasswordSalt { get; set; }
        [StringLength(100)]
        public string PasswordHash { get; set; }

    }
}
