using Buddy.WebAPI.Dtos.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.Hour
{
    public class HourDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Project { get; set; }
        [StringLength(100)]
        public string Task { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public virtual UserDto User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
