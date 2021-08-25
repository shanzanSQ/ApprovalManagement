using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class DashboardController : Controller
    {

        DashboardDAL dashboardDAL = new DashboardDAL();
        CapexApprovalDAL capexApprovalDAL = new CapexApprovalDAL();
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
            int userid = Convert.ToInt32(Session["SQuserId"]);
            int permission = capexApprovalDAL.ModulePermission(3, userid);
            if (permission != 1)
            {
                return RedirectToAction("PendingCapex", "CapexApproval");
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