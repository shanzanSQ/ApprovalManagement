using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using SQIndustryThree.Models.VisitorApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class CourierController : Controller
    {
        // GET: Courier
        CourierDAL courierDAL = new CourierDAL();

        public ActionResult Index()
        {
            
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Courier");
        }
        public ActionResult CourierBudgetIndex()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CourierBudgetIndex", "Courier");
        }
        public ActionResult CourierBudgetWindow()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CourierBudgetWindow", "Courier");
        }
        [HttpPost]
        public ActionResult LoadBusinessUnit()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(courierDAL.GetBusinessUnits(userID), JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult LoadFiscalYear()
        //{
        //    return Json(courierDAL.GetBusinessUnits(), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetDepartmentList(int location)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.courierDAL.GetDepartmentList(location), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateCourierType()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CreateCourierType", "Courier");
        }
        public ActionResult CourierTypeInformationView()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CourierTypeInformationView", "Courier");
        }
        public ActionResult FrontDeskCourierView()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("FrontDeskCourierView", "Courier");
        }
        public ActionResult CourierDispatchListView()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CourierDispatchListView", "Courier");
        }
        public ActionResult DispatchIndex()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("DispatchIndex", "Courier");
        }
        public ActionResult InboundDispatchIndex()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("InboundDispatchIndex", "Courier");
        }
        public ActionResult GetCourierPartial(string viewName)
        {
         
            if (base.Session["SQuserId"] != null)
            {
                return base.PartialView(viewName);
            }
            return base.RedirectToAction("Index", "Account");
        }
        public ActionResult GetCourierDispatchPartial(string viewName)
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.PartialView(viewName);
            }
            return base.RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public ActionResult SaveCourierRequest(CourierRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.SaveCourierRequest(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCourierBudget(CourierRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.SaveCourierBudget(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCourierProposedDate(string country, string delivery_date,string weight,string type)
        {
            Session["country"] = country;
            Session["delivery_date"] = delivery_date;
            Session["weight"] = weight;
            Session["Courier"] = null;
            Session["proposed_date"] = null;
            Session["cost"] = null;
            CourierRequestModel data = new CourierRequestModel();
            var dataGet = courierDAL.GetCourierProposedDate(country, delivery_date, weight,type);
            if (dataGet.Count > 0)
            {
                data = dataGet.FirstOrDefault();
                Session["Courier"] = (string)data.ServiceProvider;
                Session["proposed_date"] = (string)data.CourierProposedDate;
                Session["cost"] = (string)data.Rate;
             }
            return Json(new { data = dataGet });

        }
        public ActionResult GetcourierWiseCostDate(string courier, string delivery_date, string weight,string type)
        {
             var dataGet = courierDAL.GetcourierWiseCostDate(courier, delivery_date, weight, type);
            return Json(new { data = dataGet });

        }
      
        public ActionResult GetproposedDateWisecourierCostDate(string country, string delivery_date, string weight, string type,string proposed_date)
        {
            var dataGet = courierDAL.GetproposedDateWisecourierCostDate(country, delivery_date, weight, type, proposed_date);
            return Json(new { data = dataGet });

        }
        public ActionResult LoadFiscalYear()
        {
            var dataGet = courierDAL.LoadFiscalYear();
            return Json(new { data = dataGet });

        }
        public ActionResult GetcourierWiseConsolidateCostDate(string Courier, string CountryName, string Weight)
        {
            try
            {
                var dataGet = courierDAL.GetcourierWiseConsolidateCostDate(Courier, CountryName, Weight);
                return Json(new { data = dataGet });
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }
        public ActionResult GetCourierList(string country,string delivery_date,string weight, string type)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.courierDAL.GetCourierProposedDate(country, delivery_date, weight, type), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ShowPertialviewModal(CourierRequestModel CourierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_modalCourierRequestView", CourierRequestModel);
        }
        [HttpPost]
        public ActionResult ShowPertialviewModalType(CourierRequestModel CourierRequestModel)
        {
            try
            {
                if (Session["SQuserId"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(Session["SQuserId"].ToString());
                return PartialView("_modalCourierRequestFontDeskView", CourierRequestModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        [HttpPost]
        public ActionResult InboundShowPertialviewModalType(CourierRequestModel CourierRequestModel)
        {
            try
            {
                if (Session["SQuserId"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(Session["SQuserId"].ToString());
                return PartialView("_modalInboundCourierRequestFontDeskView", CourierRequestModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public ActionResult GetAllCourierRequest(int Status, string ViewName, int Progrss)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                return this.PartialView(ViewName, courierDAL.GetAllCourierRequest(userID, Status, Progrss));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        [HttpPost]
        public ActionResult GetAllCourierBudget(int Status, string ViewName, int Progrss)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                return this.PartialView(ViewName, courierDAL.GetAllCourierBudget(Status, Progrss));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        [HttpPost]
        public ActionResult GetAllCourierDispatch(int Status, string ViewName)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                return this.PartialView(ViewName, courierDAL.GetAllCourierDispatch(Status));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public ActionResult GetAllCourierReceived(int Status, string ViewName)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                return this.PartialView(ViewName, courierDAL.GetAllCourierReceived(Status));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActionResult ApproverList(int status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            List<VisitorApprover> list = this.courierDAL.GetApprovers(userID,status);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Approverlistnew(int status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            List<VisitorApprover> list = this.courierDAL.GetApproverlistnew(userID,status);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CourierRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            CourierRequestModel courierRequestModel = this.courierDAL.CourierDetailsInformation(MasterID, userID);
            courierRequestModel.Status = Status;
            return this.PartialView(ViewName, courierRequestModel);
        }
        [HttpPost]
        public ActionResult CourierDispatchedRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            CourierRequestModel courierRequestModel = this.courierDAL.CourierDispatchedRequestDetails(MasterID, userID);
            courierRequestModel.Status = Status;
            return this.PartialView(ViewName, courierRequestModel);
        }
        [HttpPost]
        public ActionResult CourierReceivedRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            CourierRequestModel courierRequestModel = this.courierDAL.CourierReceivedRequestDetails(MasterID, userID);
            courierRequestModel.Status = Status;
            return this.PartialView(ViewName, courierRequestModel);
        }
        [HttpPost]
        public ActionResult CourierDispatchDetails(int MasterID, string ViewName, int Status)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                //CourierRequestModel courierRequestModel = this.courierDAL.CourierDispatchDetailsInformation(MasterID);
                //courierRequestModel.Status = Status;
                //return this.PartialView(ViewName, courierRequestModel);
                return this.PartialView(ViewName, courierDAL.CourierDispatchDetailsInformation(MasterID));
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
       
        [HttpPost]
        public ActionResult LoadCountry(string type)
        {
            return Json(courierDAL.GetCountry(type), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadCourier()
        {
            return Json(courierDAL.GetCourier(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadCustomer()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(courierDAL.GetCustomer(userID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWeightByCourierDispatchId()
        {
            var dataGet = courierDAL.GetWeightByCourierDispatchId();
            return Json(new { data = dataGet });
            // return Json(courierDAL.GetWeightByCourierDispatchId(), JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult GetWeightByCourierDispatchId()
        //{
        //    return Json(courierDAL.GetWeightByCourierDispatchId(), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult SendCourieComments(int MasterID, int ReviewTo, string ReviewMessage)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(courierDAL.CommentSent(MasterID, ReviewTo, ReviewMessage, userID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexApprover()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CourierApproveOrReject(string CommentText, int Progress, int CourierRequestId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(courierDAL.CourierApproveOrReject(Progress, CommentText, userID, CourierRequestId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ShowCourierTypePertialviewModal(CourierType courierType)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_modalShowCourierTypeinfo", courierType);
        }
        [HttpPost]
        public ActionResult SaveCourierType(CourierType courierType)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.SaveCourierTypeDatabase(courierType, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult courierTypeCheck(CourierTypeDetails courierTypeDetails)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CourierTypeDetails> CourierTypeList = courierDAL.courierTypeCheck(courierTypeDetails, userID);
             return Json(CourierTypeList, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult showCourierTypeInformation(int status,string type)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CourierTypeDetails> CourierTypeList = courierDAL.GetCourierTypeInfo( status, type);
            return PartialView("_courierTypePertialView", CourierTypeList);
        }
        [HttpPost]
        public ActionResult showCourierTypeInformationPreview(int status, string type)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CourierTypeDetails> CourierTypeList = courierDAL.GetCourierTypeInfo( status, type);
            return PartialView("_courierTypePertialViewPreview", CourierTypeList);
        }
        [HttpPost]
        public ActionResult showCourierTypeInformationPreviewByCountry(int status, string type,string country,string weight)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CourierTypeDetails> CourierTypeList = courierDAL.GetCourierTypeInfoByCountry(status, type, country, weight);
            return PartialView("_courierTypePertialViewPreview", CourierTypeList);
        }
        public ActionResult EditcourierType(int courierTypeid)
        {
            try
            {
                if (Session["SQuserId"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(Session["SQuserId"].ToString());
                CourierTypeDetails courierTypeDetails = this.courierDAL.GetcourierType( userID, courierTypeid);
                ////courierRequestModel.Status = Status;
                return this.PartialView("_updateCourierTypepertialView", courierTypeDetails);
              //  return PartialView("_updateCourierTypepertialView", courierDAL.GetcourierType(userID, courierTypeid));
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        public ActionResult EditcourierBudget(int courierBudgetEntryId)
        {
            try
            {
                if (Session["SQuserId"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(Session["SQuserId"].ToString());
                CourierRequestModel courierRequestModel = this.courierDAL.GetcourierBudget(userID, courierBudgetEntryId);
                ////courierRequestModel.Status = Status;
                return this.PartialView("_updateCourierBudgetpertialView ", courierRequestModel);
                //  return PartialView("_updateCourierTypepertialView", courierDAL.GetcourierType(userID, courierTypeid));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public ActionResult UpdateCourierType(CourierTypeDetails courierTypeDetails)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.UpdateCourierType(courierTypeDetails, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateCourierBudget(CourierRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.UpdateCourierBudget(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFrontDeskCourier(int Status)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userId = Convert.ToInt32(base.Session["SQuserId"]);
                List<CourierRequestModel> visitorInformation = new List<CourierRequestModel>();
                visitorInformation = this.courierDAL.GetFrontDeskCourier(Status, userId, 2);
                return this.PartialView("_allFrontDeskCourierPartialView", visitorInformation);
            }
            catch (Exception ex)
            {

                throw ex;
            }
       
        }

        [HttpPost]
        public ActionResult SaveCourierDispatch(CourierRequestModel courierRequestModel)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            string _dDname = "COURIER";
            long InvoiceNo = courierDAL.GetInvoice_No(_dDname);
            result = courierDAL.SaveCourierDispatchDatabase(courierRequestModel, userID, InvoiceNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCourierReceived(CourierRequestModel courierRequestModel)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            string _dDname = "COURIER";
            long InvoiceNo = courierDAL.GetInvoice_No(_dDname);
            result = courierDAL.SaveCourierReceivedDatabase(courierRequestModel, userID, InvoiceNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateCourierRequest(CourierRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = courierDAL.UpdateCourierRequest(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourierBudgetCheck(int business_unit, string financialYear, int month)
        {
            var dataGet = courierDAL.GetCourierBudgetCheck(business_unit, financialYear, month);
            return Json(new { data = dataGet });

        }
    }
}