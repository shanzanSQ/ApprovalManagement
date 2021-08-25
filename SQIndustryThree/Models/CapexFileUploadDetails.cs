using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CapexFileUploadDetails
    {
        public int CapexFileUploadId { get; set; }
        public string CapexFileName { get; set; }
        public string CapexFilePath { get; set; }
        public string ServerFileName { get; set; }
        public string ShortPath { get; set; }
        public int CapexInfoId { get; set; }
        public int userId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}