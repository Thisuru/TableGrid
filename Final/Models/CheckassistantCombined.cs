using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class CheckassistantCombined
    {
        public string UserTimestamp { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Datetime { get; set; }
        public string Score { get; set; }
        public string Channel { get; set; }
        public string Feedback { get; set; }
        public string Tag { get; set; }
        public string Updated_by { get; set; }
        public string Updated_time { get; set; }
    }
}