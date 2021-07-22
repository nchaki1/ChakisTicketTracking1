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

namespace ChakisTicketTracking1.Views
{
    public class AssignmentController : Controller
    {
        private HelpDeskContext db = new HelpDeskContext();

        // GET: Assignment
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var assignments = db.Assignments.Include(a => a.Tech).Include(a => a.Ticket);

            switch (sortOrder)
            {
                case "name_desc":
                    assignments = assignments.OrderByDescending(a => a.Tech.LastName);
                    break;
                case "Date":
                    assignments = assignments.OrderBy(a => a.Ticket.RequestDate);
                    break;
                case "date_desc":
                    assignments = assignments.OrderByDescending(a => a.Ticket.RequestDate);
                    break;
                default:
                    assignments = assignments.OrderBy(a => a.Tech.LastName);
                    break;
            }
            return View(assignments.ToList());
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            ViewBag.TechID = new SelectList(db.Techs, "TechID", "LastName");
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "TicketDescription");
            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentID,TicketID,TechID,CompletionDate,Notes")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TechID = new SelectList(db.Techs, "TechID", "LastName", assignment.TechID);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "TicketDescription", assignment.TicketID);
            return View(assignment);
        }

        // GET: Assignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TechID = new SelectList(db.Techs, "TechID", "LastName", assignment.TechID);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "TicketDescription", assignment.TicketID);
            return View(assignment);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentID,TicketID,TechID,CompletionDate,Notes")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TechID = new SelectList(db.Techs, "TechID", "LastName", assignment.TechID);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "TicketDescription", assignment.TicketID);
            return View(assignment);
        }

        // GET: Assignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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
