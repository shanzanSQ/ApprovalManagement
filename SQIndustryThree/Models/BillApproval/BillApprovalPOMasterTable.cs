using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillApprovalPOMasterTable
    {
        public int MasterKey { get; set; }
        public int Status { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SupplierName { get; set; }
        public string UserName { get; set; }
        public int BusinessUnitId { get; set; }
        public int CategoryId { get; set; }
        public float? AdvancedPayment { get; set; }
        public float? POQty { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<BillAprrovalPoDetails> Polist { get; set; }
        public List<BillApproverModel> Approverlist { get; set; }
    }
}