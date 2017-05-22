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
            User user = GetCurrentUser();
            return View(user);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            return RedirectToAction("Index");
        }
        public ActionResult ContractAddress()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContractAddress(ContractAddress contractAddress)
        {
            if (!ModelState.IsValid)
            {
                return View(contractAddress);
            }

            return RedirectToAction("ContractAddress");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContractAddress(ContractAddress contractAddress)
        {
            if(!ModelState.IsValid)
            {
                return View(contractAddress);
            }

            return RedirectToAction("ContractAddress");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractAddress(Guid contractAddressId)
        {
            return RedirectToAction("ContractAddress");
        }

        public ActionResult InternetPackageContract()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInternetPackageContract(Guid internetPackageContractId)
        {
            InternetPackage internetPackage = internetPackageActions.GetNotCanceled(internetPackageContractId);
            User user = GetCurrentUser();
            InternetPackageContract internetPackageContract = new InternetPackageContract();
            user.InternetPackageContracts.Add(internetPackageContract);
            actions.Edit(user);

            return RedirectToAction("InternetPackageContract");
        }

        public ActionResult TVChannelContract()
        {
            return View();
        }

        public ActionResult TVChannelPackageContract()
        {
            return View();
        }

        private User GetCurrentUser()
        {
            string userNumber = User.Identity.Name;
            User user = actions.Get(userNumber);
            return user;
        }
        private double CalculatePrice(InternetPackage internetPackage)
        {
            int currentYear = DateTime.UtcNow.Year;
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day;

            int totalDaysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int leftDaysInMonth = totalDaysInMonth - currentDay;

            double fullPrice = internetPackage.Price;
            double pricePerDay = internetPackage.Price / totalDaysInMonth;
            double currentPrice = pricePerDay * leftDaysInMonth;

            return currentPrice;
        }
        private double CalculatePrice(TVChannel tvChannel)
        {
            int currentYear = DateTime.UtcNow.Year;
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day;

            int totalDaysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int leftDaysInMonth = totalDaysInMonth - currentDay;

            double fullPrice = tvChannel.Price;
            double pricePerDay = tvChannel.Price / totalDaysInMonth;
            double currentPrice = pricePerDay * leftDaysInMonth;

            return currentPrice;
        }
        private double CalculatePrice(TVChannelPackage tvChannelPackage)
        {
            int currentYear = DateTime.UtcNow.Year;
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day;

            int totalDaysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int leftDaysInMonth = totalDaysInMonth - currentDay;

            double fullPrice = tvChannelPackage.Price;
            double pricePerDay = tvChannelPackage.Price / totalDaysInMonth;
            double currentPrice = pricePerDay * leftDaysInMonth;

            return currentPrice;
        }
    }
}