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
    public class AdminTVChannelPackageController : Controller
    {
        private TVChannelPackageActions actions = new TVChannelPackageActions();

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
            IEnumerable<TVChannelPackage> tvChannelPackages = actions.GetAll();
            IEnumerable<TVChannelPackage> tvChannelPackagesSorted = actions.Sort(tvChannelPackages, sortBy, orderByDescending);
            IEnumerable<TVChannelPackageDetails> tvChannelPackagesDetailsSorted = tvChannelPackagesSorted.Select(tvChannelPackage => new TVChannelPackageDetails(tvChannelPackage)).ToArray();

            return PartialView(tvChannelPackagesDetailsSorted);
        }

        public ActionResult Details(Guid id)
        {
            TVChannelPackage tvChannelPackage = actions.Get(id);
            ViewBag.tvChannelId = actions.GetAllChannelsExceptChannelsInPackage(id).Select(tvChannel => new SelectListItem() { Text = tvChannel.Name, Value = tvChannel.Id.ToString() });
            return View(new TVChannelPackageDetails(tvChannelPackage));
        }

        public ActionResult DetailsTVChannelsAjax(Guid id)
        {
            TVChannelPackageDetails tvChannelPackageDetails = new TVChannelPackageDetails(actions.Get(id));
            return PartialView(tvChannelPackageDetails.Channels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChannelToPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            actions.AddChannelToPackage(tvChannelPackageId, tvChannelId);
            return RedirectToAction("Details", new { @id = tvChannelPackageId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveChannelFromPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            actions.RemoveChannelFromPackage(tvChannelPackageId, tvChannelId);
            return RedirectToAction("Details", new { @id = tvChannelPackageId });
        }

        public ActionResult Create()
        {
            TVChannelPackage tvChannelPackage = new TVChannelPackage();
            return View(tvChannelPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TVChannelPackage tvChannelPackage)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannelPackage);
            }

            actions.Create(tvChannelPackage);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            TVChannelPackage tvChannelPackage = actions.Get(id);
            return View(tvChannelPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TVChannelPackage tvChannelPackage)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannelPackage);
            }

            actions.Edit(tvChannelPackage);
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