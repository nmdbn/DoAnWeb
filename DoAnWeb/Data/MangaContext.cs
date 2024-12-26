using DoAnWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnWeb.Data
{
    public class MangaContext : DbContext
    {
        public MangaContext(DbContextOptions<MangaContext> options) : base(options)
        {
        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Chapter>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Manga)
                .WithMany(m => m.Chapters)
                .HasForeignKey(c => c.MangaId);
        }
    }
}
