using ITO5032_Assignment.Models;
using System;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ITO5032_Assignment.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult test()
        {
            ViewData["i"] = 9;
            return PartialView("_BadgePartial");
        }
    }
}
