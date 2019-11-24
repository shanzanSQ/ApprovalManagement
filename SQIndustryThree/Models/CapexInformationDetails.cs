using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CapexInformationDetails
    {
        public int CapexInfoDetailsId { get; set; }
        public string CapexAssetCatagory { get; set; }
        public string CapexAssetDescription { get; set; }
        public float CapexDetailsQty { get; set; }
        public float CapexUnitPrice { get; set; }
        public float CapexEstimatedCost { get; set; }
        public int CapexInfoId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}