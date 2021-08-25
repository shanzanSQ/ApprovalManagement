using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class POFileUploadDetails
    {
        public int PoFileUploadId { get; set; }
        public string POFileName { get; set; }
        public string POFilePath { get; set; }
        public string ServerFileName { get; set; }
        public long BillPOKey { get; set; }
        public int userId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}