using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class VehicleAllocationMaster
    {
        public int VehicleAllocationMasterId { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; }
        public int StartPointId { get; set; }
        public string StartPoint { get; set; }
        public int TripTypeId { get; set; }
        public string TripType { get; set; }
        public int TripCost { get; set; }
        public int RouteId { get; set; }
        public string Route { get; set; }
        public int TransactionBy { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string TransactionDateString { get; set; }
        public List<VehicleAllocationDetails> VehicleAllocationDetailsList { get; set; }
        public UserInformation UserInformation { get; set; }
    }
}