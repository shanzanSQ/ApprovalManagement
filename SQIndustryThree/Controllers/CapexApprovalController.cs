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
    public class CapexApprovalController : Controller
    {

        CapexApprovalDAL capexApproval = new CapexApprovalDAL();

        // GET: CapexApproval
        public ActionResult CreateCapex()
        {

            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int designation = Convert.ToInt32(Session["SQdesignation"]);
            if (designation == 1 || designation == 2 || designation==3 || designation==4)
            {
                return RedirectToAction("PendingCapex", "CapexApproval");
            }
            return View();
        }

        public ActionResult PendingCapex()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult CapexInformationView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult LoadBusinessUnit()
        {
            return Json(capexApproval.GetBusinessUnits(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadCapexCatagory()
        {
            return Json(capexApproval.GetCapexCatagory(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCapexApprover(int CatagoryId)
        {
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(capexApproval.GetApproverList(CatagoryId, userID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadBFCORCFOByBusinessUnit(int BusinessUnit,int Designation)
        {
            return Json(capexApproval.GetBFoORCFo(BusinessUnit, Designation), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCapexInformation(CapexInformationMaster capexInformationMaster)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = capexApproval.SaveCapexInformationDatabase(capexInformationMaster,userID);
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult UpdatedCapexInfo(CapexInformationMaster capexInformationMaster)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = capexApproval.UpdateOrApproveCapex(capexInformationMaster, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReviewCapexInfo(CommentsTable comments)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = capexApproval.ReviewCapexComment(comments, userID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowPertialviewModal(CapexInformationMaster capexInformationMaster)
        {
            capexInformationMaster.CapexFileUpload = new List<CapexFileUploadDetails>();
            capexInformationMaster.CapexFileUpload = capexApproval.GetUploadedFilesByID(0);
            return PartialView("_modalShowCapexinfo", capexInformationMaster);
        }

        [HttpPost]
        public ActionResult showCapexInformation(int status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CapexInformationMaster> capexInformationList = capexApproval.GetCapexInfo(userID,status);
            return PartialView("_pendingPertialView", capexInformationList);
        }

        [HttpPost]
        public ActionResult IndividualCapexShow(int primarykey)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_ShowUploadedFiles", capexApproval.GetSavedCapex(userID, primarykey));
        }

        public FileResult DownloadFile(string filename)
        {
            string name = Path.GetFileName(filename);
            var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + name);
            return File(ServerSavePath, "image/png");
        }

        [HttpPost]
        public ActionResult DeleteFiles()
        {
            List<CapexFileUploadDetails> capexFileUploadDetails = capexApproval.GetUploadedFilesByID(0);
            foreach (var f in capexFileUploadDetails)
            {
                try
                {
                    System.IO.File.Delete(f.CapexFilePath);
                }catch (IOException ioExp){
                    Console.WriteLine(ioExp.Message);
                }
            }
            return View();
        }

        [HttpPost]  //Now we are getting array of files check sign []
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;

                //iterating through multiple file collection   
                foreach (string str in files)
                {
                    CapexFileUploadDetails capexFileUploadDetails = new CapexFileUploadDetails();
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);

                        capexFileUploadDetails.CapexFileName = InputFileName;
                        capexFileUploadDetails.CapexFilePath = ServerSavePath;
                        capexFileUploadDetails.CapexInfoId = 0;
                        bool res = capexApproval.FileUploadToDatabase(capexFileUploadDetails);
                    }

                }
                return Json("File Uploaded Successfully!");
            }
            else
            {
                return Json("No files to upload");
            }
        }
    }
}