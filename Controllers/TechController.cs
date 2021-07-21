using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChakisTicketTracking1.DAL;
using ChakisTicketTracking1.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace ChakisTicketTracking1.Controllers
{
    public class TechController : Controller
    {
        private HelpDeskContext db = new HelpDeskContext();

        // GET: Tech
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var techs = from t in db.Techs
                        select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                techs = techs.Where(t => t.LastName.Contains(searchString)
                                         || t.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    techs = techs.OrderByDescending(t => t.LastName);
                    break;
                default:
                    techs = techs.OrderBy(t => t.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(techs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Tech/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tech tech = db.Techs.Find(id);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // GET: Tech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tech/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName")] Tech tech)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Techs.Add(tech);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to create tech. Try again, and if the problem persists see your system administrator.");
            }

            return View(tech);
        }

        // GET: Tech/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tech tech = db.Techs.Find(id);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: Tech/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? techID)
        {
            if (techID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var techToUpdate = db.Techs.Find(techID);

            if (TryUpdateModel(techToUpdate, "", new string[] { "LastName", "FirstMidName" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("",
                        "Unable to save edit. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(techToUpdate);
        }


        // GET: Tech/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Tech tech = db.Techs.Find(id);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: Tech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Tech tech = db.Techs.Find(id);
                db.Techs.Remove(tech);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

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
