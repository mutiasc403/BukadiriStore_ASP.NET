using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Models
{
    public class Login
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public int counting { get; set; }
        public string status { get; set; }
    }
}