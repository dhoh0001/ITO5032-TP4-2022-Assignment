using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITO5032_Assignment.Models;
using File = ITO5032_Assignment.Models.File;

namespace ITO5032_Assignment.Controllers
{
    [Authorize]
    [RequireHttps]
    public class FileUploadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
                    File newFile = new File();
                    newFile.file_name = _FileName;
                    newFile.file_location = _path;
                    db.Files.Add(newFile);
                    db.SaveChanges();
                    ViewBag.Message = "File Uploaded Successfully!!";
                }
                else
                {
                    ViewBag.Message = "File upload failed!!";
                }
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