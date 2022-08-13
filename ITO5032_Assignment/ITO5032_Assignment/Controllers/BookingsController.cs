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

namespace ITO5032_Assignment.Controllers
{
    [RequireHttps]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();

            if (Int32.Parse(user[0].role_id) == Roles.ADMIN.Id)
            {
                List<Booking> list = db.Bookings.ToList();
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
                ViewData["isAdmin"] = "ADMIN";
                return View(list);
            }
            else
            {
                int i = user[0].id;
                List<Booking> list = db.Bookings.Where(n => n.User_id == i).ToList();
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
                return View(list);
            }
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
