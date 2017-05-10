using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL
{
    public class ISPContext : DbContext
    {
        public DbSet<User> Users;
        public DbSet<UserBalanceLog> UserBalanceLog;

        public DbSet<TVChannel> TVChannels;
        public DbSet<TVChannelContract> TVChannelContracts;

        public DbSet<TVChannelPackage> TVChannelPackages;
        public DbSet<TVChannelPackageContract> TVChannelPackageContracts;

        public DbSet<InternetPackage> InternetPackages;
        public DbSet<InternetPackageContract> InternetPackageContracts;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
