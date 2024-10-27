using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace SchoolManagementSystem.Controllers
{
    public class SchoolLeavingTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: SchoolLeavingTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var schoolLeavingTables = db.SchoolLeavingTables.Include(s => s.ClassTable).Include(s => s.StudentTable).Include(s => s.UserTable);
            return View(schoolLeavingTables.ToList());
        }

        // GET: SchoolLeavingTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolLeavingTable schoolLeavingTable = db.SchoolLeavingTables.Find(id);
            if (schoolLeavingTable == null)
            {
                return HttpNotFound();
            }
            return View(schoolLeavingTable);
        }

        // GET: SchoolLeavingTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: SchoolLeavingTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolLeavingID,UserID,StudentID,ClassID,LeavingDate,LeavingReason,LeavingComments,CreatedDate")] SchoolLeavingTable schoolLeavingTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            schoolLeavingTable.UserID = userId;

            if (ModelState.IsValid)
            {
                db.SchoolLeavingTables.Add(schoolLeavingTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", schoolLeavingTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", schoolLeavingTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", schoolLeavingTable.UserID);
            return View(schoolLeavingTable);
        }

        // GET: SchoolLeavingTables/Edit/5
        public ActionResult Edit(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolLeavingTable schoolLeavingTable = db.SchoolLeavingTables.Find(id);
            if (schoolLeavingTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", schoolLeavingTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", schoolLeavingTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", schoolLeavingTable.UserID);
            return View(schoolLeavingTable);
        }

        // POST: SchoolLeavingTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolLeavingID,UserID,StudentID,ClassID,LeavingDate,LeavingReason,LeavingComments,CreatedDate")] SchoolLeavingTable schoolLeavingTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            schoolLeavingTable.UserID = userId;


            if (ModelState.IsValid)
            {
                db.Entry(schoolLeavingTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", schoolLeavingTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", schoolLeavingTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", schoolLeavingTable.UserID);
            return View(schoolLeavingTable);
        }

        // GET: SchoolLeavingTables/Delete/5
        public ActionResult Delete(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolLeavingTable schoolLeavingTable = db.SchoolLeavingTables.Find(id);
            if (schoolLeavingTable == null)
            {
                return HttpNotFound();
            }
            return View(schoolLeavingTable);
        }

        // POST: SchoolLeavingTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolLeavingTable schoolLeavingTable = db.SchoolLeavingTables.Find(id);
            db.SchoolLeavingTables.Remove(schoolLeavingTable);
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
