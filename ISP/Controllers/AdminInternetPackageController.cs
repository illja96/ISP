﻿using ISP.DAL.DBModels;
using ISP.DAL.Repositories;
using ISP.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminInternetPackageController : Controller
    {
        private InternetPackageRepo internetPackageRepo = new InternetPackageRepo();

        public ActionResult Index()
        {
            Dictionary<string, string> sortBy;
            Dictionary<string, bool> orderByDescending;
            internetPackageRepo.GetAvailableSortList(out sortBy, out orderByDescending);

            ViewBag.sortBy = sortBy.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value });
            ViewBag.orderByDescending = orderByDescending.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value.ToString() });

            return View();
        }

        public ActionResult IndexTableAjax(string sortBy = "Id", bool orderByDescending = false)
        {
            IEnumerable<InternetPackage> internetPackages = internetPackageRepo.GetAll();
            IEnumerable<InternetPackage> internetPackagesSorted = internetPackageRepo.Sort(internetPackages, sortBy, orderByDescending);
            IEnumerable<InternetPackageDetails> internetPackagesDetailsSorted = internetPackagesSorted.Select(internetPackage => new InternetPackageDetails(internetPackage)).ToArray();

            return PartialView(internetPackagesDetailsSorted);
        }

        public ActionResult DetailsAjax(Guid id)
        {
            InternetPackageDetails internetPackage = new InternetPackageDetails(internetPackageRepo.Get(id));
            return PartialView(internetPackage);
        }

        public ActionResult Create()
        {
            InternetPackage internetPackage = new InternetPackage();
            return View(internetPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InternetPackage internetPackage)
        {
            if (!ModelState.IsValid)
            {
                return View(internetPackage);
            }

            internetPackageRepo.Create(internetPackage);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            InternetPackage internetPackage = internetPackageRepo.Get(id);
            return View(internetPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InternetPackage internetPackage)
        {
            if (!ModelState.IsValid)
            {
                return View(internetPackage);
            }

            internetPackageRepo.Edit(internetPackage);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid id)
        {
            internetPackageRepo.Cancel(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renew(Guid id)
        {
            internetPackageRepo.Renew(id);
            return RedirectToAction("Index");
        }
    }
}