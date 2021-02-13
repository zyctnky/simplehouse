using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class CONTACTINFO
    {
        public int ID { get; set; }
        [MaxLength(150)]
        public string ADDRESS { get; set; }
        [MaxLength(50)]
        public string PHONE { get; set; }
        [MaxLength(50)]
        public string EMAIL { get; set; }
        [MaxLength(150)]
        public string FACEBOOK { get; set; }
        [MaxLength(150)]
        public string TWITTER { get; set; }
        [MaxLength(150)]
        public string INSTAGRAM { get; set; }
    }
}