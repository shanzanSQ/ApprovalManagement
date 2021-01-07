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
        HomeDAL homeDAL = new HomeDAL();
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
        [HttpPost]
        public ActionResult LoadBusinessUnit()
        {
            return Json(adminDAL.GetBusinessUnits(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadCatagory()
        {
            return Json(capexApprovalDAL.GetCapexCatagory(0), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadDesignation()
        {
            return Json(adminDAL.GetAllDesignation(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadUsers()
        {
            return Json(adminDAL.GetAllUsers(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RegisterUser(UserInformation users)
        {
            bool result = homeDAL.SAveUsersToDataBase(users);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateDesignation(string DesignationName)
        {
            bool result = homeDAL.CreateDesignation(DesignationName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveApproverList(ApproverModelClass approverModelClass)
        {
            bool result = adminDAL.SaveApproverList(approverModelClass);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult showApproverList(int BusinessUnitId,int CatagoryId)
        {
            List<UserInformation> userlist  = adminDAL.ShowApproverListByBU(BusinessUnitId,CatagoryId);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeDivView(int status)
        {
            string viewName = "";
            switch(status){
                case 0:
                    viewName = "_createUsers";
                    break;
                case 1:
                    viewName = "_createDesignation";
                    break;
                case 6:
                    viewName = "_setApproverList";
                    break;
                case 8:
                    viewName = "_PartialViewExceptionApprover";
                    break;
                case 7:
                    viewName = "_iouSetapproverList";
                    break;
            }
            return PartialView(viewName);
          
        }
    }
}