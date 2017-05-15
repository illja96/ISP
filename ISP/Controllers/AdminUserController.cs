using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    [Authorize(Roles = "Administrator, Support")]
    public class AdminUserController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}