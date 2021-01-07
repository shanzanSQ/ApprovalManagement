using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class LogSection
    {
        public int LogId { get; set; }
        public string ActionBy { get; set; }
        public string ActionStatus { get; set; }
        public string ActionDate { get; set; }
        public string Comments { get; set; }
    }
}