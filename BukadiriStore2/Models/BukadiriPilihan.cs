using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Models
{
    public class BukadiriPilihan
    {
        [Key]
        [MaxLength(5)]
        public string kodePilihan { get; set; }
        [Required]
        public string namaPilihan { get; set; }
        public int diskonPilihan { get; set; }
        public int isDelete { get; set; }
        public string tanggalBuat { get; set; }
        public string tanggalUbah { get; set; }
        public string tanggalHapus { get; set; }
    }
}