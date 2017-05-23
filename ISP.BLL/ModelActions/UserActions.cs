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
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace ISP.BLL.ModelActions
{
    public class UserActions
    {
        private RepositoryBase<User> repository;
        private RepositoryBase<ContractAddress> addressRepository;
        private ApplicationUserManager userManager;

        public UserActions()
        {
            repository = new UserRepository();
            addressRepository = new ContractAddressRepository();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public IdentityResult Create(User item, string password, ContractAddress address)
        {
            User user = new User()
            {
                UserName = repository.Count().ToString(),
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                DoB = item.DoB.Date,
                RegistrationDate = DateTime.UtcNow,
                Balance = 0
            };
            IdentityResult result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Subscriber");
                address.SubscriberId = user.Id;
                addressRepository.Create(address);
            }
            return result;
        }
        public void Edit(User item)
        {
            repository.Edit(item);
        }

        public User Get(string emailOrPhoneOrNumber)
        {
            return repository.Get(item => item.Email == emailOrPhoneOrNumber || item.PhoneNumber == emailOrPhoneOrNumber || item.UserName == emailOrPhoneOrNumber);
        }
        public User GetById(string userId)
        {
            return (repository as UserRepository).GetById(userId);
        }
        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
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
        
        public void GetAvailableRoleList(out Dictionary<string, string> roles)
        {
            roles = new Dictionary<string, string>()
            {
                { "Администратор", "Administrator"},
                { "Поддержка", "Support"},
                { "Абонент", "Subscriber"}
            };
        }

        public IdentityResult ChangePassowrd(string userId, string currentPassword, string newPassword)
        {
            return userManager.ChangePassword(userId, currentPassword, newPassword);
        }
        public IdentityResult ConfirmEmail(string userId, string code)
        {
            return userManager.ConfirmEmail(userId, code);
        }
        public IdentityResult ResetPassword(string userId, string code, string password)
        {
            User user = userManager.FindById(userId);
            if (user == null)
            {
                return null;
            }
            return userManager.ResetPassword(userId, code, password);
        }
        public string GetRoleName(string userId)
        {
            return userManager.GetRoles(userId).First();
        }
        public void ChangeRole(string userId, string roleName)
        {
            User user = userManager.FindById(userId);
            IEnumerable<string> userRoles = userManager.GetRoles(user.Id);
            userManager.RemoveFromRoles(user.Id, userRoles.ToArray());
            userManager.AddToRole(user.Id, roleName);
        }
    }
}