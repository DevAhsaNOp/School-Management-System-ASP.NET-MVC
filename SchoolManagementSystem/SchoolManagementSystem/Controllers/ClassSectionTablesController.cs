using DatabaseAccess;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ClassSectionTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ClassSectionTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var classSectionTables = db.ClassSectionTables.Include(c => c.ClassTable).Include(c => c.SectionTable);
            return View(classSectionTables.ToList());
        }

        // GET: ClassSectionTables/Details/5
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
            ClassSectionTable classSectionTable = db.ClassSectionTables.Find(id);
            if (classSectionTable == null)
            {
                return HttpNotFound();
            }
            return View(classSectionTable);
        }

        // GET: ClassSectionTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name");
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName");
            return View();
        }

        // POST: ClassSectionTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassSectionID,ClassID,SectionID,Title,IsActive")] ClassSectionTable classSectionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.ClassSectionTables.Add(classSectionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", classSectionTable.ClassID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", classSectionTable.SectionID);
            return View(classSectionTable);
        }

        // GET: ClassSectionTables/Edit/5
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
            ClassSectionTable classSectionTable = db.ClassSectionTables.Find(id);
            if (classSectionTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", classSectionTable.ClassID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", classSectionTable.SectionID);
            return View(classSectionTable);
        }

        // POST: ClassSectionTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassSectionID,ClassID,SectionID,Title,IsActive")] ClassSectionTable classSectionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(classSectionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", classSectionTable.ClassID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", classSectionTable.SectionID);
            return View(classSectionTable);
        }

        // GET: ClassSectionTables/Delete/5
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
            ClassSectionTable classSectionTable = db.ClassSectionTables.Find(id);
            if (classSectionTable == null)
            {
                return HttpNotFound();
            }

            var isClassSectionAssignedToAnySubjects = db.ClassSubjectTables.Any(x => x.ClassSectionID == id);
            if (isClassSectionAssignedToAnySubjects)
            {
                TempData["ErrorMessage"] = "This class section is assigned to subjects first delete the subjects assigned to this class section.";
                return RedirectToAction("Index");
            }

            return View(classSectionTable);
        }

        // POST: ClassSectionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ClassSectionTable classSectionTable = db.ClassSectionTables.Find(id);
            db.ClassSectionTables.Remove(classSectionTable);
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
