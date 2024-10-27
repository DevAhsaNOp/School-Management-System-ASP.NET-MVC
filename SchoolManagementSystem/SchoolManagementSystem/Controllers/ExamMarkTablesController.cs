using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using DatabaseAccess;

namespace SchoolManagementSystem.Controllers
{
    public class ExamMarkTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExamMarkTables
        public Nullable<int> SubjectID { get; set; }
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var examMarkTables = db.ExamMarkTables.Include(e => e.ExamTable).Include(e => e.StudentTable).Include(e => e.UserTable).OrderByDescending(e => e.MarkID);
            return View(examMarkTables.ToList());
        }

        // GET: ExamMarkTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            } if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMarkTable examMarkTable = db.ExamMarkTables.Find(id);
            if (examMarkTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarkTable);
        }

        // GET: ExamMarkTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Title");
            return View();
        }

        // POST: ExamMarkTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ExamMarkTable examMarkTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarkTable.UserID = userId;

            if (ModelState.IsValid)
            {
                db.ExamMarkTables.Add(examMarkTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarkTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarkTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarkTable.UserID);
            return View(examMarkTable);
        }

        // GET: ExamMarkTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMarkTable examMarkTable = db.ExamMarkTables.Find(id);
            if (examMarkTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarkTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarkTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarkTable.UserID);
            return View(examMarkTable);
        }

        // POST: ExamMarkTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamMarkTable examMarkTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarkTable.UserID = userId;

            if (ModelState.IsValid)
            {
                db.Entry(examMarkTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarkTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarkTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarkTable.UserID);
            return View(examMarkTable);
        }

        // GET: ExamMarkTables/Delete/5
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
            ExamMarkTable examMarkTable = db.ExamMarkTables.Find(id);
            if (examMarkTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarkTable);
        }

        // POST: ExamMarkTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamMarkTable examMarkTable = db.ExamMarkTables.Find(id);
            db.ExamMarkTables.Remove(examMarkTable);
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

        public ActionResult GetByPromoteID(string sid)
        {
            int promoteid = Convert.ToInt32(sid);
            var promoterecord = db.StudentPromoteTables.Find(promoteid);
            List<StudentTable> stdlist = new List<StudentTable>();
       
                stdlist.Add(new StudentTable { StudentID = (int)promoterecord.StudentID, Name = promoterecord.StudentTable.Name });
            
         
            List<ClassSubjectTable> listsubjects = new List<ClassSubjectTable>();
            var classsubject = db.ClassSubjectTables.Where(cls => cls.ClassID == promoterecord.ClassID && cls.IsActive == true);
            foreach( var subj in classsubject)
            {
                listsubjects.Add(new ClassSubjectTable { ClassSubjectID = subj.ClassSubjectID });
            }

            return Json(new { student = stdlist, subject = listsubjects }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalMarks(string sid)
        {
            int classsubjectid = Convert.ToInt32(sid);
            var totalmark = db.ClassSubjectTables.Find(classsubjectid).SubjectTable.TotalMarks;
            return Json(new { data = totalmark}, JsonRequestBehavior.AllowGet);
        }
    }
}
