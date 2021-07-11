using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Bloggy.Data.Models;

namespace Bloggy.Data
{
    public class BloggyDBContext : DbContext
    {
        #region Ctors
        public BloggyDBContext(DbContextOptions<BloggyDBContext> context) : base(context)
        { }
        #endregion Ctors

        #region Properties
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        #endregion Properties
    }

    public class BloggyDBContextFactory : IDesignTimeDbContextFactory<BloggyDBContext>
    {
        public BloggyDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggyDBContext>();
            optionsBuilder.UseSqlite("Data Source = BloggyData.db");
            return new BloggyDBContext(optionsBuilder.Options);
        }
    }
}
