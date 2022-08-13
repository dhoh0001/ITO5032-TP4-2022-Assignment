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
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();

            if (Int32.Parse(user[0].role_id) == Roles.ADMIN.Id)
            {
                ViewData["isAdmin"] = "ADMIN";
                return View(db.AppUsers.ToList());
            }
            else
            {
                return View(user);
            }
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,role_id,first_name,last_name,date_of_birth,username,password,salt,address1,address2,email,external_id")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.AppUsers.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,role_id,first_name,last_name,date_of_birth,username,password,salt,address1,address2,email,external_id")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            db.AppUsers.Remove(appUser);
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
    }
}
