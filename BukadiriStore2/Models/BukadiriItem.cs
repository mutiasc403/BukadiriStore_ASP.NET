using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Models
{
    public class BukadiriItem
    {
        [Key]
        [MaxLength(5)]
        public string kodeItem { get; set; }
        [Required]
        public string namaItem { get; set; }
        public int hargaItem { get; set; }
        public string tanggalBuat { get; set; }
        public string tanggalUbah { get; set; }
        public string tanggalHapus { get; set; }

        [MaxLength(5)]
        public string kodeProvinsi { get; set; }
        public BukadiriProvinsi BukadiriProvinsi { get; set; } //cara bikin foreign key, coba cek di table dbo > column > nanti keliatan
        
        [MaxLength(5)]
        public string kodePilihan { get; set; }
        public BukadiriPilihan BukadiriPilihan { get; set; }
        
        [MaxLength(5)]
        public string kodeLapak { get; set; }
        public BukadiriLapak BukadiriLapak { get; set; }
        public int isDelete { get; set; }
    }
}