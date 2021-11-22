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
    }
}
