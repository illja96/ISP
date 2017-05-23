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
    public class AdminTVChannelPackageContractController : Controller
    {
        private TVChannelPackageContractActions actions;
        private UserActions userActions;
        private ContractAddressActions contractAddressActions;

        public AdminTVChannelPackageContractController()
        {
            actions = new TVChannelPackageContractActions();
            userActions = new UserActions();
            contractAddressActions = new ContractAddressActions();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid contractAddressId, Guid tvChannelPackageId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            User user = userActions.GetById(contractAddress.SubscriberId);

            if (!actions.CanSubscribe(contractAddressId, tvChannelPackageId))
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
                actions.Create(user.Id, contractAddressId, tvChannelPackageId);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid id)
        {
            TVChannelPackageContract tvChannelPackageContract = actions.GetNotCanceled(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Support") || User.Identity.Name == tvChannelPackageContract.Subscriber.UserName)
            {
                actions.Cancel(tvChannelPackageContract.Id);

                if (User.Identity.Name == tvChannelPackageContract.Subscriber.UserName)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Details", "AdminUser", new { @id = tvChannelPackageContract.SubscriberId });
                }

            }
            else
            {
                throw new Exception();
            }
        }
    }
}