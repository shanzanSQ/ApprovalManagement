using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class VehicleRequestMaster
    {
        public int VehicleRequestMasterId { get; set; }
        public string DateOfRequest { get; set; }
        public int RequestorId { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnit { get; set; }
        public int DepartmentHeadId { get; set; }
        public string TravelStratDate { get; set; }
        public string TravelEndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public string DepartmentName { get; set; }
        public int RouteType { get; set; }
        public string Route { get; set; }
        public int PurposeofTravelId { get; set; }
        public string PurposeofTravel { get; set; }
        public string TripType { get; set; }
        public string PreferredVehicle { get; set; }
        public int NoofUser { get; set; }
        public string Remarks { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public int IsApproved { get; set; }
        public int ApproverStatus { get; set; }
        public int DeligationUserId { get; set; }
        public decimal AllocatedCost { get; set; }
        public string CommentFromCOO { get; set; }
        public string CommentFromStationMaster { get; set; }
        public string BusinessUnitName { get; set; }
        public UserInformation UserInformation { get; set; }
    }
}