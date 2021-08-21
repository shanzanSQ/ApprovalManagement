using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class BuyerListModel
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int UnitId { get; set; }
        public string BusinessUnitName { get; set; }
    }
}