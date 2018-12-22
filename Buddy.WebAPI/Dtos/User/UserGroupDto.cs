using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.User
{
    public class UserGroupDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
