using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class ArrivedVisitor
    {
        public int ArrivedVisitorId { get; set; }
        public int RequestorId { get; set; }
        public int VisitorId { get; set; }
        public int RowId { get; set; }
        public string VisitorCardNo { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string VehicleNo { get; set; }
        public string CheckIn { get; set; }
        public DateTime InTime { get; set; }
        public string CheckOut { get; set; }
        public DateTime OutTime { get; set; }
        public string Remarks { get; set; }

    }
}