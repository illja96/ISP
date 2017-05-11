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
            return View();
        }
        
        public ActionResult IndexTableAjax()
        {
            var tvChannels = tvChannelRepo.GetAllCancelled().Select(tvChannel => new TVChannelDetails(tvChannel));

            return PartialView(tvChannels);
        }
        
        public ActionResult DetailsAjax(Guid id)
        {
            var tvChannel = new TVChannelDetails(tvChannelRepo.GetCancelled(id));

            return PartialView(tvChannel);
        }
    }
}