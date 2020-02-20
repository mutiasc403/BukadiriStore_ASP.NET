using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Models
{
    public class BukadiriLapak
    {
        [Key]
        [MaxLength(5)]
        public string kodeLapak { get; set; }
        [Required]
        public string namaLapak { get; set; }
        public int peringkatLapak { get; set; }
        public int isDelete { get; set; }
        public string tanggalBuat { get; set; }
        public string tanggalUbah { get; set; }
        public string tanggalHapus { get; set; }
    }
}