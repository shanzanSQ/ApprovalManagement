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
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public string InvoiceTypeName { get; set; }
        public int InvoiceTypeID { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
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
        public double DiscountPercent { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal TotalAmount { get; set; }
        public double AdjustmentPercent { get; set; }
        public decimal AdjustmentAmt { get; set; }
        public double RetaintionPercent { get; set; }
        public decimal RetaintionAmt { get; set; }
        public decimal AdvTotal { get; set; }
        public double VATPercent { get; set; }
        public decimal VATAmt { get; set; }
        public double AITPercent { get; set; }
        public decimal AITAmt { get; set; }
        public decimal NetValue { get; set; }
        public int CapexInfoId { get; set; }
        public string CreateDate { get; set; }
        public int IsApproved { get; set; }
        public string UpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }

        public int CCID { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterOwner { get; set; }
        public string CostCenterDesignation { get; set; }

        public List<InvoiceInformation> BillInfoList { get; set; }
        //public ExceptionDetailsTable ExceptionDetails { get; set; }
        public List<BillFileUploadDetails> BillFilesList { get; set; }
        public List<POFileUploadDetails> POFilesList { get; set; }
        public List<GRNFileUploadDetails> GRNFilesList { get; set; }
        public List<BillApproverModel> ApproverList { get; set; }
        public List<BillComments> BillComments { get; set; }
        public List<LogSection> BillLogSection { get; set; }

        public List<ChequeInfoDetails> ChequeInfoDetails { get; set; }

    }
}