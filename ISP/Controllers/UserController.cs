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
        private ContractAddressActions contractAddressActions;
        private InternetPackageActions internetPackageActions;
        private TVChannelActions tvChannelActions;
        private TVChannelPackageActions tvChannelPackagesActions;

        public UserController()
        {
            actions = new UserActions();
            contractAddressActions = new ContractAddressActions();
            internetPackageActions = new InternetPackageActions();
            tvChannelActions = new TVChannelActions();
            tvChannelPackagesActions = new TVChannelPackageActions();
        }

        public ActionResult Index()
        {
            User user = actions.GetById(User.Identity.GetUserId());
            return View(user);
        }

        public ActionResult DetailsInternetPackageContract(Guid contractAddressId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            IEnumerable<InternetPackageContract> internetPackageContracts = contractAddress.InternetPackageContracts.OrderByDescending(item => item.Number).ToArray();

            return PartialView(internetPackageContracts);
        }
        public ActionResult DetailsTVChannelContract(Guid contractAddressId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            IEnumerable<TVChannelContract> tvChannelContracts = contractAddress.TVChannelContracts.OrderByDescending(item => item.Number).ToArray();

            return PartialView(tvChannelContracts);
        }
        public ActionResult DetailsTVChannelPackageContract(Guid contractAddressId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            IEnumerable<TVChannelPackageContract> tvChannelPackageContracts = contractAddress.TVChannelPackageContracts.OrderByDescending(item => item.Number).ToArray();

            return PartialView(tvChannelPackageContracts);
        }

        public ActionResult TopUpBalance()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TopUpBalance(double amount)
        {
            User user = actions.GetById(User.Identity.GetUserId());
            user.Balance += amount;
            actions.Edit(user);

            return RedirectToAction("Index");
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