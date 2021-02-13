using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class InvoiceInformation
    {
        public int PODetailsKey { get; set; }
        public string PO { get; set; }
        public string Article { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public int UnitID { get; set; }
        public decimal POQty { get; set; }
        public decimal Rate { get; set; }
        public decimal POValue { get; set; }
        public decimal InvoiceQty { get; set; }
        public decimal InitialQty { get; set; }
        public decimal InvoiceBalance { get; set; }
        public decimal InvoiceValue { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}