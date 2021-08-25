using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class IOURequestModel
    {
        public int IouRequestId { get; set; }
        public int IouAmmountId { get; set; }
        public int IsSettledApprove { get; set; }
        public int Status { get; set; }
        public string DateOfRequest { get; set; }
        public string RequiredDate { get; set; }
        public string SettlementDate { get; set; }
        public int RivisionNo { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public string UserName { get; set; }
        public int DepartmentID { get; set; }
        public int UserId { get; set; }
        public string DepartmentName { get; set; }
        public int Ammount { get; set; }
        public int TotalDisburseAmmount { get; set; }
        public int TotalExpenceAmmount { get; set; }
        public string SettlementCreateDate { get; set; }
        public int IsApproved { get; set; }
        public int IsSettled { get; set; }
        public int Pending { get; set; }
        public string Purpose { get; set; }
        public string RemarksSettlement { get; set; }
        public string ItemName { get; set; }
        public string CreateDate { get; set; }
        public List<CapexFileUploadDetails> IOurequestfiles { get; set; }
        public List<CapexFileUploadDetails> IouSettlementFiles { get; set; }
        public List<IOUApproverModel> IOUApproverList { get; set; }
        public List<IOUApproverModel> SettlementApproverList { get; set; }
        public List<IOURequestModel> AmmountList { get; set; }
        public List<IOURequestModel> DisburseList { get; set; }
        public List<CommentsTable> IouComments { get; set; }
        public List<LogSection> IouLogSection { get; set; }
    }
}