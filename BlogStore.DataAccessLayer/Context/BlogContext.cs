using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.DataAccessLayer.Context
{
    public class BlogContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server bağlantı
            optionsBuilder.UseSqlServer("Server=DESKTOP-IMEIVGC\\SQLEXPRESS;Initial Catalog=BlogStore;Integrated Security=true;Trust Server Certificate=true");
        }

        // DbSet'ler (tablolar) tanımlanır
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many için composite primary key tanımı
            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleId, at.TagId });

            // İlişkilerin konfigürasyonu
            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId);
        }
    }
}
