using ISP.BLL.ModelActions;
using ISP.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    public class HomeController : Controller
    {
        private InternetPackageActions internetPackageActions;
        private TVChannelActions tvChannelActions;
        private TVChannelPackageActions tvChannelPackageActions;

        public HomeController()
        {
            internetPackageActions = new InternetPackageActions();
            tvChannelActions = new TVChannelActions();
            tvChannelPackageActions = new TVChannelPackageActions();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InternetPackage()
        {
            IEnumerable<InternetPackageDetails> internetPackages = internetPackageActions.GetAllNotCanceled().Select(item => new InternetPackageDetails(item)).ToArray();
            return View(internetPackages);
        }

        public ActionResult TVChannelPackage()
        {
            IEnumerable<TVChannelPackageDetails> tvChannelPackages = tvChannelPackageActions.GetAllNotCanceled().Select(item => new TVChannelPackageDetails(item)).ToArray();
            return View(tvChannelPackages);
        }

        public ActionResult TVChannel()
        {
            IEnumerable<TVChannelDetails> tvChannels = tvChannelActions.GetAllNotCanceled().Select(item => new TVChannelDetails(item)).ToArray();
            return View(tvChannels);
        }

        public ActionResult Connect()
        {
            return View();
        }
    }
}