using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class ChequeInfo
    {
        public int InvoiceKey { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalInvoiceQty { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public decimal NetValue { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal AllocatedValue { get; set; }

        public List<ChequeInfoDetails> ChequeInfoDetails { get; set; }
        public List<BillApproverModel> ApproverList { get; set; }
        public List<InvoiceInformation> BillInfoList { get; set; }
    }
}