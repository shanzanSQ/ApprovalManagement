// Decompiled with JetBrains decompiler
// Type: SQIndustryThree.Models.RequestorModel
// Assembly: SQIndustryThree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E941217C-ED18-461E-BAA5-9C847750ED36
// Assembly location: C:\Users\ashiqurrahman\Desktop\AMSTest2Last\AMSTest2\bin\SQIndustryThree.dll

using System;
using System.Collections.Generic;
using System.Web;

namespace SQIndustryThree.Models
{
    public class RequestorModel
    {
        public int RequestorId { get; set; }

        public int BusinessUnitId { get; set; }

        public string BusinessUnitName { get; set; }

        public string SQID { get; set; }
        public int SQUnitId { get; set; }
        public int SQUnitName { get; set; }
        public int SQDepartmentId { get; set; }
        public int SQDepartmentName { get; set; }



        public int VisitMode { get; set; }

        public int LocationId { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int RivisionNo { get; set; }

        public int IsApproved { get; set; }

        public int DepartmentHeadId { get; set; }

        public int ApproverId { get; set; }

        public string LocationName { get; set; }

        public string RequestorName { get; set; }

        public string RequestorEmail { get; set; }

        public string RequerstorMobile { get; set; }

        public string RequestorDesignation { get; set; }

        public string RequestorDepartment { get; set; }

        public string NIDorPassport { get; set; }

        public string Image { get; set; }

        public string ImagePath { get; set; }

        public int Status { get; set; }

        public int Created_By { get; set; }

        public HttpPostedFileBase UploadImage { get; set; }

        public int ModeOfVisit { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<VisitorModel> VisitorList { get; set; }

        public List<IOUApproverModel> VisitorApproverList { get; set; }

        public List<CommentsTable> VisitorComments { get; set; }

        public List<LogSection> VisitorLogSection { get; set; }
    }
}
