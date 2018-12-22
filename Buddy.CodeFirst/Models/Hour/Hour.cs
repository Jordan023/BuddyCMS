using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy.CodeFirst.Models.Hour
{
    public class Hour
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Project { get; set; }
        [StringLength(100)]
        public string Task { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public virtual User.User User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
