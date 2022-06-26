// using Entities.Models;

using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;


namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User> 
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().Property(t => t.Id).HasColumnName("CategoryId");
            modelBuilder.Entity<Post>().Property(t => t.Id).HasColumnName("PostId");
         
             modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}