using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Module
{
    public class Module
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
