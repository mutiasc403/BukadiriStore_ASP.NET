using BukadiriStore2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BukadiriStore2.Context
{
    public class BukadiriContext : DbContext
    {
        public DbSet<BukadiriProvinsi> BukadiriProvinsi { get; set; } //table
        public DbSet<BukadiriPilihan> BukadiriPilihan { get; set; } //table
        public DbSet<BukadiriLapak> BukadiriLapak { get; set; } //table
        public DbSet<BukadiriItem> BukadiriItem { get; set; } //table
        public DbSet<Login> Login { get; set; } //table
    }
}