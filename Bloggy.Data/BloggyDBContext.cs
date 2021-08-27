using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Bloggy.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;

namespace Bloggy.Data
{
    public class BloggyDBContext : ApiAuthorizationDbContext<AppUser>
    {
        #region Ctors
        public BloggyDBContext(DbContextOptions options) : base(options, new OperationalStoreOptionsMigrations()) { }
        #endregion Ctors

        #region Properties
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        #endregion Properties

        #region Methods
        protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
        #endregion Methods
    }

    public class BloggyDBContextFactory : IDesignTimeDbContextFactory<BloggyDBContext>
    {
        public BloggyDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggyDBContext>();
            optionsBuilder.UseSqlite("Data Source = ../BloggyData.db");
            return new BloggyDBContext(optionsBuilder.Options);
        }
    }

    public class OperationalStoreOptionsMigrations : IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions()
        {
            DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
            EnableTokenCleanup = false,
            PersistedGrants = new TableConfiguration("PersistedGrants"),
            TokenCleanupBatchSize = 100,
            TokenCleanupInterval = 3600,
        };
    }
}
