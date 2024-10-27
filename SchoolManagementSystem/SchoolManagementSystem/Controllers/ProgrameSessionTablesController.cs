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
    public class ProgrameSessionTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ProgrameSessionTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var programeSessionTables = db.ProgrameSessionTables.Include(p => p.ProgrameTable).Include(p => p.SessionTable).Include(p => p.UserTable);
            return View(programeSessionTables.ToList());
        }

        // GET: ProgrameSessionTables/Details/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Programe_ID = new SelectList(db.ProgrameTables, "ProgrameID", "Name");
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name");
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: ProgrameSessionTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                var sessionName = db.SessionTables.Where(s => s.SessionID == programeSessionTable.Session_ID).SingleOrDefault();
                var programName = db.ProgrameTables.Where(s => s.ProgrameID == programeSessionTable.Programe_ID).SingleOrDefault();

                if (sessionName != null)
                {
                    programeSessionTable.Details = "(" + sessionName.Name + ") - " + (programName != null ? programName.Name : "") + programeSessionTable.Details;

                    if (!programeSessionTable.Details.Contains(sessionName.Name))
                    {
                        programeSessionTable.Details = "(" + sessionName.Name + ") - " + (programName != null ? programName.Name : "") + programeSessionTable.Details;
                    }
                }
                db.ProgrameSessionTables.Add(programeSessionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Programe_ID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.Programe_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.User_ID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Edit/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Programe_ID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.Programe_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.User_ID);
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgrameSessionID,User_ID,Session_ID,Programe_ID,Details,RegDate,Description")] ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                var sessionName = db.SessionTables.Where(s => s.SessionID == programeSessionTable.Session_ID).SingleOrDefault();
                var programName = db.ProgrameTables.Where(s => s.ProgrameID == programeSessionTable.Programe_ID).SingleOrDefault();

                if (sessionName != null)
                {
                    if (!programeSessionTable.Details.Contains(sessionName.Name))
                    {
                        var details = "(" + sessionName.Name + " - " + (programName != null ? programName.Name : "") + ")" + programeSessionTable.Details;
                        programeSessionTable.Details = details;
                    }
                }
                db.Entry(programeSessionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Programe_ID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.Programe_ID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.User_ID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Delete/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            db.ProgrameSessionTables.Remove(programeSessionTable);
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
