﻿using ISP.BLL.Identity;
using ISP.BLL.ModelActions;
using ISP.DAL.DBModels;
using ISP.DAL.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    [Authorize(Roles = "Administrator, Support")]
    public class AdminUserController : Controller
    {
        private UserActions actions = new UserActions();

        public ActionResult Index()
        {
            Dictionary<string, string> sortBy;
            Dictionary<string, bool> orderByDescending;
            actions.GetAvailableSortList(out sortBy, out orderByDescending);

            ViewData["sortBy"] = sortBy.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value });
            ViewData["orderByDescending"] = orderByDescending.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value.ToString() });

            return View();
        }

        public ActionResult IndexTableAjax(string sortBy = "Id", bool orderByDescending = false)
        {
            IEnumerable<User> users = actions.GetAll();
            IEnumerable<User> usersSorted = actions.Sort(users, sortBy, orderByDescending);
            Dictionary<string, string> usersRole = new Dictionary<string, string>();
            foreach (User user in users)
            {
                string userRole = actions.GetRoleName(user.Id);
                usersRole.Add(user.Id, userRole);
            }

            IEnumerable<UserDetails> usersDetailsSorted = usersSorted.Select(user => new UserDetails(user, usersRole[user.Id])).ToArray();

            return PartialView(usersDetailsSorted);
        }

        public ActionResult Details(string id)
        {
            return View();
        }

        public ActionResult Create()
        {
            UserAdminCreate user = new UserAdminCreate()
            {
                Password = DateTime.UtcNow.GetHashCode().ToString()
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdminCreate userDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(userDetails);
            }

            User user;
            string password;
            ContractAddress address;
            userDetails.Split(out user, out password, out address);

            if (actions.Create(user, password, address).Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(userDetails);
            }
        }

        public ActionResult Edit(string id)
        {
            User user = actions.GetById(id);
            string userRole = actions.GetRoleName(user.Id);

            if (userRole == "Administrator" && !User.IsInRole("Administrator"))
                return RedirectToAction("Index");

            Dictionary<string, string> roles;
            actions.GetAvailableRoleList(out roles);
            ViewData["roles"] = roles.Select(item => new SelectListItem { Text = item.Key, Value = item.Value });
            return View(new UserAdminEdit(user, userRole));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAdminEdit userDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(userDetails);
            }
            string userRole;
            User user = actions.GetById(userDetails.Id);
            userDetails.Split(ref user, out userRole);
            actions.Edit(user);
            if (User.IsInRole("Administrator"))
            {
                actions.ChangeRole(user.Id, userRole);
            }
            return RedirectToAction("Index");
        }
    }
}