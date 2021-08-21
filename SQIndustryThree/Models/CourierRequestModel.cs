using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CourierRequestModel
    {
        //public string weight { get; set; }
        public int CourierBudgetEntryId { get; set; }
        public int CourierDispatchNo { get; set; }
        public string ReferenceNo { get; set; }
        public string CourierNumber { get; set; }
        public string ConsolidateCost { get; set; }
        public string ConsolidateWeight { get; set; }
        public string LASTSERIALNO { get; set; }
        public string total { get; set; }
        public string Netweight { get; set; }
        public string NetweightSum { get; set; }
        public string ConsolidateValue { get; set; }
        public string DateOfRequest { get; set; }
        public int RivisionNo { get; set; }
        public int Pending { get; set; }
        public int CourierRequestId { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public int RequesterId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DeartmentName { get; set; }
        public string CourierType { get; set; }
        public string Customer { get; set; }
        public string BuyerName { get; set; }
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string ContactNo { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string DispatchDate { get; set; }
        public string FontDispatchDate { get; set; }
        public string FontReceivedDate { get; set; }
        public string FontReceivedWeight { get; set; }
        public string ReceivedDate { get; set; }
        public string ReceivedWeight { get; set; }
        public string HandOverTo { get; set; }
        public string Deliverydate { get; set; }
        public string ProductDescription { get; set; }
        public string Weight { get; set; }
        public string ActualWeight { get; set; }
        public string Volume { get; set; }
        public string AirwayBillno { get; set; }
        public string FontAirwayBillno { get; set; }
        public string Courier { get; set; }
        public string ProposedDate { get; set; }
        public string Cost { get; set; }
        public string GenerateCourier { get; set; }
        public string GenerateCourierName { get; set; }
        public string GenerateProposedDate { get; set; }
        public string GenerateCost { get; set; }
        public string Remarks { get; set; }
        public string FontRemarks { get; set; }
        public string CreateDate { get; set; }
        public string CourierTypeId { get; set; }
        public string CourierProposedDate { get; set; }
        public string ServiceProvider { get; set; }
        public string Rate { get; set; }
        public int IsApproved { get; set; }
        public string BudgetYear { get; set; }
        public string MonthOfYear { get; set; }
        public string Amount { get; set; }
        public string Current_Financial_Year { get; set; }
        // public List<CourierApproverModel> CourierApproverList { get; set; }
        public List<CourierApproverModel> CourierApproverList { get; set; }
        public List<CourierFontDeskModel> CourierFontDeskModelList { get; set; }
        public List<CourierAmmountListModel> CourierAmmountList { get; set; }
        public List<LogSection> CourierLogSection { get; set; }
        public List<CommentsTable> CourierComments { get; set; }


    }
}