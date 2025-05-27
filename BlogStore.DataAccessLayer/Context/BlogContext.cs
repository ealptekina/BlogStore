using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.DataAccessLayer.Context
{
    public class BlogContext : DbContext
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
    }
}
