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

namespace ITO5032_Assignment.Controllers
{
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
                    list = list.OrderBy(u => u.User.first_name + u.User.last_name);
                    break;
                case "user_desc":
                    list = list.OrderByDescending(u => u.User.first_name + u.User.last_name);
                    break;
                default:
                    list = list.OrderBy(u => u.start_datetime);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            List<AppUser> users = db.AppUsers.ToList();
            foreach (Booking b in list)
            {
                foreach (AppUser u in users)
                {
                    if (u.id == b.User_id)
                        b.User = u;
                }
                b.User = users.Find(item => item.id == b.User_id);
                b.Bookable = db.Bookables.Where(bkbl => bkbl.id == b.Bookable_id).ToList()[0];
            }
            if (user[0].role_id == Roles.ADMIN.Id)
            {
                ViewData["isAdmin"] = "ADMIN";
            }
            else
            {
                int i = user[0].id;
                list = list.Where(n => n.User_id == i);
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
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
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
            return View();
        }
    }
}
