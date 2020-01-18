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
        public ActionResult GetUserInformation()
        {

            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            UserInformation users = homedal.GetUserInformation(userID);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RecoveryPassword()
        {

            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            bool result= homedal.RecoveryPassword(userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(string email,string oldpass, string newpass)
        {
            bool result = false;
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            UserInformation users = homedal.CheckUserLogin(email, oldpass);
            if (users.Empty)
            {
                result = false;
            }
            else
            {
                result = homedal.changePassword(userID, newpass);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}