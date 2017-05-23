using ISP.BLL.ModelActions;
using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    [Authorize]
    public class AdminInternetPackageContractController : Controller
    {
        private InternetPackageContractActions actions;
        private UserActions userActions;
        private ContractAddressActions contractAddressActions;

        public AdminInternetPackageContractController()
        {
            actions = new InternetPackageContractActions();
            userActions = new UserActions();
            contractAddressActions = new ContractAddressActions();
        }

        public ActionResult Create(Guid contractAddressId, Guid internetPackageId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            User user = userActions.GetById(contractAddress.SubscriberId);

            if (!actions.CanSubscribe(contractAddressId, internetPackageId))
            {
                TempData["notEnoughBalance"] = true;

                if (User.Identity.Name == user.UserName)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Details", "AdminUser", new { @id = user.Id });
                }
            }

            if (User.IsInRole("Administrator") || User.IsInRole("Support") || User.Identity.Name == user.UserName)
            {
                actions.Create(user.Id, contractAddressId, internetPackageId);

                if (User.Identity.Name == user.UserName)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Details", "AdminUser", new { @id = user.Id });
                }
            }
            else
            {
                throw new Exception();
            }
        }
        public ActionResult Cancel(Guid id)
        {
            InternetPackageContract internetPackageContract = actions.GetNotCanceled(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Support") || User.Identity.Name == internetPackageContract.Subscriber.UserName)
            {
                actions.Cancel(internetPackageContract.Id);

                if (User.Identity.Name == internetPackageContract.Subscriber.UserName)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Details", "AdminUser", new { @id = internetPackageContract.SubscriberId });
                }

            }
            else
            {
                throw new Exception();
            }
        }
    }
}