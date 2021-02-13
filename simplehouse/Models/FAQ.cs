using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class FAQ
    {
        public int ID { get; set; }
        [MaxLength(150)]
        public string TITLE { get; set; }
        public string DETAILS { get; set; }
        public int STATE_ID { get; set; }
        public string STATE_NAME { get; set; }
    }
}