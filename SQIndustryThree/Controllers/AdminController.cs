using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class AdminController : Controller
    {
        AdminDAL adminDAL = new AdminDAL();
        CapexApprovalDAL capexApprovalDAL = new CapexApprovalDAL();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminPanel()
        {
            if (Session["SQAdminID"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult ApprovalManagementSystem()
        {
            if (Session["SQAdminID"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
       
        [HttpPost]
        public ActionResult AllApprovedCapex(int BusinessUnitID,int CatagoryID,string SelectDate,string EndDate)
        {
            if (Session["SQAdminID"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            int userID = Convert.ToInt32(Session["SQAdminID"].ToString());
            List<CapexInformationMaster> capexInformationList = adminDAL.GetALLCapexInfo(BusinessUnitID, CatagoryID, SelectDate, EndDate);
            return PartialView("_pertialCapexForAdmin", capexInformationList);
        }

        [HttpPost]
        public ActionResult IndividualCapexShow(int primarykey)
        {
            if (Session["SQAdminID"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            int userID = 0;
            return PartialView("_capexIdShowAdmin", capexApprovalDAL.GetSavedCapex(userID, primarykey));
        }


        [HttpPost]
        public ActionResult CheckAdminLogin(string UserEmail,string UserPassword)
        {
            ResultResponse result = new ResultResponse();
            UserInformation users = adminDAL.AdminUserLogin(UserEmail, UserPassword);
            if (users.Empty)
            {
                result.isSuccess = true;
                result.msg = "Wrong Username Or Password";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.isSuccess = false;
                result.msg = Url.Action("DashboardAdmin", "Admin");
                Session["SQAdminID"] = users.UserInformationId;
                Session["SQAdminName"] = users.UserInformationName;
                Session["SQAdminEmail"] = users.UserInformationEmail;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult DashboardAdmin()
        {
            return View();
        }
        public ActionResult ChangeDivView(int status)
        {
            return PartialView();
        }
    }
}