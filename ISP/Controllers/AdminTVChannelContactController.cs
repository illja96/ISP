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
    public class AdminTVChannelContactController : Controller
    {
        private TVChannelContractActions actions;
        private UserActions userActions;
        private ContractAddressActions contractAddressActions;

        public AdminTVChannelContactController()
        {
            actions = new TVChannelContractActions();
            userActions = new UserActions();
            contractAddressActions = new ContractAddressActions();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid contractAddressId, Guid tvChannelId)
        {
            ContractAddress contractAddress = contractAddressActions.Get(contractAddressId);
            User user = userActions.GetById(contractAddress.SubscriberId);

            if (!actions.CanSubscribe(contractAddressId, tvChannelId))
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
                actions.Create(user.Id, contractAddressId, tvChannelId);

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
            TVChannelContract tvChannelContract = actions.GetNotCanceled(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Support") || User.Identity.Name == tvChannelContract.Subscriber.UserName)
            {
                actions.Cancel(tvChannelContract.Id);

                if (User.Identity.Name == tvChannelContract.Subscriber.UserName)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Details", "AdminUser", new { @id = tvChannelContract.SubscriberId });
                }

            }
            else
            {
                throw new Exception();
            }
        }
    }
}