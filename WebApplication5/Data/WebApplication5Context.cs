using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class WebApplication5Context : DbContext
    {
        public WebApplication5Context()
        {
        }

        public WebApplication5Context(DbContextOptions<WebApplication5Context> options)
            : base(options)
        {
        }

        public DbSet<Crypto> Crypto { get; set; }

        protected internal void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.HasKey(e => e.Cid);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Reviewdate).IsRequired();
            });
        }
    }
}
