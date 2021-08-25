using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CapexInformationMaster
    {
        public int CapexInfoId { get; set; }
        public string CapexName { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName{ get; set; }
        public string CapexAssetType { get; set; }
        public string CapexDescription { get; set; }
        public string CapexSourceType { get; set; }
        public DateTime CapexCreateDate { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int IsApproved { get; set; }
        public int CapexCatagoryID { get; set; }
        public string CapexCatagoryName { get; set; }
        public string UserName { get; set; }
        public string CapexReview { get; set; }
        public string Currency { get; set; }
        public int CurrencyID { get; set; }
        public int Revision { get; set; }
        public double Cost{ get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public List<CapexInformationDetails> CapexInformationDetails { get; set; }
        public List<QueryModel> ApproverQueryModelList { get; set; }
        public List<CommentsTable> CommentsTables { get; set; }
        public List<CapexFileUploadDetails> CapexFileUpload { get; set; }
        public List<LogSection> LogSections { get; set; }
    }
}