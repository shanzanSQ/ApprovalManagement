using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillQuality
    {
        public int QualityID { get; set; }
        public string QualityParam { get; set; }
        public string QualityResult { get; set; }
        public string QualityComment { get; set; }
        public int Rate { get; set; }
        public string RateName { get; set; }
        public string FileName { get; set; }
        public string FilPath { get; set; }
        public int InvoiceKey { get; set; }
        public string InvoiceNo { get; set; }
        public string UserName { get; set; }
    }
}