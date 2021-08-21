using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public int Id { get; set; }
        public string Weight { get; set; }
    }
}