using SQIndustryThree.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQIndustryThree.Models;

namespace SQIndustryThree.Controllers
{
    public class VisitorController1 : Controller
    {
        VisitorDAL visitorDAL = new VisitorDAL();
        // GET: Visitor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisitorApproval()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        public ActionResult VisitorRequestForm()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult FrontDeskView()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }



        [HttpPost]
        public ActionResult GetAllVisitorInformation(int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userId = Convert.ToInt32(Session["SQuserId"]);
            List<VisitorRequestModel> visitorInformation = new List<VisitorRequestModel>();
            visitorInformation = visitorDAL.GetAllVisitorInformation(Status, userId,1);
            return PartialView("_allRequestPartialView", visitorInformation);
        }
        public ActionResult GetFrontDeskVisitor(int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userId = Convert.ToInt32(Session["SQuserId"]);
            List<VisitorRequestModel> visitorInformation = new List<VisitorRequestModel>();
            visitorInformation = visitorDAL.GetAllVisitorInformation(Status, userId,2);
            return PartialView("_allFrontDeskPartialView", visitorInformation);
        }

        [HttpPost]
        public ActionResult ChangeDivView(int status)
        {
            string viewName = "";
            switch (status)
            {
                case 0:
                    viewName = "AdminApproverView";
                    break;
                case 1:
                    viewName = "_addPartialOperation";
                    break;
            }
            return PartialView(viewName);

        }

        [HttpPost]
        public ActionResult IndividualRequestShow(int PrimaryKey)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return PartialView("_modalVisitorRequest", visitorDAL.IndividualRequestShow(PrimaryKey,userID));
        }

        [HttpPost]
        public ActionResult UpdateOrReject(int PrimaryKey,int Status)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(visitorDAL.UpdateOrReject(PrimaryKey, userID,Status),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSubMenuBYUserPermission()
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(visitorDAL.SUbMenuByPermission(userID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveVisitorForApprove(RequestorModel visitor)
        {
            if (Session["SQuserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["SQuserId"].ToString());
            return Json(visitorDAL.SaveVistorRequest(visitor,userID), JsonRequestBehavior.AllowGet);
        }

    }
}