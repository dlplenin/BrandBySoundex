using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BrandBySoundex.Models;

namespace BrandBySoundex.Data
{
    public class BrandBySoundexContext : DbContext
    {
        public BrandBySoundexContext (DbContextOptions<BrandBySoundexContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity => { entity.HasIndex(brand => (new { brand.Marca })).IsUnique(); });
        }

        //[DbFunction("SqlServer", "SOUNDEX")]
        [DbFunction(Name = "SoundEx", IsBuiltIn = true)]
        public static string Soundex(string s) => throw new Exception();

        [DbFunction(Name = "Difference", IsBuiltIn = true)]
        public static int Difference(string expre1, string expre2) => throw new Exception();
    }
}
