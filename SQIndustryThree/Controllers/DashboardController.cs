using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class DashboardController : Controller
    {

        DashboardDAL dashboardDAL = new DashboardDAL();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashboardCapex()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult LoadAllInformation(int year,int catagory)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(dashboardDAL.GetApproveStatus(userID,year,catagory), JsonRequestBehavior.AllowGet); 
        }

    }
}