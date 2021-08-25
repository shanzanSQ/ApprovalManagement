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
    public class CameraController : Controller
    {
        CameraDAL cameraDal = new CameraDAL();

        [HttpGet]
        // GET: Camera
        public ActionResult Capture(int RequestId)
        {
            Session["RequestId"] = RequestId;
            return View();
        }

        [HttpPost]
        public ActionResult Capture(string name)
        {
            try
            {

                int RequestId = Convert.ToInt32(base.Session["RequestId"]);

                var files = Request.Files;
                if (files != null)
                {
                    foreach (string str in files)
                    {
                        HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;

                        if (file != null)
                        {
                            // Getting Filename
                            var fileName = file.FileName;
                            var currentmilse = DateTime.Now.Ticks;
                            var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var InputFileExtention = Path.GetExtension(file.FileName);
                            var FullFileWithext = InputFileName + currentmilse + InputFileExtention;
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Images/Visitors/") + FullFileWithext);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);

                            Session["Imagename"] = FullFileWithext;
                            Session["ServerPath"] = ServerSavePath;

                            if (!string.IsNullOrEmpty(ServerSavePath))
                            {
                                // Storing Image in Folder
                                // StoreInFolder(file, ServerSavePath);

                                var imageBytes = System.IO.File.ReadAllBytes(ServerSavePath);
                                if (imageBytes != null)
                                {
                                    // Storing Image in Folder
                                    StoreInDatabase(RequestId,imageBytes, FullFileWithext, ServerSavePath);
                                }
                            }



                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //private void StoreInFolder(HttpPostedFileBase file, string fileName)
        //{
        //    using (FileStream fs = System.IO.File.Create(fileName))
        //    {
        //        file.CopyTo(fs);
        //        fs.Flush();
        //    }
        //}

        /// <summary>
        /// Saving captured image into database.
        /// </summary>
        /// <param name="imageBytes"></param>
        private void StoreInDatabase(int RequestId, byte[] imageBytes, string ImageName, string ImagePath)
        {
            try
            {
                if (imageBytes != null)
                {
                    string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                    ImageStore imageStore = new ImageStore()
                    {
                        RowId = RequestId,
                        CreateDate = DateTime.Now,
                        ImageBase64String = imageUrl,
                        ImageId = 0,
                        ImageName = ImageName,
                        ImagePath = ImagePath

                    };
                    cameraDal.ImageInsert(imageStore);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}