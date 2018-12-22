using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.User
{
    public class UserGroup
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
