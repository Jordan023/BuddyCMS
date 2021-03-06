﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Log
{
    public class Log
    {
        public long Id { get; set; }
        [StringLength(20)]
        public string ModuleCode { get; set; }
        [StringLength(50)]
        public string FunctionName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public virtual LogType LogType { get; set; }
    }
}
