using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.AirFreightTracker
{
    public class AirFreightMaster
    {
        public int AirFreightMasterId { get; set; }
        public int ExceptionMasterId { get; set; }
        public ExceptionRequestMaster ExceptionRequestMaster { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnit { get; set; }
        public int BuyersNameId { get; set; }
        public string BuyersName { get; set; }
        public int ForwarderId { get; set; }
        public string Forwarder { get; set; }
        public string ERDate { get; set; }
        public int FrieghtCostOnACOf { get; set; }
        public List<AirFreightDetails> AirFreightDetails { get; set; }
    }
}