using ITO5032_Assignment.Models;
using System;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ITO5032_Assignment.Controllers
{
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
                return View("Index", mailObject);
            }
            else
            {
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
            var plainTextContent = "and easy to do anywhere, even with C# 1" + mailObject.Body;
            var htmlContent = "<strong>and easy to do anywhere, even with C# 2</strong>" + mailObject.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
