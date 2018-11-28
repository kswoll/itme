using ItMe.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

            var partialFieldConverter = new ValueConverter<PartialDate, string>(
                x => x.Encode(),
                x => PartialDate.Decode(x));
            modelBuilder.Entity<DbJob>().Property(x => x.Start).HasConversion(partialFieldConverter);
            modelBuilder.Entity<DbJob>().Property(x => x.End).HasConversion(partialFieldConverter);
        }
    }
}