using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class HomeController : Controller
    {
        HomeDAL homedal = new HomeDAL();
        public ActionResult Index()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(UserInformation users)
        {
            bool result = homedal.SAveUsersToDataBase(users);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

    }
}