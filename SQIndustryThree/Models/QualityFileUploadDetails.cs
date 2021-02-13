using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class QualityFileUploadDetails
    {
        public int QualityFileUploadId { get; set; }
        public string QualityFileName { get; set; }
        public string QualityFilePath { get; set; }
        public string ServerFileName { get; set; }
        public int QualityId { get; set; }
        public int userId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}