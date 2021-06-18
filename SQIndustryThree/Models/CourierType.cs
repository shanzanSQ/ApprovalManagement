using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CourierType
    {
        public int Revision { get; set; }
        public int CourierTypeId { get; set; }
        public string Type { get; set; }
        public string CourierTypeName { get; set; }
        public string PostCode { get; set; }
        public string CreateBy { get; set; }
        public string UserName { get; set; }
        public List<CourierTypeDetails> CourierTypeDetails { get; set; }
        

    }
}