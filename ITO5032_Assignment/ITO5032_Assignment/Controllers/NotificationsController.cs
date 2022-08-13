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
    public class NotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notifications
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();

            if (Int32.Parse(user[0].role_id) == Roles.ADMIN.Id)
            {
                List<Notification> list = db.Notifications.ToList();
                List<AppUser> users = db.AppUsers.ToList();
                foreach (Notification not in list)
                {
                    foreach (AppUser u in users)
                    {
                        if (u.id == not.User_id)
                            not.User = u;
                    }
                    not.User = users.Find(item => item.id == not.User_id);
                }
                ViewData["isAdmin"] = "ADMIN";
                return View(list);
            }
            else
            {
                int i = user[0].id;
                List<Notification> list = db.Notifications.Where(n => n.User_id == i).ToList();
                List<AppUser> users = db.AppUsers.ToList();
                foreach (Notification not in list)
                {
                    foreach (AppUser u in users)
                    {
                        if (u.id == not.User_id)
                            not.User = u;
                    }
                    not.User = users.Find(item => item.id == not.User_id);
                }
                return View(list);
            }
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,message,notification_datetime,User_id")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,message,notification_datetime,User_id")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
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
        public ActionResult getNotificationCount()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();

            if (db.Notifications.ToList().Count > 0 && Int32.Parse(user[0].role_id) != Roles.ADMIN.Id) {
                int i = user[0].id;
                List<Notification> list = db.Notifications.Where(n => n.User_id == i).ToList();
                ViewData["numNotifications"] = list.Count;
                return PartialView("_BadgePartial");
            } 
            else
            {
                return new EmptyResult();
            }
        }
    }
}
