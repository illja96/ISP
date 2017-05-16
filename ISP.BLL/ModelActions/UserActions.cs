using ISP.BLL.Identity;
using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ISP.DAL.Repositories;
using ISP.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ISP.BLL.ModelActions
{
    public class UserActions
    {
        public ISPContext context;
        public ApplicationUserManager userManager;

        public UserActions(ApplicationUserManager userManager)
        {
            context = new ISPContext();
            this.userManager = userManager;
        }
        public UserActions(ISPContext context, ApplicationUserManager userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IdentityResult Create(User item, string password)
        {
            User user = new User()
            {
                Email = item.Email,
                UserName = context.Set<User>().Count().ToString(),
                PhoneNumber = item.PhoneNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                Balance = item.Balance
            };
            IdentityResult result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Subscriber");
            }
            return result;
        }
        public void Edit(User item)
        {
            context.Entry<User>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public User Get(string emailOrPhoneOrNumber)
        {
            return context.Set<User>().First(item => item.Email == emailOrPhoneOrNumber || item.PhoneNumber == emailOrPhoneOrNumber);
        }
        public IEnumerable<User> GetAll()
        {
            return context.Set<User>();
        }

        public void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            sortBy = new Dictionary<string, string>()
            {
                { "По умолчанию", "Id" },
                { "По имени", "FirstName" },
                { "По фамилии", "LastName" },
                { "По отчеству", "MiddleName" },
                { "По номеру", "UserName" },
                { "По email", "Email" },
                { "По телефону", "PhoneNumber" },
                { "По балансу", "Balance" }
            };

            orderByDescending = new Dictionary<string, bool>()
            {
                { "По возрастанию", false },
                { "По убыванию", true }
            };
        }
        public IEnumerable<User> Sort(IEnumerable<User> items, string sortBy, bool orderByDescending)
        {
            switch (sortBy)
            {
                case "FirstName":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.FirstName);
                    else
                        return items.OrderBy(item => item.FirstName);

                case "LastName":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.LastName);
                    else
                        return items.OrderBy(item => item.LastName);

                case "MiddleName":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.MiddleName);
                    else
                        return items.OrderBy(item => item.MiddleName);

                case "UserName":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.UserName);
                    else
                        return items.OrderBy(item => item.UserName);

                case "Email":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.Email);
                    else
                        return items.OrderBy(item => item.Email);

                case "PhoneNumber":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.PhoneNumber);
                    else
                        return items.OrderBy(item => item.PhoneNumber);

                case "Balance":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.Balance);
                    else
                        return items.OrderBy(item => item.Balance);

                default:
                    return items;
            }
        }

        public IdentityResult ConfirmEmail(string id, string code)
        {
            return userManager.ConfirmEmail(id, code);
        }
        public IdentityResult ResetPassword(string id, string code, string password)
        {
            User user = userManager.FindById(id);
            if (user == null)
            {
                return null;
            }
            return userManager.ResetPassword(id, code, password);
        }
        public void ChangeRole(string id, string roleName)
        {
            User user = userManager.FindById(id);
            foreach(IdentityUserRole role in user.Roles)
            {
                string userRoleName = context.Roles.Find(role.RoleId).Name;
                userManager.RemoveFromRole(user.Id, userRoleName);
            }            
            userManager.AddToRole(user.Id, roleName);
        }
    }
}