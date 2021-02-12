using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simplehouse.Models
{
    public class MEMBER
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string NAME { get; set; }
        [MaxLength(50)]
        public string TITLE { get; set; }
        [MaxLength(150)]
        public string DESCRIPTION { get; set; }
        [MaxLength(150)]
        public string FACEBOOK { get; set; }
        [MaxLength(150)]
        public string TWITTER { get; set; }
        [MaxLength(150)]
        public string INSTAGRAM { get; set; }
        [MaxLength(150)]
        public string IMAGE { get; set; }
        public int STATE_ID { get; set; }
        public string STATE_NAME { get; set; }
    }
}