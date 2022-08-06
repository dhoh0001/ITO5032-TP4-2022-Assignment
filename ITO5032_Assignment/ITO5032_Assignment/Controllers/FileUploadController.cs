using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITO5032_Assignment.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Index");
            }
        }
    }
}