using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CommonModel
    {
        public int BusinessUnitId { get; set; }

        public string BusinessUnitName { get; set; }

        public int LocationId { get; set; }

        public string LocatioName { get; set; }

        public int ApproverId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DesignationName { get; set; }

        public int DepartmentId { get; set; }

        public string DeartmentName { get; set; }

        public string DepartmentName { get; set; }
    }
}