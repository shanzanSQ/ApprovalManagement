using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models.BillApproval
{
    public class BillComments
    {
        public int QueryId { get; set; }
        public int InvoiceKey { get; set; }
        public int ReviewerBy { get; set; }
        public string ReviewerByName { get; set; }
        public int ReviewerTo { get; set; }
        public string ReviewerToName { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime CreateBy { get; set; }
        public DateTime UpdatedBY { get; set; }
    }
}