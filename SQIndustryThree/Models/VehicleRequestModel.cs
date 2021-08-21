using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class VehicleRequestModel
    {
        public int VehicleRequesMastertId { get; set; }
        public int RivisionNo { get; set; }
        public int Status { get; set; }
        public string DateOfRequest { get; set; }
        public int RequestorId { get; set; }
        public string UserName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int BusinessUnitId { get; set; }
        public int BudgetHeadrId { get; set; }
        public string BusinessUnitName { get; set; }
        public int DepartmentHeadId { get; set; }
        public string TravelStratDate { get; set; }
        public string TravelEndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartingPoint { get; set; }
        public string StartingPointName { get; set; }
        public string RouteType { get; set; }
        public string RouteTypeName { get; set; }
        public string PurposeofTravel { get; set; }
        public string PurposeofTravelName { get; set; }
        public string TripType { get; set; }
        public string TripTypeName { get; set; }
        public string PreferredVehicle { get; set; }
        public string PreferredVehicleName { get; set; }
        public int NoofUser { get; set; }
        public int NoofDays { get; set; }
        public string Remarks { get; set; }
        public int VehicleRequestDetailstId { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public string DesignationName { get; set; }
        public string Department { get; set; }
        public string DepartmentName { get; set; }
        public string DeartmentName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int IsApproved { get; set; }
         public int Pending { get; set; }
        public string CreateDate { get; set; }
        public string RequestorName { get; set; }

        public string RequestorEmail { get; set; }

        public string RequerstorMobile { get; set; }

        public string RequestorDesignation { get; set; }

        public string RequestorDepartment { get; set; }
        public int Created_By { get; set; }
        public int DepartmentID { get; set; }
        public string BudgetYear { get; set; }
        public string MonthOfYear { get; set; }
        public string InialAmount { get; set; }
        public string Amount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Budget_MTD { get; set; }
        public string MTDcost { get; set; }
        public string ReaminsCost { get; set; }
        public string Central_Budget_MTD { get; set; }
        public string Central_MTDcost { get; set; }
        public string Central_ReaminsCost { get; set; }
        public string VehicleRate { get; set; }
        public int VehicleBudgetEntryId { get; set; }
        public string AllocatedCost { get; set; }
        public List<CapexFileUploadDetails> IOurequestfiles { get; set; }
        public List<CapexFileUploadDetails> IouSettlementFiles { get; set; }
        public List<CourierApproverModel> VehicleApproverList { get; set; }
        public List<VehicleRequestModel> UserDetailsList { get; set; }
        public List<VehicleRequestModel> VehicleDeligationList { get; set; }
        public List<CommentsTable> VehicleComments { get; set; }
        public List<LogSection> VehicleLogSection { get; set; }


    }
}