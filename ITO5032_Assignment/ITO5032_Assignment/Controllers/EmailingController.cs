using ITO5032_Assignment.Models;
using System;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ITO5032_Assignment.Controllers
{
    [Authorize]
    [RequireHttps]
    public class EmailingController : Controller
    {
        // GET: Emailing
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailingModel mailObject)
        {
            sendEmail(mailObject);

            if (ModelState.IsValid)
            {
                ViewBag.Message = "sucesss";
                return View("Index", mailObject);
            }
            else
            {
                ViewBag.Message = "failure";
                return View();
            }
        }

        public async Task sendEmail(EmailingModel mailObject)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(mailObject.From, "Example User");
            var subject = mailObject.Subject;
            var to = new EmailAddress(mailObject.To, "Example User");
            List<EmailAddress> tos = new List<EmailAddress>();
            tos.Add(to);
            tos.Add(from);
            var plainTextContent = "and easy to do anywhere, even with C# 1" + mailObject.Body;
            var htmlContent = "<strong>and easy to do anywhere, even with C# 2</strong>" + mailObject.Body;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
            byte[] bytes = null;
            try
            {
                bytes = System.IO.File.ReadAllBytes("C:/Users/danie/source/repos/ITO5032_Assignment/ITO5032_Assignment/ITO5032_Assignment/Images/dogsandtots.png");
            } catch (Exception e){
                var ex = e;
            }
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("logo.png", file);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
