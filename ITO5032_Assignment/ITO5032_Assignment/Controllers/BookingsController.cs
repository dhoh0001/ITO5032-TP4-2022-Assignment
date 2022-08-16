using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITO5032_Assignment.Enums;
using ITO5032_Assignment.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ITO5032_Assignment.Controllers
{
    [Authorize]
    [RequireHttps]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StartTimeSortParm = String.IsNullOrEmpty(sortOrder) ? "start_time_desc" : "";
            ViewBag.UserSortParm = sortOrder == "user" ? "user_desc" : "user";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var id = User.Identity.GetUserId(); 
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();
            var list = from usr in db.Bookings select usr;


            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.Bookable.name.Contains(searchString)
                                       || s.Bookable.description.Contains(searchString)
                                       || s.User.first_name.Contains(searchString)
                                       || s.User.last_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "start_time_desc":
                    list = list.OrderByDescending(u => u.start_datetime);
                    break;
                case "user":
                    list = list.OrderBy(u => u.User_id);
                    break;
                case "user_desc":
                    list = list.OrderByDescending(u => u.User_id);
                    break;
                default:
                    list = list.OrderBy(u => u.start_datetime);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (list.ToList().Count > 0 )
            {
                foreach (Booking b in list)
                {
                    b.User = db.AppUsers.Where(item => item.id == b.User_id).ToList()[0];
                    b.Bookable = db.Bookables.Where(bkbl => bkbl.id == b.Bookable_id).ToList()[0];
                }
            }
            if (user.Count > 0)
            {
                if (user[0].role_id == Roles.ADMIN.Id)
                {
                    ViewData["isAdmin"] = "ADMIN";
                }
                else
                {
                    int i = user[0].id;
                    list = list.Where(n => n.User_id == i);
                }
            }
            return View(list.ToPagedList(pageNumber, pageSize));

        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            booking.User = db.AppUsers.Where(item => item.id == booking.User_id).ToList()[0];
            booking.Bookable = db.Bookables.Where(bkbl => bkbl.id == booking.Bookable_id).ToList()[0];
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,start_datetime,end_datetime,User_id,Bookable_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                string message = "A booking for has been placed from" + booking.start_datetime + " to " + booking.end_datetime;

                db.Bookings.Add(booking);
                Notification not = new Notification();
                not.message = message;
                not.User_id = booking.User_id;
                not.notification_datetime = DateTime.Now;
                db.Notifications.Add(not);
                db.SaveChanges();
                sendEmail(booking, message);
                
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        public async void sendEmail(Booking booking, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dogsandtots@gmail.com", "Dogs And Tots");
            var subject = "Dogs and Tots Booking!";
            List<EmailAddress> tos = new List<EmailAddress>();
            var to1 = db.AppUsers.Where(u => u.id == booking.User_id).ToList();
            var bookable = db.Bookables.Where(b => b.id == booking.Bookable_id).ToList()[0];
            var service_provider = db.AppUsers.Where(sp => sp.id == bookable.service_provider_id).ToList()[0];

            if (to1.Count > 0)
            {
                tos.Add(new EmailAddress(to1[0].email, to1[0].first_name + " " + to1[0].last_name));
            }
            var to2 = db.AppUsers.Where(u => u.id == booking.Bookable.service_provider_id).ToList();
            if (to2.Count > 0)
            {
                tos.Add(new EmailAddress(service_provider.email, service_provider.first_name + " " + service_provider.last_name));
            }
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
            byte[] bytes = null;
            try
            {
                bytes = System.IO.File.ReadAllBytes("C:/Users/danie/source/repos/ITO5032_Assignment/ITO5032_Assignment/ITO5032_Assignment/Images/dogsandtots.png");
            }
            catch (Exception e)
            {
                var ex = e;
            }
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("logo.png", file);

            byte[] bytes2 = null;
            var location = db.Locations.Where(l => l.id == bookable.Location_id).ToList()[0];
            var file2 = db.Files.Where(f => f.id == location.file_id).ToList()[0];
            try
            {
                bytes2 = System.IO.File.ReadAllBytes(file2.file_location);
            }
            catch (Exception e)
            {
                var ex = e;
            }
            var file2attach = Convert.ToBase64String(bytes2);
            msg.AddAttachment("logo.png", file2attach);

            var response = await client.SendEmailAsync(msg);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,start_datetime,end_datetime,User_id,Bookable_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            booking.User = db.AppUsers.Where(item => item.id == booking.User_id).ToList()[0];
            booking.Bookable = db.Bookables.Where(bkbl => bkbl.id == booking.Bookable_id).ToList()[0];
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult UserBooking()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();
            var bookings = db.Bookings.Where(b => b.User_id == user[0].id);
            bookings = bookings.OrderBy(b => b.start_datetime);
            return View(bookings.ToPagedList(1, bookings.ToList().Count()));
        }
    }
}
