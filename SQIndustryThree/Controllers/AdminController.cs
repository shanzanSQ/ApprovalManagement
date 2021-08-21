using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using SQIndustryThree.Models.BillApproval;
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

        [HttpPost]
        public ActionResult LoadBillSupplier()
        {
            return Json(adminDAL.GetAllBillSuppliers(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadBillCurrency()
        {
            return Json(adminDAL.GetAllBillCurrency(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadBillUnit()
        {
            return Json(adminDAL.GetAllBillUnits(), JsonRequestBehavior.AllowGet);
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
                case 9:
                    viewName = "_billSupplierList";
                    break;
                case 10:
                    viewName = "_billCurrencyList";
                    break;
                case 11:
                    viewName = "_billUnitList"; //_billUnitList
                    break;
            }
            return PartialView(viewName);
          
        }


        public ActionResult GetAllBillSuppliers()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                //Loading
                List<BillSupplier> data = adminDAL.GetAllBillSuppliers();

                //Total record count
                int totalRecords = data.Count;

                //Verification
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    //Apply Search
                    data = data.Where(a =>
                    a.SupplierID.ToString().ToLower().Contains(search.ToLower()) ||
                    a.Supplier.ToLower().Contains(search.ToLower())).ToList();
                }

                //Sorting
                data = adminDAL.SupplierSorting(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            return result;
        }

        public ActionResult GetAllBillCurrency()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                //Loading
                List<BillCurrency> data = adminDAL.GetAllBillCurrency();

                //Total record count
                int totalRecords = data.Count;

                //Verification
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    //Apply Search
                    data = data.Where(a =>
                    a.CurrencyID.ToString().ToLower().Contains(search.ToLower()) ||
                    a.CurrencyCode.ToLower().Contains(search.ToLower()) ||
                    a.Currency.ToLower().Contains(search.ToLower())).ToList();
                }

                //Sorting
                data = adminDAL.CurrencySorting(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            return result;
        }

        public ActionResult GetAllBillUnits()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                //Loading
                List<BillUnit> data = adminDAL.GetAllBillUnits();

                //Total record count
                int totalRecords = data.Count;

                //Verification
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    //Apply Search
                    data = data.Where(a =>
                    a.UnitID.ToString().ToLower().Contains(search.ToLower()) ||
                    a.UnitName.ToLower().Contains(search.ToLower())).ToList();
                }

                //Sorting
                data = adminDAL.UnitSorting(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            return result;
        }

    }
}