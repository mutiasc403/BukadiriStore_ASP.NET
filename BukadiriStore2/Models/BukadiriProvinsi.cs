using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Models
{
    public class BukadiriProvinsi
    {
        [Key]
        [MaxLength(5)]
        public string kodeProvinsi { get; set; }
        [Required]
        public string namaProvinsi { get; set; }
        public int jumlahKotaProvinsi { get; set; }
        public int isDelete { get; set; }
        public string tanggalBuat { get; set; }
        public string tanggalUbah { get; set; }
        public string tanggalHapus { get; set; }
    }
}