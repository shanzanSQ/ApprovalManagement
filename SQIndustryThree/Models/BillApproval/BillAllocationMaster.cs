using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillAllocationMaster
    {
        public int BillAllocationMasterId { get; set; }
        public long PaymentTransactionNo { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmount { get; set; }
        public string ChequeDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int CreatedBy { get; set; }

        public List<BillAllocationDetails> AllocationDetails { get; set; }
    }
}