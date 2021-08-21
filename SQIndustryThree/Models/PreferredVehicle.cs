using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class PreferredVehicle
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public int PurposeofVisitId { get; set; }
        public string PurposeofTravelName { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public int StartingPointId { get; set; }
        public string StartingPointName { get; set; }
        public int BudgetHeaderId { get; set; }
        public string BudgetHeaderName { get; set; }

    }
}