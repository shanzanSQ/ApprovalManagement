using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class ExceptionGenaralInformation
    {
        public int ExceptionGenralId { get; set; }
        public int? ExceptionMasterId { get; set; }
        public string StyleNo { get; set; }
        public string Color { get; set; }
        public string PO { get; set; }
        public string OriginalDD { get; set; }
        public string RevisedDD { get; set; }
        public float FOB { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public float Claim { get; set; }
        public float MaterialLiability { get; set; }
        public float GarmentsLiability { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}