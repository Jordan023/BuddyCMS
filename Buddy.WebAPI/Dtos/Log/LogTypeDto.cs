using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.Log
{
    public class LogTypeDto
    {
        public long Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
    }
}
