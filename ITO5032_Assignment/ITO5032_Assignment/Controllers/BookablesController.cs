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
    public class BookablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookables
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StartTimeSortParm = sortOrder == "start_time" ? "start_time_desc" : "start_time";
            ViewBag.LocationSortParm = sortOrder == "location_asc" ? "location_desc" : "location_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var list = from bk in db.Bookables select bk;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.name.Contains(searchString)
                                       || s.description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(u => u.name);
                    break;
                case "start_time":
                    list = list.OrderBy(u => u.available_start_time);
                    break;
                case "start_time_desc":
                    list = list.OrderByDescending(u => u.available_start_time);
                    break;
                case "location_asc":
                    list = list.OrderBy(u => u.Location);
                    break;
                case "location_desc":
                    list = list.OrderByDescending(u => u.Location);
                    break;
                default:
                    list = list.OrderBy(u => u.name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
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

        public ActionResult RoomBookable()
        {
            return View();
        }
        public ActionResult ServiceProviderBookable()
        {
            return View();
        }
    }
}
