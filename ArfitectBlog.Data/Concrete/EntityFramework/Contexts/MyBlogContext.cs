using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Data.Concrete.EntityFramework.Mappings;
using ArfitectBlog.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArfitectBlog.Data.Concrete.EntityFramework.Contexts
{
    public class MyBlogContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyBlog;Trusted_Connection=True;MultipleActiveResultSets=True;");
        //}
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
        }
    }
}
