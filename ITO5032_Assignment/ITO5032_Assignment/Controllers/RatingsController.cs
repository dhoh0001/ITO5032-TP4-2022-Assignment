using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITO5032_Assignment.Models;
using PagedList;

namespace ITO5032_Assignment.Controllers
{
    [Authorize]
    [RequireHttps]
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ratings
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = from usr in db.Ratings select usr;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.User.first_name.Contains(searchString)
                                       || s.User.last_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "user_desc":
                    list = list.OrderByDescending(u => u.User_id);
                    break;
                default:
                    list = list.OrderBy(u => u.User_id);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (list.ToList().Count > 0)
            {
                foreach (Rating r in list)
                {
                    r.User = db.AppUsers.Where(item => item.id == r.User_id).ToList()[0];
                    r.Service_Provider = db.AppUsers.Where(item => item.id == r.service_provider_id).ToList()[0];
                }
            }

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            rating.User = db.AppUsers.Where(item => item.id == rating.User_id).ToList()[0];
            rating.Service_Provider = db.AppUsers.Where(item => item.id == rating.service_provider_id).ToList()[0];
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,score,comment,service_provider_id,User_id")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,score,comment,service_provider_id,User_id")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            rating.User = db.AppUsers.Where(item => item.id == rating.User_id).ToList()[0];
            rating.Service_Provider = db.AppUsers.Where(item => item.id == rating.service_provider_id).ToList()[0];
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
