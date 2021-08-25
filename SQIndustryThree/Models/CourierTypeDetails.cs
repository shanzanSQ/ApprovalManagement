using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class CourierTypeDetails
    {
        public int Revision { get; set; }
        public int CourierTypeId { get; set; }
        public string Type { get; set; }
        public string CourierTypeName { get; set; }
        public string PostCode { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string UserName { get; set; }
        //public string CourierTypeId { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string ServiceProviderId { get; set; }
        public string ServiceProvider { get; set; }
        public string ServiceProviderName { get; set; }
        public string WeightRange { get; set; }
        public string LeadTimeFrom { get; set; }
        public string LeadTimeTo { get; set; }
        //public string CountryShortName { get; set; }
        public string Rate { get; set; }
        public string Currency { get; set; }
       

        // public string Type { get; set; }
        // public DateTime CreateDate { get; set; }
        //public string CraeteBy { get; set; }
        //  public DateTime UpdateDate { get; set; }
        // public string UpdateBy { get; set; }

         public List<CommonModel> commonModelList { get; set; }
    }
}