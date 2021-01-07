using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQIndustryThree.DAL;
using SQIndustryThree.Models.BillApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.Controllers
{
    public class BillApprovalController : Controller
    {
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

        [HttpPost]
        public ActionResult GetAllBillRequest(int status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["SQuserId"]);
            return PartialView("_RequestListSoFar", billDal.GetBillRequestList(status,userid));
        }
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
    }
}