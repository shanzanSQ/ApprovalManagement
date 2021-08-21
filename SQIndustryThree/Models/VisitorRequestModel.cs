// Decompiled with JetBrains decompiler
// Type: SQIndustryThree.Models.VisitorRequestModel
// Assembly: SQIndustryThree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E941217C-ED18-461E-BAA5-9C847750ED36
// Assembly location: C:\Users\ashiqurrahman\Desktop\AMSTest2Last\AMSTest2\bin\SQIndustryThree.dll

using System;
using System.Collections.Generic;

namespace SQIndustryThree.Models
{
    public class VisitorRequestModel
    {
        public int RequestorId { get; set; }

        public int VisitorId { get; set; }

        public int BusinessUnitId { get; set; }

        public int ModeOfVisit { get; set; }

        public string BusinessUnitName { get; set; }
        public string SQID { get; set; }
        public int SQUnitId { get; set; }
        public string SQUnitName { get; set; }
        public string SQDepartmentId { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }
        public string MeetingWith { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategroyName { get; set; }

        public string RequestorName { get; set; }

        public string RequestorEmail { get; set; }

        public string RequerstorMobile { get; set; }

        public string RequestorDesignation { get; set; }

        public string RequestorDepartment { get; set; }

        public string VisitorName { get; set; }

        public string VisitorEmail { get; set; }

        public string VisitorMobile { get; set; }

        public string VisitorDesignation { get; set; }

        public string VisitorCompany { get; set; }

        public string NIDorPassport { get; set; }

        public string VisitorNationality { get; set; }

        public string Image { get; set; }

        public string ImagePath { get; set; }

        public string PurposeOfVisitSQ { get; set; }

        public string Chainavisit { get; set; }

        public string CraeteDate { get; set; }

        public DateTime VisitDate { get; set; }

        public string UpdateDate { get; set; }

        public int IsApproved { get; set; }

        public int TotalVisitor { get; set; }

        public string ApprovedStatus { get; set; }

        public string Remarks { get; set; }

        public int Pending { get; set; }

        public string VisitorCardNo { get; set; }
        public string VehicleNo { get; set; }

        public string GateRemarks { get; set; }

        public string CheckIn { get; set; }

        public DateTime InTime { get; set; }

        public string CheckOut { get; set; }

        public DateTime OutTime { get; set; }
        public string Checked { get; set; }
        public List<ArrivedVisitor> arrivedVisitors { get; set; }
    }
}
