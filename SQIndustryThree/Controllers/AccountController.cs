
using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class AccountController : Controller
    {

        HomeDAL homeDAL = new HomeDAL();
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
                result.isSuccess = false;
                result.msg = Url.Action("CapexInformationView", "CapexApproval");
                Session["SQuserId"] = users.UserInformationId;
                Session["SQuserName"] = users.UserInformationName;
                Session["SQdesignation"] = users.DesignationId;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            bool result = true;
            Session.Abandon();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}