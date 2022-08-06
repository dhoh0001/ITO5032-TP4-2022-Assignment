using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITO5032_Assignment.Models;

namespace ITO5032_Assignment.Controllers
{
    public class BookablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookables
        public ActionResult Index()
        {
            return View(db.Bookables.ToList());
        }

        // GET: Bookables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookable bookable = db.Bookables.Find(id);
            if (bookable == null)
            {
                return HttpNotFound();
            }
            return View(bookable);
        }

        // GET: Bookables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,available_day,available_start_time,available_end_time,max_occupancy,booking_type,Location_id")] Bookable bookable)
        {
            if (ModelState.IsValid)
            {
                db.Bookables.Add(bookable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookable);
        }

        // GET: Bookables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookable bookable = db.Bookables.Find(id);
            if (bookable == null)
            {
                return HttpNotFound();
            }
            return View(bookable);
        }

        // POST: Bookables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,available_day,available_start_time,available_end_time,max_occupancy,booking_type,Location_id")] Bookable bookable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookable);
        }

        // GET: Bookables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookable bookable = db.Bookables.Find(id);
            if (bookable == null)
            {
                return HttpNotFound();
            }
            return View(bookable);
        }

        // POST: Bookables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookable bookable = db.Bookables.Find(id);
            db.Bookables.Remove(bookable);
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
