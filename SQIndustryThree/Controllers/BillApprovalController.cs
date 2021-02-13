using DocSoOperation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQIndustryThree.DAL;
using SQIndustryThree.Models;
using SQIndustryThree.Models.BillApproval;
using SQIndustryThree.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class BillApprovalController : Controller
    {
        BasicUtilities _BasicUtilities = new BasicUtilities();
        // GET: BillApproval
        BillApprovalDAL billDal = new BillApprovalDAL();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BillApprovalRequest()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult MyRequestSoFar()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult POUploadInterface()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UploadExcelData(List<BillAprrovalPoDetails> billAprrovalPoDetails)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            //List<BillAprrovalPoDetails> billAprrovalPoDetails= JsonConvert.DeserializeObject<List<BillAprrovalPoDetails>>(datalist.ToString());
            return Json(billDal.BillApprovalDatabase(billAprrovalPoDetails, userid),JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetBillApprovalPolist(int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            //List<BillAprrovalPoDetails> billAprrovalPoDetails= JsonConvert.DeserializeObject<List<BillAprrovalPoDetails>>(datalist.ToString());
            return Json(billDal.GetAllBillAPpprovalPo(Status), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetIndividualPODetails(int PoMasterKey,int BUnit,int CatKey)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return PartialView("_podetailsview", billDal.IndividualPOetailS(PoMasterKey,BUnit,CatKey));
        }
        [HttpPost]
        public ActionResult PreviewApproval(BillApprovalPOMasterTable billApprovalPOMasterTable)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return PartialView("_submitForApprovalModal", billApprovalPOMasterTable);
        }

        //[HttpPost]
        //public ActionResult GetAllBillRequest(int status)
        //{
        //    if (Session["SQuserId"] == null)
        //    {
        //        return RedirectToAction("Index", "Account");
        //    }
        //    int userid = Convert.ToInt32(Session["SQuserId"]);
        //    return PartialView("_RequestListSoFar", billDal.GetBillRequestList(status,userid));
        //}
        [HttpPost]
        public ActionResult SubmitForBillApprovalRequest(BillApprovalPOMasterTable billApprovalPOMasterTable)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(billDal.SubmitForBillApprovalRequest(billApprovalPOMasterTable.Polist,userid, billApprovalPOMasterTable.BusinessUnitId, billApprovalPOMasterTable.CategoryId));
        }


        #region Bill Entry


        public ActionResult BillEntry()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }



            return View();
        }

        public ActionResult ModalShowBeforeSubmit(BillRequestMaster billRequestMaster)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_MyBillPreview", billRequestMaster);

        }

        public ActionResult SaveBillRequest(BillRequestMaster billRequestMaster)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            Session[AMSSession.BillInfoList] = billRequestMaster.BillInfoList;

            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = billDal.SaveBillRequest(billRequestMaster, userID);

            if (Session[AMSSession.BillInfoList] != null)
            {
                foreach (var item in (List<InvoiceInformation>)Session[AMSSession.BillInfoList])
                {
                    var POUpdate = billDal.UpdatePODetails(Convert.ToDouble(item.InvoiceQty), Convert.ToDouble(item.InvoiceBalance), item.PODetailsKey);
                }
            }

            

            Session[AMSSession.InvoiceKey] = result.pk;
            Session[AMSSession.InvoiceNo] = billRequestMaster.InvoiceNo;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvoiceList()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public ActionResult GetBillPartial(string viewName)
        {
            if (base.Session["SQuserId"] != null)
            {
                return base.PartialView(viewName);
            }
            return base.RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult GetAllBillRequest(int Status, string ViewName, int Progrss)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }

            List<Dictionary<string, object>> _dtResult = new List<Dictionary<string, object>>();
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            var _list = billDal.GetAllBillRequest(userID, Status, Progrss);

            _dtResult = _BasicUtilities.GetTableRows(_list);
            //

            ViewBag.BillList = _dtResult;

            return this.PartialView(ViewName);
        }

        [HttpPost]
        public ActionResult BillRequestDetails(int MasterID, string ViewName, int Status)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            BillRequestMaster billRequest = billDal.GetBillInforamtion(MasterID, userID);
            billRequest.Status = Status;
            return this.PartialView(ViewName, billRequest);
        }

        public ActionResult BillApproval()
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }

            return base.View();
        }

        public ActionResult SupplierList()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            try
            {
               
                DataTable dt = billDal.SupplierList();
                //   List<Dictionary<string, object>>
                List<Dictionary<string, object>> _List = _BasicUtilities.GetTableRows(dt);

                return Json(_List);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult ApproverList(int invoiceType)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            try
            {

                DataTable dt = billDal.InvoiceTypeWiseApproverList(invoiceType);
                //   List<Dictionary<string, object>>
                List<Dictionary<string, object>> _List = _BasicUtilities.GetTableRows(dt);

                return Json(_List);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        public ActionResult POList(int supplierId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            try
            {
                DataTable dt = billDal.POList(supplierId);
                //   List<Dictionary<string, object>>
                List<Dictionary<string, object>> _List = _BasicUtilities.GetTableRows(dt);

                return Json(_List);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult AddQuality()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            try
            {
                if (Session[AMSSession.InvoiceKey] != null)
                {
                    ViewBag.InvoiceNo = Session[AMSSession.InvoiceNo].ToString();
                    ViewBag.InvoiceKey = Session[AMSSession.InvoiceKey].ToString();
                }
                

                return View();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult EntryQuality(int invoiceKey, string invoiceNo, string item, string result, string comment, string fileName, string filePath)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            string itemTrim = item.TrimEnd(',');
            string resultTrim = result.TrimEnd(',');
            string commentTrim = comment.TrimEnd(',');
            string fileNameTrim = fileName.TrimEnd(',');
            string filePathTrim = filePath.TrimEnd(',');
            List<string> itemList = itemTrim.Split(',').ToList();
            List<string> resultList = resultTrim.Split(',').ToList();
            List<string> commentList = commentTrim.Split(',').ToList();
            List<string> fileNameList = fileNameTrim.Split(',').ToList();
            List<string> filePathList = filePathTrim.Split(',').ToList();

            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            var saveQuality = false;
            if (invoiceKey != 0 && !string.IsNullOrEmpty(invoiceNo) && itemList != null)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(itemList[i]))
                    {
                        var f = string.Empty;
                        var p = string.Empty;

                        if (fileNameList[i] == "undefined")
                        {
                            f = "";
                        }
                        else
                        {
                            f = fileNameList[i];
                        }

                        if (filePathList[i] == "undefined")
                        {
                            p = "";
                        }
                        else
                        {
                            p = filePathList[i];
                        }

                        saveQuality = billDal.SaveQuality(invoiceKey, invoiceNo, itemList[i], resultList[i], commentList[i], f, p, userID);
                    }                   
                }                
            }

            return Json(saveQuality);
        }


        public ActionResult BillFileUpload()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            List<BillFileUploadDetails> fileuploadList = new List<BillFileUploadDetails>();
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
                        var ServerSavePath = Path.Combine(Server.MapPath("~/BillFileUpload/") + FullFileWithext);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        BillFileUploadDetails fileUploadModel = new BillFileUploadDetails();
                        fileUploadModel.BillFileName = file.FileName.ToString();
                        fileUploadModel.BillFilePath = ServerSavePath;
                        fileUploadModel.ServerFileName = FullFileWithext;
                        fileuploadList.Add(fileUploadModel);
                    }
                }
            }
            return Json(fileuploadList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BillDeleteFiles(string FilePath)
        {
            bool result = false;
            try
            {
                System.IO.File.Delete(Server.MapPath("~/BillFileUpload/") + FilePath);
                result = true;
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFile(string filepath, string filename)
        {
            //string name = Path.GetFileName(filename);
            //var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + name);
            //return File(ServerSavePath, "image/png");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));

            //string fname = Path.GetFileName(filename);
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + fname);
            //string aaa = Server.MapPath("~/Uploads/" + fname);
            //Response.TransmitFile(Server.MapPath("~/Uploads/" + fname));
            //Response.End();
        }

        public FileResult QualityDownloadFile(string filepath, string filename)
        {
            //string name = Path.GetFileName(filename);
            //var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + name);
            //return File(ServerSavePath, "image/png");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));

            //string fname = Path.GetFileName(filename);
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + fname);
            //string aaa = Server.MapPath("~/Uploads/" + fname);
            //Response.TransmitFile(Server.MapPath("~/Uploads/" + fname));
            //Response.End();
        }

        public ActionResult QualityFileUpload()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            List<QualityFileUploadDetails> fileuploadList = new List<QualityFileUploadDetails>();
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
                        var ServerSavePath = Path.Combine(Server.MapPath("~/QualityFileUpload/") + FullFileWithext);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        QualityFileUploadDetails fileUploadModel = new QualityFileUploadDetails();
                        fileUploadModel.QualityFileName = file.FileName.ToString();
                        fileUploadModel.QualityFilePath = ServerSavePath;
                        fileUploadModel.ServerFileName = FullFileWithext;
                        fileuploadList.Add(fileUploadModel);
                    }
                }
            }
            return Json(fileuploadList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult QualityDeleteFiles(string FilePath)
        {
            bool result = false;
            try
            {
                System.IO.File.Delete(Server.MapPath("~/QualityFileUpload/") + FilePath);
                result = true;
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BillApproveOrReject(string CommentText, int Progress, int RequestorId)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.billDal.BillApproveOrReject(Progress, CommentText, userID, RequestorId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult QualityExist(int InvoiceKey)
        {
            //int result = 0;

            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }

           int result = billDal.CheckQuality(InvoiceKey);

            return Json(result);
        }

        public ActionResult QualityPreview(int InvoiceKey, string ViewName)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            var billQuality = billDal.GetQualityInforamtion(InvoiceKey);
            //billRequest.Status = Status;
            return this.PartialView(ViewName, billQuality);
        }


        [HttpPost]
        public ActionResult SendBillComments(int MasterID, int ReviewTo, string ReviewMessage)
        {
            if (base.Session["SQuserId"] == null)
            {
                return base.RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(base.Session["SQuserId"].ToString());
            return base.Json(this.billDal.BillCommentSent(MasterID, ReviewTo, ReviewMessage, userID), JsonRequestBehavior.AllowGet);
        }

        #endregion



    }
}