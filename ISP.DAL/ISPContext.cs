using ISP.DAL.DBModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL
{
    public class ISPContext : IdentityDbContext<User>
    {
        public DbSet<UserBalanceLog> UserBalanceLog { get; set; }

        public DbSet<TVChannel> TVChannels { get; set; }
        public DbSet<TVChannelContract> TVChannelContracts { get; set; }

        public DbSet<TVChannelPackage> TVChannelPackages { get; set; }
        public DbSet<TVChannelPackageContract> TVChannelPackageContracts { get; set; }

        public DbSet<InternetPackage> InternetPackages { get; set; }
        public DbSet<InternetPackageContract> InternetPackageContracts { get; set; }

        public ISPContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            //modelBuilder.Entity<TVChannelContract>()
            //    .HasRequired(tvc => tvc.Address)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<TVChannelContract>()
            //    .HasRequired(tvc => tvc.TVChannel)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TVChannelPackageContract>()
            //    .HasRequired(tvc => tvc.Address)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<TVChannelPackageContract>()
            //    .HasRequired(tvc => tvc.TVChannelPackage)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InternetPackageContract>()
            //    .HasRequired(tvc => tvc.Address)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<InternetPackageContract>()
            //    .HasRequired(tvc => tvc.InternetPackage)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }

        public static ISPContext Create()
        {
            return new ISPContext();
        }
    }
}
