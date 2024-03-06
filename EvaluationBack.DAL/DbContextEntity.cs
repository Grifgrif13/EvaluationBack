using EvaluationBack.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBack.DAL
{
    public class DbContextEntity : DbContext
    {
        public DbContextEntity(DbContextOptions<DbContextEntity> options) : base(options)
        {
        }

        // DB sets
        public DbSet<Evenement> Evenement { get; set; }

        /// <summary>
        /// Action sur OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.OnModelInstantiated(modelBuilder);
        }

        /// <summary>
        /// Instanciation de la table évènement
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void OnModelInstantiated(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);

                entity.Property(e => e.Date)
                .IsRequired()
                .HasMaxLength(150);
                
                entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(150);
            });
        }
    }
}
