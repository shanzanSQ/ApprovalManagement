using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using SQIndustryThree.Models.VisitorApproval;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class VisitorController : Controller
    {
        private VisitorDAL visitorDAL = new VisitorDAL();

        public VisitorController()
        {
        }

        public ActionResult ApproverList(int category, int subcategory, int unit)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            List<VisitorApprover> list = this.visitorDAL.GetApprovers(category, subcategory, unit);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApproverListCategoryBased(int category,int subcategory, int unit, int DepartmentHeadId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            List<VisitorApprover> list = this.visitorDAL.GetApproverCategoryBased(category,subcategory, unit, DepartmentHeadId);
            return base.Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeDivView(int status)
        {
            string viewName = "";
            if (status == 0)
            {
                viewName = "AdminApproverView";
            }
            else if (status == 1)
            {
                viewName = "_addPartialOperation";
            }
            return base.PartialView(viewName);
        }

        public ActionResult FactoryGateView()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }

        public ActionResult FactoryGateViewCelsius2()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }

        public ActionResult FrontDeskView()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }

        public ActionResult GateInfoUpdate(int visitorId, string visitorCardNo, string vehicleNo, string remarks, string checkin, string checkout)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            string data = "";
            dynamic result = this.visitorDAL.UpadteVisitorCheckinAndCheckOut(visitorId, visitorCardNo, vehicleNo, remarks, checkin, checkout);
            if (result != 0)
            {
                data = "Updated Data Successfully";
            }
            return base.Json(data);
        }

        [HttpPost]
        public ActionResult GetAllVisitorInformation(int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userId = Convert.ToInt32(base.Session["SQuserId"]);
            List<VisitorRequestModel> visitorInformation = new List<VisitorRequestModel>();
            if (visitorInformation != null)
            {
                visitorInformation = this.visitorDAL.GetAllVisitorInformation(Status, userId, 1);
            }
            
            return this.PartialView("_allRequestPartialView", visitorInformation);
        }

        [HttpPost]
        public ActionResult GetAllVisitorRequest(int Status, string ViewName, int Progrss)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            var approval = this.visitorDAL.GetAllVisitorRequest(userID, Status, Progrss);
            return this.PartialView(ViewName, approval);
        }

        [HttpPost]
        public ActionResult GetBussinessCategories(int unit)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.VisitorCategories(unit), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartmentList(int location)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.GetDepartmentList(location), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartments()
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.GetDepartments(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFactoryVisitor(int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            Convert.ToInt32(base.Session["SQuserId"]);
            List<VisitorRequestModel> visitorInformation = new List<VisitorRequestModel>();
            return this.PartialView("_allFrontDeskPartialView", this.visitorDAL.GetFactoryVisitorInformation(Status));
        }

        public ActionResult GetFrontDeskVisitor(int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userId = Convert.ToInt32(base.Session["SQuserId"]);
            List<VisitorRequestModel> visitorInformation = new List<VisitorRequestModel>();
            visitorInformation = this.visitorDAL.GetAllVisitorInformation(Status, userId, 2);
            return this.PartialView("_allFrontDeskPartialView", visitorInformation);
        }

        [HttpPost]
        public ActionResult GetSubMenuBYUserPermission()
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.visitorDAL.SUbMenuByPermission(userID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetVisitorCategories(int unit)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.VisitorCategories(unit), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVisitorPartial(string viewName)
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.PartialView(viewName);
            }
            return base.RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult GetVisitorSubCategories(int catId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.VisitorSubCategories(catId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult IndividualRequestShow(int PrimaryKey)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            VisitorRequestModel list = this.visitorDAL.IndividualRequestShow(PrimaryKey, userID);
            return this.PartialView("_modalVisitorRequest", list);
        }

        public ActionResult LoadApprovers(int unitId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            return base.Json(this.visitorDAL.GetApprovers(unitId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModalBeforeVisitorSubmit(RequestorModel visitor)
        {
            var result = string.Empty;

            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());


            //if (visitor.LocationId == 1)
            //{
            //    if (visitor.VisitMode == 0)
            //    {
            //        result = "Please Select Visit Mode";
            //    }
            //    else if (visitor.VisitMode == 1)
            //    {
            //        if (visitor.CategoryId == 0)
            //        {
            //            result = "Please Select Category";
            //        }
            //        else if (visitor.SubCategoryId == 0)
            //        {
            //            result = "Please Select Sub Category";
            //        }
            //        else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
            //        {
            //            result = "Please Select Department";
            //        }

            //    }
            //    else
            //    {
            //        if (visitor.CategoryId == 0)
            //        {
            //            result = "Please Select Category";
            //        }
            //        else if (visitor.SubCategoryId == 0)
            //        {
            //            result = "Please Select Sub Category";
            //        }
            //        else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
            //        {
            //            result = "Please Select Department";
            //        }
            //    }
            //}
            //else if (visitor.LocationId == 2)
            //{
            //    if (visitor.BusinessUnitId == 0)
            //    {
            //        result = "Please Select Bussiness Unit";
            //    }
            //    else if (visitor.CategoryId == 0)
            //    {
            //        result = "Please Select Category";
            //    }
            //    else if (visitor.SubCategoryId == 0)
            //    {
            //        result = "Please Select Sub Category";
            //    }
            //    else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
            //    {
            //        result = "Please Select Department";
            //    }
            //}
            //else
            //{
            //    result = visitor.ToString();
            //}


            dynamic user = this.visitorDAL.UserDetails(userID);
            visitor.RequestorName = (string)user.UserName;
            visitor.RequestorEmail = (string)user.UserEmail;
            visitor.RequestorDesignation = (string)user.DesignationName;
            visitor.RequerstorMobile = (string)user.UserPhone;
            visitor.Created_By = userID;
            return this.PartialView("_visitorModalRequestView", visitor);
        }

        public ActionResult ModalBeforeVisitorUpdate(RequestorModel visitor)
        {

            var result = string.Empty;
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            dynamic user = this.visitorDAL.UserDetails(userID);
            visitor.RequestorName = (string)user.UserName;
            visitor.RequestorEmail = (string)user.UserEmail;
            visitor.RequestorDesignation = (string)user.DesignationName;
            visitor.RequerstorMobile = (string)user.UserPhone;
            visitor.Created_By = (int)user.UserId;

            if (visitor.LocationId == 1)
            {
                if (visitor.VisitMode == 0)
                {
                    result = "Please Select Visit Mode";
                }else if(visitor.VisitMode == 1)
                {
                    if (visitor.CategoryId == 0)
                    {
                        result = "Please Select Category";
                    }
                    else if(visitor.SubCategoryId == 0)
                    {
                        result = "Please Select Sub Category";
                    }
                    else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
                    {
                        result = "Please Select Department";
                    }

                }
                else
                {
                    if (visitor.CategoryId == 0)
                    {
                        result = "Please Select Category";
                    }
                    else if (visitor.SubCategoryId == 0)
                    {
                        result = "Please Select Sub Category";
                    }
                    else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
                    {
                        result = "Please Select Department";
                    }
                }
            }
            else if (visitor.LocationId == 2)
            {
                if (visitor.BusinessUnitId == 0)
                {
                    result = "Please Select Bussiness Unit";
                }
                else if(visitor.CategoryId == 0)
                {
                    result = "Please Select Category";
                }
                else if (visitor.SubCategoryId == 0)
                {
                    result = "Please Select Sub Category";
                }
                else if (string.IsNullOrEmpty(visitor.RequestorDepartment))
                {
                    result = "Please Select Department";
                }
            }
            else
            {
                result = visitor.ToString();
            }
            

                return this.PartialView("_visitorModalRequestView", result);
        }

        [HttpPost]
        public ActionResult SaveVisitor(RequestorModel visitor)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            result = this.visitorDAL.SaveVisitor(visitor, userID);
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveVisitorForApprove(RequestorModel visitor)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.visitorDAL.SaveVistorRequest(visitor, userID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendVisitorComments(int MasterID, int ReviewTo, string ReviewMessage)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.visitorDAL.CommentSent(MasterID, ReviewTo, ReviewMessage, userID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateOrReject(int PrimaryKey, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.visitorDAL.UpdateOrReject(PrimaryKey, userID, Status), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateVisitorRequest(RequestorModel visitor)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            result = this.visitorDAL.UpdateVisitorRequest(visitor, userID);
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitorApproval()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }

        public ActionResult VisitorApproveOrReject(string CommentText, int Progress, int RequestorId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.visitorDAL.VisitorApproveOrReject(Progress, CommentText, userID, RequestorId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VisitorFileUpload()
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            string fileName = string.Empty;
            string ServerSavePath = string.Empty;
            if (base.Request.Files.Count > 0)
            {
                foreach (string str in base.Request.Files)
                {
                    HttpPostedFileBase file = base.Request.Files[str];
                    if (file == null)
                    {
                        continue;
                    }
                    long currentmilse = DateTime.Now.Ticks;
                    string InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string InputFileExtention = Path.GetExtension(file.FileName);
                    string FullFileWithext = string.Concat(InputFileName, currentmilse.ToString(), InputFileExtention);
                    ServerSavePath = Path.Combine(new string[] { string.Concat(base.Server.MapPath("~/Images/Visitors/"), FullFileWithext) });
                    file.SaveAs(ServerSavePath);
                    fileName = FullFileWithext;
                }
            }
            return base.Json(new { fileName = fileName, ServerSavePath = ServerSavePath }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VisitorRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            RequestorModel VisitorRequest = this.visitorDAL.VisitorDetailsInformation(MasterID, userID);
            VisitorRequest.Status = Status;
            return this.PartialView(ViewName, VisitorRequest);
        }

        public ActionResult VisitorRequestForm()
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.View();
            }
            return base.RedirectToAction("Index", "Account");
        }
    }
}