using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillSubmitModel
    {
        public int MasterKey { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SupplierName { get; set; }
        public string BusinessUnitName { get; set; }
        public string UserName { get; set; }
        public string CreateDate { get; set; }
        public float POQty { get; set; }
        public float PoValue { get; set; }
        public float PIQty { get; set; }
        public float PIValue { get; set; }
        public float Discount { get; set; }
        public float TotalPayment { get; set; }
    }
}