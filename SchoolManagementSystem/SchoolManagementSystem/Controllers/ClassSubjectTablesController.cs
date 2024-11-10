using DatabaseAccess;
using SchoolManagementSystem.Request;
using SchoolManagementSystem.Response;
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
                .Where(c => c.IsActive == true)
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
                return RedirectToAction("Login", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var classSubjectDetails = db.ClassSubjectTables.Where(cst => cst.ClassSectionID == id)
                .Select(cst => new ClassSubjectResponse
                {
                    ClassSectionID = cst.ClassSectionID,
                    Subjects = db.ClassSubjectTables
                    .Where(c => c.ClassSectionID == id && c.IsActive == true)
                    .Select(s => s.SubjectTable.Name).ToList(),
                    ClassName = cst.ClassSectionTable.ClassTable.Name,
                    Section = cst.ClassSectionTable.SectionTable.SectionName,
                })
                .ToList();

            if (classSubjectDetails == null || classSubjectDetails.Count == 0)
                return HttpNotFound();

            var classSubjectData = new ClassSubjectResponse
            {
                ClassSectionID = classSubjectDetails.FirstOrDefault().ClassSectionID,
                Subjects = classSubjectDetails.SelectMany(s => s.Subjects).Distinct().ToList(),
                ClassName = classSubjectDetails.FirstOrDefault().ClassName,
                Section = classSubjectDetails.FirstOrDefault().Section
            };

            return View(classSubjectData);
        }

        // GET: ClassSubjectTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var allClassSectionsWhomSubjectsAreNotAssigned = db.ClassSectionTables
                .Where(s => s.IsActive == true && s
                .ClassSubjectTables.All(cs => !cs.IsActive))
                .ToList();

            ViewBag.ClassSectionID = new SelectList(allClassSectionsWhomSubjectsAreNotAssigned, "ClassSectionID", "Title");
            var model = new ClassSubjectCreateRequest
            {
                SubjectID = db.SubjectTables.Select(s => new SelectListItem { Text = s.Name, Value = s.SubjectID.ToString(), Selected = false }).ToList()
            };
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
                var subjectIds = classSubjectTable.SubjectID.Where(s => s.Selected).Select(s => Convert.ToInt32(s.Value)).ToList();
                var classTitle = db.ClassSectionTables.Where(s => s.ClassSectionID == classSubjectTable.ClassSectionID).Select(s => s.Title).FirstOrDefault();
                var subjectList = db.SubjectTables.Where(s => subjectIds.Contains(s.SubjectID)).Select(s => new { s.SubjectID, s.Name }).ToList();

                foreach (var subjectId in subjectIds)
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
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
                return RedirectToAction("Login", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var classSubjectTable = db.ClassSubjectTables
                .Where(cst => cst.ClassSectionID == id && cst.IsActive == true)
                .ToList();

            if (classSubjectTable == null || classSubjectTable.Count == 0)
                return HttpNotFound();

            var classSubjectData = new ClassSubjectUpdateRequest
            {
                Title = classSubjectTable.FirstOrDefault().Title,
                SubjectID = classSubjectTable.Where(x => x.IsActive)
                .Select(s => new SelectListItem
                {
                    Text = s.SubjectTable.Name,
                    Value = s.SubjectID.ToString(),
                    Selected = true
                }).ToList(),
                ClassSubjectID = classSubjectTable.FirstOrDefault().ClassSubjectID,
                IsActive = classSubjectTable.FirstOrDefault().IsActive,
                ClassSectionID = classSubjectTable.FirstOrDefault().ClassSectionID
            };

            ViewBag.ClassSectionID = new SelectList(db.ClassSectionTables.Where(s => s.IsActive == true), "ClassSectionID", "Title", id);

            var subjectList = db.SubjectTables
                .Select(s => new SelectListItem { Text = s.Name, Value = s.SubjectID.ToString(), Selected = false })
                .ToList();
            var finalSubjectList = subjectList
                .Select(s => new SelectListItem
                {
                    Text = s.Text,
                    Value = s.Value,
                    Selected = classSubjectData.SubjectID.Any(si => si.Value == s.Value)
                })
                .ToList();
            classSubjectData.SubjectID = finalSubjectList;

            return View(classSubjectData);
        }

        // POST: ClassSubjectTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSubjectUpdateRequest classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var classSubjectData = db.ClassSubjectTables.Where(cst => cst.ClassSectionID == classSubjectTable.ClassSectionID && cst.IsActive == true).ToList();
                classSubjectData.ForEach(cst => cst.IsActive = false);
                classSubjectData.ForEach(cst => db.Entry(cst).State = EntityState.Modified);
                db.SaveChanges();

                var subjectIds = classSubjectTable.SubjectID.Where(s => s.Selected).Select(s => Convert.ToInt32(s.Value)).ToList();
                var classTitle = db.ClassSectionTables.Where(s => s.ClassSectionID == classSubjectTable.ClassSectionID).Select(s => s.Title).FirstOrDefault();
                var subjectList = db.SubjectTables.Where(s => subjectIds.Contains(s.SubjectID)).Select(s => new { s.SubjectID, s.Name }).ToList();

                foreach (var subjectId in subjectIds)
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
            return View(classSubjectTable);
        }

        //// GET: ClassSubjectTables/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
        //    if (classSubjectTable == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classSubjectTable);
        //}

        //// POST: ClassSubjectTables/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
        //    db.ClassSubjectTables.Remove(classSubjectTable);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
