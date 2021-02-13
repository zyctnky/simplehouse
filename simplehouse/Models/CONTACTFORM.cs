using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class CONTACTFORM
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string NAME { get; set; }
        [MaxLength(50)]
        public string EMAIL { get; set; }
        [MaxLength(150)]
        public string MESSAGE { get; set; }

        public DateTime SEND_DATE { get; set; }

    }
}