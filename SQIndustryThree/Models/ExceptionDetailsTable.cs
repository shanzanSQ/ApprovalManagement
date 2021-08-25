using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class ExceptionDetailsTable
    {
        public int ExceptionDetailsId { get; set; }
        public int ExceptionMasterId { get; set; }
        public int Recoverable { get; set; }
        public float GrossWeight { get; set; }
        public float VolumetricWeight { get; set; }
        public float AirFreightRate { get; set; }
        public float AirFreightCost { get; set; }
        public string RecoverableFrom { get; set; }
        public float RecoverableAmmount { get; set; }
        public string PoInvoiceNo { get; set; }
        public float ClaimedAmmount { get; set; }
        public float DiscountAmount { get; set; }
        public float AmmountCancelation { get; set; }
        public float GarmentsLiability { get; set; }
        public string ExceptionDetails { get; set; }
        public string LossOrLiabilityCompany { get; set; }
        public string ValueOfLoss { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}