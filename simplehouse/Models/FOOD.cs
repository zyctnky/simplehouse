using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class FOOD
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string TITLE { get; set; }
        [MaxLength(250)]
        public string DESCRIPTION { get; set; }
        public decimal PRICE { get; set; }
        [MaxLength(50)]
        public string IMAGE { get; set; }
        public int CATEGORY_ID { get; set; }
        public int STATE_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string STATE_NAME { get; set; }
    }
}