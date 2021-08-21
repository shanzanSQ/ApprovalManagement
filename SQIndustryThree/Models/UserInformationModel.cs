using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class UserInformationModel
    {
       
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string IsActive { get; set; }
        public int Revision { get; set; }
        public int DepartmentId { get; set; }
        public string DeartmentName { get; set; }
        public int CapexCatagoryID { get; set; }
        public string CapexCatagoryName { get; set; }
        public int AssetCatagoryId { get; set; }
        public string AssetCatagoryName { get; set; }

    }
}