using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class QueryModel
    {
        public int ApprovalId { get; set; }
        public string ApproverName { get; set; }
        public int ApproverUserId { get; set; }
        public int IsApproved { get; set; }
        public string ReplyMessage { get; set; }
        public string ReviewComment { get; set; }
        public string UpdateDate { get; set; }
        public string DesignationName { get; set; }
        public int ApproverNO { get; set; }
    }
}