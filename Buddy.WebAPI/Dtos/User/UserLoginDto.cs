using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.User
{
    public class UserLoginDto
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
