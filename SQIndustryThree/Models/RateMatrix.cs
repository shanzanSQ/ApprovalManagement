using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class RateMatrix
    {
        public int RateMatrixId { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; }
        public int RouteId { get; set; }
        public string Route { get; set; }
        public int TripTypeId { get; set; }
        public string TripType { get; set; }
        public decimal Rate { get; set; }
    }
}