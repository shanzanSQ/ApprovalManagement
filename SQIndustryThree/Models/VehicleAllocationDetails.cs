using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class VehicleAllocationDetails
    {
        public int VehicleAllocationDetailsId { get; set; }
        public int VehicleAllocationMasterId { get; set; }
        public int VehicleRequestMasterId { get; set; }
        public int AllocatedCost { get; set; }
    }
}