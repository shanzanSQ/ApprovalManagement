using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class ExceptionRequestMaster
    {
        public int ExceptionMasterId { get; set; }
        public int? ExceptionTypeId { get; set; }
        public string ExceptionTypeName { get; set; }
        public string ExceptionReasonName { get; set; }
        public int ExceptioReasonsId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public string CreateDate { get; set; }
        public int IsApproved { get; set; }
        public string UpdateDate { get; set; }
        public int? RivisionNo { get; set; }
        public int? NoApprover { get; set; }
        public string Reasons { get; set; }
        public int? BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Odd { get; set; }
        public string Rdd { get; set; }
        public string ResponsiblePerson { get; set; }
        public string RequestBy { get; set; }
        public int RequestorID { get; set; }
        public int PendingComments { get; set; }
        public string NecessaryAction { get; set; }
        public string HrActionRemarks { get; set; }
        public int IsHrInteraction { get; set; }
        public int SupplyChainApprover { get; set; }

        public int ShowType { get; set; }

        public List<ExceptionGenaralInformation> ExpgenaralInfoList { get; set; }
        public ExceptionDetailsTable ExceptionDetails { get; set; }
        public List<CapexFileUploadDetails> ExceptionFilesList { get; set; }
        public List<QueryModel> ApproverList { get; set; }
        public List<CommentsTable> ExceptionComments { get; set; }
    }
}