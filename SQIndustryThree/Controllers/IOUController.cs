using DocSoOperation.Models;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class IOUController : Controller
    {
        IOUDAL ioudal = new IOUDAL();
        // GET: IOU
        public ActionResult IOUManagementView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult IOUSettlementView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult IouApproverView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult IouCashierView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult GetIOUPartial(string viewName)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return PartialView(viewName);
        }
        public ActionResult GetDepartmentId(int Status,int DepartmentHeadID)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(ioudal.GetDepartmentID(Status,DepartmentHeadID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetApproverList(int BusinessUnit, int LocationId,int DepartmentId,int Ammount)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(ioudal.GETIOUApproverList(BusinessUnit, LocationId,DepartmentId,Ammount,userid), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteFiles(string FilePath)
        {
            bool result = false;
            try
            {
                System.IO.File.Delete(Server.MapPath("~/IOUFileUpload/") + FilePath);
                result = true;
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IOUFileUpload()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                List<CapexFileUploadDetails> fileuploadList = new List<CapexFileUploadDetails>();
                if (Request.Files.Count > 0)
                {
                    var files = Request.Files;
                    foreach (string str in files)
                    {
                        HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var currentmilse = DateTime.Now.Ticks;
                            var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var InputFileExtention = Path.GetExtension(file.FileName);
                            var FullFileWithext = InputFileName + currentmilse + InputFileExtention;
                            var ServerSavePath = Path.Combine(Server.MapPath("~/IOUFileUpload/") + FullFileWithext);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            CapexFileUploadDetails fileUploadModel = new CapexFileUploadDetails();
                            fileUploadModel.CapexFileName = file.FileName.ToString();
                            fileUploadModel.CapexFilePath = ServerSavePath;
                            fileUploadModel.ServerFileName = FullFileWithext;
                            fileuploadList.Add(fileUploadModel);
                        }
                    }
                }
                return Json(fileuploadList, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ModalBeforeIOUSubmit(IOURequestModel IouRequestmodal)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            IouRequestmodal.SettlementDate = "";
            return PartialView("_modalRequestView", IouRequestmodal);
        }

        [HttpPost]
        public ActionResult SaveIOuRequest(IOURequestModel iOURequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = ioudal.SaveIOuRequest(iOURequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateIouRequest(IOURequestModel iOURequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = ioudal.UpdateIouRequest(iOURequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitForSettlement(IOURequestModel iOURequestModel)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = ioudal.SubmitForSettlement(iOURequestModel, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllIOURequest(int Status, string ViewName, int Progrss)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView(ViewName, ioudal.GetAllIouRequest(userID, Status, Progrss));
        }

        [HttpPost]
        public ActionResult IouRequestDetails(int MasterID,string ViewName,int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            IOURequestModel iOURequest = ioudal.IouDetailsInformation(MasterID,userID);
            iOURequest.Status = Status;
            return PartialView(ViewName, iOURequest);
        }
        [HttpPost]
        public ActionResult IouApproveOrReject(string CommentText, int Progress, int IOuRequestId,string SettlementDate)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(ioudal.IOUApproveOrReject(Progress, CommentText, userID, IOuRequestId,SettlementDate), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IouSettlementApprove(string CommentText, int Progress, int IOuRequestId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(ioudal.IouSettlementApprove(Progress, CommentText, userID, IOuRequestId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteIouItemRow(int RowID)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(ioudal.DeleteAmmountFrom(RowID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SendIOUComments(int MasterID, int ReviewTo, string ReviewMessage)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(ioudal.CommentSent(MasterID, ReviewTo, ReviewMessage, userID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDisburseAmount(int IouRequestId, int Amount, string Remarks)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(ioudal.SaveDisburseAmount(IouRequestId, Amount, Remarks, userID), JsonRequestBehavior.AllowGet);
        }
    }
}