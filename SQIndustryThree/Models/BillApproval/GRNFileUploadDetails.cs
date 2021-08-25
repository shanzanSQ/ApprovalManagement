using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class GRNFileUploadDetails
    {
        public int GRNFileUploadId { get; set; }
        public string GRNFileName { get; set; }
        public string GRNFilePath { get; set; }
        public string ServerFileName { get; set; }
        public long BillPOKey { get; set; }
        public int userId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}