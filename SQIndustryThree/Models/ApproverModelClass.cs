using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class ApproverModelClass
    {
        public int BusinessUnitId { get; set; }
        public int CatagoryId { get; set; }
        public int Status { get; set; }
        public List<UserInformation> UserInformationList { get; set; }

    }
}