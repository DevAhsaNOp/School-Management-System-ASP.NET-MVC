using DatabaseAccess;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ClassSubjectTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ClassSubjectTables
        public async Task<ActionResult> Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var classSubject = await db.ClassSubjectTables
                .AsQueryable()
                .AsNoTracking()
                .Include(c => c.SubjectTable)
                .Include(c => c.ClassSectionTable)
                .GroupBy(x => x.ClassSectionID)
                .ToListAsync();

            var classSubjectList = classSubject
                 .Select(x => new ClassSubjectVM
                 {
                     ClassSectionID = x.Key,
                     Subjects = x.Select(y => y.SubjectTable.Name).ToList(),
                     Title = x.Select(y => y.ClassSectionTable.Title).FirstOrDefault(),
                     IsActive = x.Select(y => y.IsActive).FirstOrDefault()
                 });

            return View(classSubjectList);
        }

        // GET: ClassSubjectTables/Details/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ClassSectionID = new SelectList(db.ClassSectionTables.Where(s => s.IsActive == true), "ClassSectionID", "Title");
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");

            var model = new ClassSubjectCreateRequest();
            return View(model);
        }

        // POST: ClassSubjectTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassSubjectCreateRequest classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var classTitle = db.ClassSectionTables.Where(s => s.ClassSectionID == classSubjectTable.ClassSectionID).Select(s => s.Title).FirstOrDefault();
                var subjectList = db.SubjectTables.Where(s => classSubjectTable.SubjectID.Contains(s.SubjectID)).Select(s => new { s.SubjectID, s.Name }).ToList();

                foreach (var subjectId in classSubjectTable.SubjectID)
                {
                    var classSubject = new ClassSubjectTable
                    {
                        ClassSectionID = classSubjectTable.ClassSectionID,
                        SubjectID = subjectId,
                        Title = classTitle + " - " + subjectList.FirstOrDefault(su => su.SubjectID == subjectId).Name,
                        IsActive = classSubjectTable.IsActive,
                        ClassSubjectID = classSubjectTable.ClassSubjectID
                    };
                    db.ClassSubjectTables.Add(classSubject);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSectionID = new SelectList(db.ClassSectionTables.Where(s => s.IsActive == true), "ClassSectionID", "Title", classSubjectTable.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);

            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Edit/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSectionID = new SelectList(db.ClassTables.Where(s => s.IsActive == true), "ClassSectionID", "Title", classSubjectTable.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSubjectUpdateteRequest classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var classTitle = db.ClassSectionTables.Where(s => s.ClassSectionID == classSubjectTable.ClassSectionID).Select(s => s.Title).FirstOrDefault();
                var subjectList = db.SubjectTables.Where(s => classSubjectTable.SubjectID.Contains(s.SubjectID)).Select(s => new { s.SubjectID, s.Name }).ToList();

                foreach (var subjectId in classSubjectTable.SubjectID)
                {
                    var classSubject = new ClassSubjectTable
                    {
                        ClassSectionID = classSubjectTable.ClassSectionID,
                        SubjectID = subjectId,
                        Title = classTitle + " - " + subjectList.FirstOrDefault(su => su.SubjectID == subjectId).Name,
                        IsActive = classSubjectTable.IsActive,
                        ClassSubjectID = classSubjectTable.ClassSubjectID
                    };
                    db.Entry(classSubject).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSectionID = new SelectList(db.ClassSectionTables.Where(s => s.IsActive == true), "ClassSectionID", "Title", classSubjectTable.ClassSectionID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);

            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Delete/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            db.ClassSubjectTables.Remove(classSubjectTable);
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
