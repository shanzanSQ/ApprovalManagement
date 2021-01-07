using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillTransactionTable
    {
        public int BillTransKey { get; set; }
        public float? BillPoDetailskey { get; set; }
        public float? PIQty { get; set; }
        public float? PIValue { get; set; }
        public int? IsApproved { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}