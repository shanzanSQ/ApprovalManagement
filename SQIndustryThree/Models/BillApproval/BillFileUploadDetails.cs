using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class BillFileUploadDetails
    {
        public int BillFileUploadId { get; set; }
        public string BillFileName { get; set; }
        public string BillFilePath { get; set; }
        public string ServerFileName { get; set; }
        public long BillPOKey { get; set; }
        public int userId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}