using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class CATEGORY
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string NAME { get; set; }
        public int STATE_ID { get; set; }

        public string STATE_NAME { get; set; }
    }
}