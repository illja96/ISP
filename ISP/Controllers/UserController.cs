using ISP.BLL.ModelActions;
using ISP.DAL.DBModels;
using ISP.DAL.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserActions actions;
        private InternetPackageActions internetPackageActions;
        private TVChannelActions tvChannelActions;
        private TVChannelPackageActions tvChannelPackagesActions;

        public UserController()
        {
            actions = new UserActions();
            internetPackageActions = new InternetPackageActions();
            tvChannelActions = new TVChannelActions();
            tvChannelPackagesActions = new TVChannelPackageActions();
        }

        public ActionResult Index()
        {
            User user = actions.GetById(User.Identity.GetUserId());
            return View(user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangeUserPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = actions.ChangePassowrd(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["PasswordChanged"] = true;
                return RedirectToAction("Index", "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }
        }
    }
}