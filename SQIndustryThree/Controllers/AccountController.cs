
using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class AccountController : Controller
    {

        HomeDAL homeDAL = new HomeDAL();
        CapexApprovalDAL capexApproval = new CapexApprovalDAL();
        AdminDAL admin = new AdminDAL();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string UserEmail,string UserPassword)
        {
            ResultResponse result = new ResultResponse();
            UserInformation users = homeDAL.CheckUserLogin(UserEmail, UserPassword);
            if (users.Empty)
            {
                result.isSuccess = true;
                result.msg = "Wrong Username Or Password";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ModuleModel> moduleList = new List<ModuleModel>();
                moduleList = homeDAL.GetModuleByUser(users.UserInformationId,0);
                if (moduleList.Count<=0)
                {
                    result.isSuccess = true;
                    result.msg = "You Don't Have Permission To This System";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result.isSuccess = false;
                    result.msg = Url.Action(moduleList[0].ModuleValue,moduleList[0].ModuleController);
                    Session["SQuserId"] = users.UserInformationId;
                    Session["SQuserName"] = users.UserInformationName;
                    int permission = capexApproval.ModulePermission(1, users.UserInformationId);
                    if (permission != 1)
                    {
                        Session["Requestor"] = 0;
                    }
                    else
                    {
                        Session["Requestor"] = 1;
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
               
            }

        }
        public ActionResult Logout()
        {
            bool result = true;
            Session.Abandon();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadPermissionMenu(int ProjectId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            List<ModuleModel> moduleList = new List<ModuleModel>();
            moduleList = homeDAL.GetModuleByUser(userid,ProjectId);
            return Json(moduleList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadProjectMenu()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            List<ModuleModel> moduleList = new List<ModuleModel>();
            moduleList = homeDAL.GetProjectmenu(userid);
            return Json(moduleList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string Email)
        {
            bool result = admin.RecoveryPassword(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}