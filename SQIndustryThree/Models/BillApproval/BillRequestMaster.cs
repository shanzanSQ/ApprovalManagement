using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillRequestMaster
    {
        public int InvoiceKey { get; set; }
        public string Requestor { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceTypeName { get; set; }
        public int InvoiceTypeID { get; set; }
        public string SupplierName { get; set; }
        public int SupplierID { get; set; }
        public string InvoiceDate { get; set; }
        public long POKey { get; set; }
        public int FinalInvoice { get; set; }
        public string Remarks { get; set; }
        public string Notes { get; set; }

        public decimal TotalQty { get; set; }
        public decimal TotalRate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal AlreadyQty { get; set; }

        public decimal TotalBalanceQty { get; set; }
        public decimal TotalInvoiceQty { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPaid { get; set; }
        public string CreateDate { get; set; }
        public int IsApproved { get; set; }
        public string UpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }

        public List<InvoiceInformation> BillInfoList { get; set; }
        //public ExceptionDetailsTable ExceptionDetails { get; set; }
        public List<BillFileUploadDetails> BillFilesList { get; set; }
        public List<IOUApproverModel> ApproverList { get; set; }
        public List<BillComments> BillComments { get; set; }
        public List<LogSection> BillLogSection { get; set; }

    }
}