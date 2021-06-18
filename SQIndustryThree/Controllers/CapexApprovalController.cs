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
        AdminDAL adminDAL = new AdminDAL();
        HomeDAL homeDAL = new HomeDAL();

        // GET: CapexApproval
        public ActionResult CreateCapex()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            int permission = capexApproval.ModulePermission(1,userid);
            if (permission != 1)
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
            int userid = Convert.ToInt32(Session["SQuserId"]);
            int permission = capexApproval.ModulePermission(2, userid);
            if (permission != 1)
            {
                return RedirectToAction("PendingCapex", "CapexApproval");
            }
            return View();
        }
        public ActionResult LoadBusinessUnit()
        {
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(capexApproval.GetBusinessUnits(userid), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadLocation()
        {
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(capexApproval.GetLocation(userid), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadCurrency()
        {
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return Json(capexApproval.LoadCurrency(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCapexCatagory()
        {
            int userid = Convert.ToInt32(Session["SQuserId"]);
            int permission = capexApproval.ModulePermission(1, userid);
            if (permission != 1)
            {
                return Json(capexApproval.GetCapexCatagory(0), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(capexApproval.GetCapexCatagory(userid), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult LoadCapexApprover(int CatagoryId)
        {
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(capexApproval.GetApproverList(CatagoryId, userID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadBFCORCFOByBusinessUnit(int BusinessUnit,int CatagoryId)
        {
            return Json(capexApproval.GetBFoORCFo(BusinessUnit, CatagoryId), JsonRequestBehavior.AllowGet);
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
        public ActionResult RevisedCapexInformation(CapexInformationMaster capexInformationMaster)
        {
            ResultResponse result = new ResultResponse();
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            result = capexApproval.RevisedCapexInformation(capexInformationMaster, userID);
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
            List<ModuleModel> moduleList = new List<ModuleModel>();
            moduleList =homeDAL.GetModuleByUser(userID,1);
            result = capexApproval.ReviewCapexComment(comments, userID);
            result.msg = Url.Action(moduleList[0].ModuleValue, moduleList[0].ModuleController);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowPertialviewModal(CapexInformationMaster capexInformationMaster)
        {
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            capexInformationMaster.CapexFileUpload = new List<CapexFileUploadDetails>();
            capexInformationMaster.CapexFileUpload = capexApproval.GetUploadedFilesByID(0,userID);
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
            var capexInfo = capexApproval.GetSavedCapex(userID, primarykey);
            //  int permission = capexApproval.ModulePermission(4, userID);
            return PartialView("_ShowUploadedFiles", capexInfo);
            //if (permission != 1)
            //{
               
            //}
            //else
            //{
            //    return PartialView("_capexIdShowAdmin", capexApproval.GetSavedCapex(userID, primarykey));
            //}
            
        }
        [HttpPost]
        public ActionResult AssetSelect(int CatagoryId)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(capexApproval.GetAssetCatagoryById(userID, CatagoryId),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editcapexmodal(int primarykey)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_updateCapexpertialView", capexApproval.GetSavedCapex(userID, primarykey));
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

        [HttpPost]
        public ActionResult DeleteFiles()
        {
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            List<CapexFileUploadDetails> capexFileUploadDetails = capexApproval.GetUploadedFilesByID(0,userID);
            bool rest = false;
            if (capexFileUploadDetails.Count>0 || capexFileUploadDetails ==null)
            {
                foreach (CapexFileUploadDetails f in capexFileUploadDetails)
                {
                    try
                    {
                        System.IO.File.Delete(f.CapexFilePath);
                    }
                    catch (IOException ioExp)
                    {
                        Console.WriteLine(ioExp.Message);
                    }
                }

                rest = capexApproval.DeleteFileFromDatabase(0, userID);
            }
            else
            {
                rest = false;
            }
            return Json(rest,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]  //Now we are getting array of files check sign []
        public ActionResult UploadFiles()
        {
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());

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
                        var currentmilse = DateTime.Now.Ticks;
                        var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var InputFileExtention = Path.GetExtension(file.FileName);
                        var FullFileWithext = InputFileName + currentmilse+ InputFileExtention;
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + FullFileWithext);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);

                        capexFileUploadDetails.CapexFileName = file.FileName.ToString();
                        capexFileUploadDetails.CapexFilePath = ServerSavePath;
                        capexFileUploadDetails.CapexInfoId = 0;
                        capexFileUploadDetails.userId = userID;
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
        //[HttpPost]
        public ActionResult AdminPanel()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            int permission = capexApproval.ModulePermission(4, userID);
            if (permission != 1)
            {
                return RedirectToAction("PendingCapex", "CapexApproval");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AllApprovedCapex(int BusinessUnitID, int CatagoryID, string SelectDate, string EndDate)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            List<CapexInformationMaster> capexInformationList = adminDAL.GetALLCapexInfo(BusinessUnitID, CatagoryID, SelectDate, EndDate);
            return PartialView("_pertialCapexForAdmin", capexInformationList);
        }
    }
    
}