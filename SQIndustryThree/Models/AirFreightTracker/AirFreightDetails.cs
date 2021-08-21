using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.AirFreightTracker
{
    public class AirFreightDetails
    {
        public int AirFreightDetailsId { get; set; }
        public int AirFreightMasterId { get; set; }
        public AirFreightMaster AirFreightMaster { get; set; }
        public int ExceptionGenralId { get; set; }
        public ExceptionGenaralInformation ExceptionGenaralInformation { get; set; }
        public string PO { get; set; }
        public string ParentPO { get; set; }
        public int ModeOfShipmentId { get; set; }
        public string ModeOfShipment { get; set; }
        public string PortOfDestination { get; set; }
        public string CountryOfDestination { get; set; }
        public int IncotermId { get; set; }
        public string Incoterm { get; set; }
        public string InvoiceNo { get; set; }
        public decimal InvoiceValueInUSD { get; set; }
        public int QTYInPack { get; set; }
        public int QTYInPcERApproved { get; set; }
        public int QtyInPc { get; set; }
        public int QtyInCtn { get; set; }
        public decimal GrossWeightInKg { get; set; }
        public string HAWBLNo { get; set; }
        public DateTime HAWBLDateD { get; set; }
        public string HAWBLDate { get; set; }
        public decimal ChargeableWeightInKgERApproved { get; set; }
        public decimal FreightAmountInUSDErApproved { get; set; }
        public decimal ChargeableWeightInKg { get; set; }
        public decimal FreightAmountInUSD { get; set; }
        public decimal FrieghtRatePerKgERApproved { get; set; }
        public decimal FreightRatePerKg { get; set; }
        public decimal FreightAmountInBDT { get; set; }
        public string FreightInvoiceNo { get; set; }
        public DateTime FreightInvoiceReceivedDateD { get; set; }
        public string FreightInvoiceReceivedDate { get; set; }
        public DateTime BillSubDateForPaymentD { get; set; }
        public string BillSubDateForPayment { get; set; }
        public DateTime PaymentDateD { get; set; }
        public string PaymentDate { get; set; }
        public DateTime CHQPOSubmitDateToForwarderD { get; set; }
        public string CHQPOSubmitDateToForwarder { get; set; }
        public DateTime AWABReleaseDateD { get; set; }
        public string AWABReleaseDate { get; set; }
        public string Remarks { get; set; }
        public List<AirFreightFile> AirFreightFiles { get; set; }
    }
}