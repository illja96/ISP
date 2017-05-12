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
    public class AdminTVChannelController : Controller
    {
        private TVChannelRepo tvChannelRepo = new TVChannelRepo();

        public ActionResult Index()
        {
            Dictionary<string, string> sortBy;
            Dictionary<string, bool> orderByDescending;
            tvChannelRepo.GetAvailableSortList(out sortBy, out orderByDescending);

            ViewBag.sortBy = sortBy.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value });
            ViewBag.orderByDescending = orderByDescending.Select(item => new SelectListItem() { Text = item.Key, Value = item.Value.ToString() });

            return View();
        }

        public ActionResult IndexTableAjax(string sortBy = "Id", bool orderByDescending = false)
        {
            IEnumerable<TVChannel> tvChannels = tvChannelRepo.GetAllCancelled();
            IEnumerable<TVChannel> tvChannelsSorted = tvChannelRepo.Sort(tvChannels, sortBy, orderByDescending);
            IEnumerable<TVChannelDetails> tvChannelsDetailsSorted = tvChannelsSorted.Select(tvChannel => new TVChannelDetails(tvChannel)).ToArray();

            return PartialView(tvChannelsDetailsSorted);
        }

        public ActionResult DetailsAjax(Guid id)
        {
            TVChannelDetails tvChannel = new TVChannelDetails(tvChannelRepo.GetCancelled(id));
            return PartialView(tvChannel);
        }

        public ActionResult Create()
        {
            TVChannel tvChannel = new TVChannel();
            return View();
        }

        [HttpPost]
        public ActionResult Create(TVChannel tvChannel)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannel);
            }

            tvChannelRepo.Create(tvChannel);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            TVChannel tvChannel = tvChannelRepo.GetCancelled(id);
            return View(tvChannel);
        }

        [HttpPost]
        public ActionResult Edit(TVChannel tvChannel)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannel);
            }

            tvChannelRepo.Edit(tvChannel);
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(Guid id)
        {
            tvChannelRepo.Cancel(id);
            return RedirectToAction("Index");
        }

        public ActionResult Renew(Guid id)
        {
            tvChannelRepo.Renew(id);
            return RedirectToAction("Index");
        }
    }
}