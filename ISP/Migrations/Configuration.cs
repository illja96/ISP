namespace ISP.Migrations
{
    using ISP.DAL;
    using ISP.DAL.DBModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ISPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ISPContext context)
        {
            ISPContextSeed.SeedUsersAndRoles(context);
            ISPContextSeed.SeedTVChannelPackages(context);
            ISPContextSeed.SeedInternetPackages(context);
        }
    }
}