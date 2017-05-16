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
    public class AdminTVChannelController : Controller
    {
        private TVChannelActions actions = new TVChannelActions();

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
            IEnumerable<TVChannel> tvChannels = actions.GetAll();
            IEnumerable<TVChannel> tvChannelsSorted = actions.Sort(tvChannels, sortBy, orderByDescending);
            IEnumerable<TVChannelDetails> tvChannelsDetailsSorted = tvChannelsSorted.Select(tvChannel => new TVChannelDetails(tvChannel)).ToArray();

            return PartialView(tvChannelsDetailsSorted);
        }

        public ActionResult DetailsAjax(Guid id)
        {
            TVChannelDetails tvChannel = new TVChannelDetails(actions.Get(id));
            return PartialView(tvChannel);
        }

        public ActionResult Create()
        {
            TVChannel tvChannel = new TVChannel();
            return View(tvChannel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TVChannel tvChannel)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannel);
            }

            actions.Create(tvChannel);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            TVChannel tvChannel = actions.Get(id);
            return View(tvChannel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TVChannel tvChannel)
        {
            if (!ModelState.IsValid)
            {
                return View(tvChannel);
            }

            actions.Edit(tvChannel);
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