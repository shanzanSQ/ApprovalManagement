using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillRequestModel
    {
        public int InvoiceKey { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string Supplier { get; set; }
        public bool IsFinalInvoice { get; set; }
        public decimal TotalInvoiceQty { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPaid { get; set; }
        public string Remarks { get; set; }
        public int IsApproved { get; set; }
    }
}