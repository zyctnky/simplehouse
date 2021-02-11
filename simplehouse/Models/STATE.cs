using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class STATE
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string NAME { get; set; }
    }
}