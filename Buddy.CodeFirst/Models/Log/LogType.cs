using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Log
{
    public class LogType
    {
        public long Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
    }
}
