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
            SeedUsersAndRoles(context);
        }

        private void SeedUsersAndRoles(ISPContext context)
        {
            var RoleStore = new RoleStore<IdentityRole>(context);
            var RoleManager = new RoleManager<IdentityRole>(RoleStore);

            var AdminRole = new IdentityRole { Name = "Administrator" };
            RoleManager.Create(AdminRole);

            var SupportRole = new IdentityRole { Name = "Support" };
            RoleManager.Create(SupportRole);

            var UserRole = new IdentityRole { Name = "Subscriber" };
            RoleManager.Create(UserRole);

            var UserStore = new UserStore<User>(context);
            var UserManager = new UserManager<User>(UserStore);
            var User = new User
            {
                UserName = "root",
                Email = "root@localhost.localhost",
                PhoneNumber = "+380000000000",
                FirstName = "root",
                LastName = "root",
                Balance = 0,
                DoB = DateTime.UtcNow.Date,
                RegistrationDate = DateTime.UtcNow
            };
            UserManager.Create(User, "rootroot");
            UserManager.AddToRole(User.Id, "Administrator");

            context.SaveChanges();
        }
    }
}