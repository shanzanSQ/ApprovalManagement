using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillAllocationDetails
    {
        public int BillAllocationMasterId { get; set; }
        public int PaymentTransactionNo { get; set; }
        public int InvoiceKey { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string PONo { get; set; }
        public decimal BillAmount { get; set; }
        public decimal PaidSoFar { get; set; }
        public decimal Paid { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}