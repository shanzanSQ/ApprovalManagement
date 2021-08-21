using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.AirFreightTracker
{
    public class AirFreightFile
    {
        public int AirFreightFileId { get; set; }
        public int AirFreightDetailsId { get; set; }
        public AirFreightDetails AirFreightDetails { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ShortPath { get; set; }
        public string ServerFileName { get; set; }
    }
}