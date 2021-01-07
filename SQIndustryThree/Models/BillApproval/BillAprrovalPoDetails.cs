using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillAprrovalPoDetails
    {
        public int BillPoDetailskey { get; set; }
        public string PONumber { get; set; }
        public string SupllierName { get; set; }
        public string ArticleName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public float POQty { get; set; }
        public float Rate { get; set; }
        public float PoValue { get; set; }
        public float PIQty { get; set; }
        public float PIValue { get; set; }
        public float PIBalance { get; set; }
        public float PIRaised { get; set; }
        public float Discount { get; set; }
        public float TotalPayment { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? MasterKey { get; set; }
        public float? RemainsQty { get; set; }
    }
}