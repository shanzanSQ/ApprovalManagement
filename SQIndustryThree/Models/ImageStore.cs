using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Models
{
    [Table("ImageStore")]
    public class ImageStore
    {
        [Key]
        public int ImageId { get; set; }
        public int RowId { get; set; }
        public string ImageBase64String { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}