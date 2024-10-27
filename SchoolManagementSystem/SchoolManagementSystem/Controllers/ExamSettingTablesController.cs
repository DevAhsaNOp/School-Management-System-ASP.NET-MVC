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
    public class ExamSettingTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExamSettingTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var examSettingTables = db.ExamSettingTables.Include(e => e.ExamTable).Include(e => e.ProgrameSessionTable).Include(e => e.SessionTable).Include(e => e.UserTable);
            return View(examSettingTables.ToList());
        }

        // GET: ExamSettingTables/Details/5
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
            ExamSettingTable examSettingTable = db.ExamSettingTables.Find(id);
            if (examSettingTable == null)
            {
                return HttpNotFound();
            }
            return View(examSettingTable);
        }

        // GET: ExamSettingTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Exam_ID = new SelectList(db.ExamTables, "ExamID", "Title");
            ViewBag.ProgrameSession_ID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details");
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name");
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: ExamSettingTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamSettingID,User_ID,Session_ID,Exam_ID,ProgrameSession_ID,Description")] ExamSettingTable examSettingTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examSettingTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                db.ExamSettingTables.Add(examSettingTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Exam_ID = new SelectList(db.ExamTables, "ExamID", "Title", examSettingTable.Exam_ID);
            ViewBag.ProgrameSession_ID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", examSettingTable.ProgrameSession_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", examSettingTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", examSettingTable.User_ID);
            return View(examSettingTable);
        }

        // GET: ExamSettingTables/Edit/5
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
            ExamSettingTable examSettingTable = db.ExamSettingTables.Find(id);
            if (examSettingTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Exam_ID = new SelectList(db.ExamTables, "ExamID", "Title", examSettingTable.Exam_ID);
            ViewBag.ProgrameSession_ID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", examSettingTable.ProgrameSession_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", examSettingTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", examSettingTable.User_ID);
            return View(examSettingTable);
        }

        // POST: ExamSettingTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamSettingID,User_ID,Session_ID,Exam_ID,ProgrameSession_ID,Description")] ExamSettingTable examSettingTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examSettingTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                db.Entry(examSettingTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Exam_ID = new SelectList(db.ExamTables, "ExamID", "Title", examSettingTable.Exam_ID);
            ViewBag.ProgrameSession_ID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", examSettingTable.ProgrameSession_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", examSettingTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", examSettingTable.User_ID);
            return View(examSettingTable);
        }

        // GET: ExamSettingTables/Delete/5
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
            ExamSettingTable examSettingTable = db.ExamSettingTables.Find(id);
            if (examSettingTable == null)
            {
                return HttpNotFound();
            }
            return View(examSettingTable);
        }

        // POST: ExamSettingTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamSettingTable examSettingTable = db.ExamSettingTables.Find(id);
            db.ExamSettingTables.Remove(examSettingTable);
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
