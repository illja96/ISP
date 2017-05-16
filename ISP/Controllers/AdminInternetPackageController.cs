using ISP.BLL.ModelActions;
using ISP.DAL.DBModels;
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
        private InternetPackageActions actions = new InternetPackageActions();

        public ActionResult Index()
        {
            Dictionary<string, string> sortBy;
            Dictionary<string, bool> orderByDescending;
            actions.GetAvailableSortList(out sortBy, out orderByDescending);

            ViewBag.sortBy = sortBy.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value });
            ViewBag.orderByDescending = orderByDescending.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value.ToString() });

            return View();
        }

        public ActionResult IndexTableAjax(string sortBy = "Id", bool orderByDescending = false)
        {
            IEnumerable<InternetPackage> internetPackages = actions.GetAll();
            IEnumerable<InternetPackage> internetPackagesSorted = actions.Sort(internetPackages, sortBy, orderByDescending);
            IEnumerable<InternetPackageDetails> internetPackagesDetailsSorted = internetPackagesSorted.Select(internetPackage => new InternetPackageDetails(internetPackage)).ToArray();

            return PartialView(internetPackagesDetailsSorted);
        }

        public ActionResult DetailsAjax(Guid id)
        {
            InternetPackageDetails internetPackage = new InternetPackageDetails(actions.Get(id));
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

            actions.Create(internetPackage);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            InternetPackage internetPackage = actions.Get(id);
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

            actions.Edit(internetPackage);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid id)
        {
            actions.Cancel(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renew(Guid id)
        {
            actions.Renew(id);
            return RedirectToAction("Index");
        }
    }
}