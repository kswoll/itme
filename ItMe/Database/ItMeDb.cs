using Microsoft.EntityFrameworkCore;

namespace ItMe.Database
{
    public class ItMeDb : DbContext
    {
        public DbSet<DbPerson> Persons { get; set; }
        public DbSet<DbPersonLogin> PersonLogins { get; set; }
        public DbSet<DbFeature> Features { get; set; }
        public DbSet<DbBlogPost> BlogPosts { get; set; }

        public ItMeDb(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbPerson>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<DbPerson>().HasIndex(x => x.Host).IsUnique();
            modelBuilder.Entity<DbBlogPost>().HasIndex(x => new { x.PersonId, x.Slug }).IsUnique();
        }
    }
}