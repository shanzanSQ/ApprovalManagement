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
    public class VehicleController : Controller
    {
        // GET: Vehicle
        VehicleDAL vehicleDAL = new VehicleDAL();


        public ActionResult Index()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Vehicle");
        }
        public ActionResult VehicleApproverView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult COOVehicleApproverView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult StationManagerVehicleApproverView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult CentralStationManagerVehicleApproverView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult VehicleBudgetIndex()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("CourierBudgetIndex", "Courier");
        }
        public ActionResult AddVehicleRate()
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("AddVehicleRate", "Courier");
        }
        public ActionResult GetVehiclePartial(string viewName)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return PartialView(viewName);
        }
        public ActionResult ApproverListUnitBased(int unit, int DepartmentHeadId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CourierApproverModel> list = new List<CourierApproverModel>();
            if (userID == DepartmentHeadId)
            {
                list = this.vehicleDAL.GetApproverCategoryBased(unit, 0);
            }
            else
            {
                list = this.vehicleDAL.GetApproverCategoryBased(unit, DepartmentHeadId);
            }

            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult approverlistnewUnitBased(int unit)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            List<CourierApproverModel> list = this.vehicleDAL.GetApproverUnitBased(unit);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult approverlistnewdepartment_head(int DepartmentHeadId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            List<CourierApproverModel> list = this.vehicleDAL.GetApproverDeoartment_headBased(DepartmentHeadId);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModalBeforeVehicleSubmit(VehicleRequestModel vehicleRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            dynamic user = this.vehicleDAL.UserDetails(userID);
            vehicleRequestModel.RequestorName = (string)user.UserName;
            vehicleRequestModel.RequestorEmail = (string)user.UserEmail;
            vehicleRequestModel.RequestorDesignation = (string)user.DesignationName;
            vehicleRequestModel.RequerstorMobile = (string)user.UserPhone;
            vehicleRequestModel.Created_By = (int)user.UserId;
            // IouRequestmodal.SettlementDate = "";
            return PartialView("_modalRequestView", vehicleRequestModel);
        }
        [HttpPost]
        public ActionResult SaveVehicleRequest(VehicleRequestModel vehicleRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = vehicleDAL.SaveVehicleRequest(vehicleRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllVehicleRequest(int Status, string ViewName, int Progrss)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView(ViewName, vehicleDAL.GetAllVehicleRequest(userID, Status, Progrss));
        }
        [HttpPost]
        public ActionResult VehicleRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            VehicleRequestModel iOURequest = vehicleDAL.VehicleDetailsInformation(MasterID, userID);
            iOURequest.Status = Status;
            return PartialView(ViewName, iOURequest);
        }
        [HttpPost]
        public ActionResult VehicleApproveOrReject(string CommentText, int Progress, int VehicleRequestId, int DeligationUserId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(vehicleDAL.VehicleApproveOrReject(Progress, CommentText, userID, VehicleRequestId, DeligationUserId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult VehicleDCApproveOrReject(string CommentText, int Progress, int VehicleRequestId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(vehicleDAL.VehicleDCApproveOrReject(Progress, CommentText, userID, VehicleRequestId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SendVehicleComments(int MasterID, int ReviewTo, string ReviewMessage)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(vehicleDAL.CommentSent(MasterID, ReviewTo, ReviewMessage, userID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateVehicleRequest(VehicleRequestModel iOURequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = vehicleDAL.UpdateVehicleRequest(iOURequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBudgetVehiclePartial(string viewName)
        {

            if (base.Session["SQuserId"] != null)
            {
                return base.PartialView(viewName);
            }
            return base.RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public ActionResult SaveVehicleBudget(VehicleRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = vehicleDAL.SaveVehicleBudget(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadPreferredVehicle()
        {
            return Json(vehicleDAL.LoadPreferredVehicle(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadBudgetHeader()
        {
            return Json(vehicleDAL.LoadBudgetHeader(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadPurposeofVisit(int LocationId)
        {
            return Json(vehicleDAL.LoadPurposeofVisit(LocationId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadStartingPoint(int LocationId)
        {
            return Json(vehicleDAL.LoadStartingPoint(LocationId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadRoute(int starting_point)
        {
            return Json(vehicleDAL.LoadRoute(starting_point), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadBusinessUnit(int starting_point)
        {
            return Json(vehicleDAL.LoadBusinessUnit(starting_point), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllVehicleBudget(int Status, string ViewName, int Progrss)
        {
            try
            {
                if (base.Session["SQuserId"] == null)
                {
                    return base.RedirectToAction("Index", "Account");
                }
                int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
                return this.PartialView(ViewName, vehicleDAL.GetAllVehicleBudget(Status, Progrss));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActionResult VehicleAllocation()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        public ActionResult VehicleAllocationList()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public ActionResult AllVehicleRequestsForAllocation()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return Json(vehicleDAL.GetAllVehicleRequestsForAllocation((int)Session["SQuserId"]), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VehicleAllocateForUsers(VehicleAllocationMaster data)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            data.TransactionBy = (int)Session["SQuserId"];
            return Json(vehicleDAL.AllocateVehicleForUserFromMaster(data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllVehicleAllocationList()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            int id = (int)Session["SQuserId"];
            return Json(vehicleDAL.GetAllVehicleAllocationList(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllVehicleAllocationPassengerList(int id)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return Json(vehicleDAL.GetAllPassengers(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadVehicleVendorDropDownOptions(int routeId, int tripTypeId, int vehicleTypeId)
        {
            return Json(vehicleDAL.GetVehicleVendorDropDownOptions(routeId, tripTypeId, vehicleTypeId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadVehicleTypeDropDownOptions(int routeId, int tripTypeId)
        {
            return Json(vehicleDAL.GetVehicleTypeDropDownOptions(routeId, tripTypeId), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult loadRouteDropDownOptions(int startPointId)
        //{
        //    return Json(vehicleDAL.GetRouteDropDownOptions(startPointId), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult VehicleInfo(int id)
        {
            return Json(vehicleDAL.GetVehicleInfo(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleCost(RateMatrix data)
        {
            return Json(vehicleDAL.GetVehicleCost(data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleRequestById(int id)
        {
            return Json(vehicleDAL.GetVehicleRequestById(id), JsonRequestBehavior.AllowGet);
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
                VehicleRequestModel courierRequestModel = this.vehicleDAL.GetcourierBudget(userID, courierBudgetEntryId);
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
        public ActionResult UpdateCourierBudget(VehicleRequestModel courierRequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = vehicleDAL.UpdateCourierBudget(courierRequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CostAdjustment(int TripId, int AdjustmentValue)
        {
            var passengerList = vehicleDAL.AdjustCost(TripId, AdjustmentValue);
            int tripCost = vehicleDAL.GetTripCost(TripId);
            return Json(vehicleDAL.allocateCostForRequest(passengerList, tripCost), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult TripCost(int tripId)
        //{
        //    return Json(, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult AllRateMatrix()
        {
            return Json(new { data = vehicleDAL.GetAllRateMatrix() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadVehicleVendorDropDownOptionsForRate()
        {
            return Json(vehicleDAL.GetVehicleVendorDropDownOptions(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadVehicleTypeDropDownOptions1()
        {
            return Json(vehicleDAL.GetVehicleTypeDropDownOptions(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadRouteDropDownOptions1()
        {
            return Json(vehicleDAL.GetRouteDropDownOptions(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddRateMatrix(RateMatrix data)
        {
            return Json(vehicleDAL.AddRateMatrix(data), JsonRequestBehavior.AllowGet);
        }
    }
}