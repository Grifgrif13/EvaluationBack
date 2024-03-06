using EvaluationBack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationBack.DAL
{
    public class DbContextEntity : DbContext
    {
        public DbContextEntity(DbContextOptions<DbContextEntity> options) : base(options)
        {
        }

        // DB sets
        public DbSet<Model1> Model1 { get; set; }

        private void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.OnModelInstantiated(modelBuilder);
        }

        private void OnModelInstantiated(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model1>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
