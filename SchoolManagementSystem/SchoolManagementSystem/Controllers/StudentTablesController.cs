using AutoMapper;
using DatabaseAccess;
using SchoolManagementSystem.Request;
using SchoolManagementSystem.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StudentTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var studentTables = db.StudentTables.Include(s => s.ClassSectionTable).Include(s => s.ProgrameTable).Include(s => s.SessionTable).Include(s => s.UserTable);
            return View(studentTables.ToList());
        }

        // GET: StudentTables/Details/5
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
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // GET: StudentTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ClassID = new SelectListItem[] { new SelectListItem { Text = "Select Class", Value = "", Disabled = true, Selected = true } }
            .Concat(db.ClassTables.Select(x => new SelectListItem { Text = x.Name, Value = x.ClassID.ToString() })).ToList();
            var classGroupBySection = db.ClassSectionTables
                .GroupBy(x => x.ClassID)
                .Select(x => new
                {
                    ClassID = x.Key,
                    Section = x.Select(y => new
                    {
                        Id = y.SectionID,
                        Name = y.SectionTable.SectionName.Trim()
                    }).ToList()
                }).ToList();
            ViewBag.ClassWithSections = classGroupBySection;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentRequest studentTable, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                if (studentTable.ClassID != null && studentTable.ClassID.GetValueOrDefault() > 0)
                {
                    ViewBag.ClassID = new SelectListItem[] { new SelectListItem { Text = "Select Class", Value = "", Disabled = true, Selected = false } }
                    .Concat(db.ClassTables.Select(x => new SelectListItem { Text = x.Name, Value = x.ClassID.ToString(), Selected = x.ClassID == studentTable.ClassID })).ToList();
                }
                else
                {
                    ViewBag.ClassID = new SelectListItem[] { new SelectListItem { Text = "Select Class", Value = "", Disabled = true, Selected = true } }
                    .Concat(db.ClassTables.Select(x => new SelectListItem { Text = x.Name, Value = x.ClassID.ToString() })).ToList();
                }

                var classGroupBySection = db.ClassSectionTables
                    .GroupBy(x => x.ClassID)
                    .Select(x => new
                    {
                        ClassID = x.Key,
                        Section = x.Select(y => new
                        {
                            Id = y.SectionID,
                            Name = y.SectionTable.SectionName.Trim()
                        }).ToList()
                    }).ToList();
                ViewBag.ClassWithSections = classGroupBySection;
                ModelState.AddModelError("", "Please fill all required fields.");
                return View(studentTable);
            }

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            studentTable.User_ID = userId;
            studentTable.Photo = "/Content/StaffPhoto/default.png";

            if (image != null)
            {
                string fileName = image.FileName;
                string _path = Path.Combine(Server.MapPath("/Content/StudentPhoto"), fileName);
                image.SaveAs(_path);
                studentTable.Photo = "/Content/StudentPhoto/" + fileName;
            }

            if (ModelState.IsValid)
            {
                var classSectionID = db.ClassSectionTables.FirstOrDefault(x => x.ClassID == studentTable.ClassID && x.SectionID == studentTable.SectionID).ClassSectionID;
                StudentTable student = new StudentTable
                {
                    User_ID = studentTable.User_ID,
                    ClassSectionID = classSectionID,
                    Name = studentTable.Name,
                    FatherName = studentTable.FatherName,
                    DateofBirth = Convert.ToDateTime(studentTable.DateofBirth),
                    AddmissionDate = Convert.ToDateTime(studentTable.AddmissionDate),
                    Photo = studentTable.Photo,
                    Gender = studentTable.Gender,
                    Address = studentTable.Address,
                    ContactNo = studentTable.ContactNo,
                    EmailAddress = studentTable.EmailAddress,
                    GuardianContactNo = studentTable.GuardianContactNo,
                    Nationality = studentTable.Nationality,
                    PreviousSchool = studentTable.PreviousSchool,
                    PreviousPercentage = studentTable.PreviousPercentage,
                };

                db.StudentTables.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentTable);
        }

        // GET: StudentTables/Edit/5
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

            StudentTable studentTable = db.StudentTables.Find(id);

            if (studentTable == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClassID = db.ClassTables.Select(x => new ClassDDViewModel { ClassID = x.ClassID, ClassName = x.Name }).ToList();

            var classGroupBySection = db.ClassSectionTables
                .GroupBy(x => x.ClassID)
                .Select(x => new
                {
                    ClassID = x.Key,
                    Section = x.Select(y => new
                    {
                        Id = y.SectionID,
                        Name = y.SectionTable.SectionName.Trim()
                    }).ToList()
                }).ToList();

            ViewBag.ClassWithSections = classGroupBySection;
            StudentRequest student = Mapper.Map<StudentRequest>(studentTable);

            return View(student);
        }

        // POST: StudentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentRequest studentTable, HttpPostedFileBase image)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                if (studentTable.ClassID != null && studentTable.ClassID.GetValueOrDefault() > 0)
                {
                    ViewBag.ClassID = new SelectListItem[] { new SelectListItem { Text = "Select Class", Value = "", Disabled = true, Selected = false } }
                    .Concat(db.ClassTables.Select(x => new SelectListItem { Text = x.Name, Value = x.ClassID.ToString(), Selected = x.ClassID == studentTable.ClassID })).ToList();
                }
                else
                {
                    ViewBag.ClassID = new SelectListItem[] { new SelectListItem { Text = "Select Class", Value = "", Disabled = true, Selected = true } }
                    .Concat(db.ClassTables.Select(x => new SelectListItem { Text = x.Name, Value = x.ClassID.ToString() })).ToList();
                }

                var classGroupBySection = db.ClassSectionTables
                    .GroupBy(x => x.ClassID)
                    .Select(x => new
                    {
                        ClassID = x.Key,
                        Section = x.Select(y => new
                        {
                            Id = y.SectionID,
                            Name = y.SectionTable.SectionName.Trim()
                        }).ToList()
                    }).ToList();
                ViewBag.ClassWithSections = classGroupBySection;
                ModelState.AddModelError("", "Please fill all required fields.");
                return View(studentTable);
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            studentTable.User_ID = userId;

            if (image != null)
            {
                string fileName = image.FileName;
                string _path = Path.Combine(Server.MapPath("/Content/StudentPhoto"), fileName);
                image.SaveAs(_path);
                studentTable.Photo = "/Content/StudentPhoto/" + fileName;
            }

            if (ModelState.IsValid)
            {
                var classSectionID = db.ClassSectionTables.FirstOrDefault(x => x.ClassID == studentTable.ClassID && x.SectionID == studentTable.SectionID).ClassSectionID;
                StudentTable student = new StudentTable
                {
                    StudentID = studentTable.StudentID,
                    User_ID = studentTable.User_ID,
                    ClassSectionID = classSectionID,
                    Name = studentTable.Name,
                    FatherName = studentTable.FatherName,
                    DateofBirth = Convert.ToDateTime(studentTable.DateofBirth),
                    AddmissionDate = Convert.ToDateTime(studentTable.AddmissionDate),
                    Photo = studentTable.Photo,
                    Gender = studentTable.Gender,
                    Address = studentTable.Address,
                    ContactNo = studentTable.ContactNo,
                    EmailAddress = studentTable.EmailAddress,
                    GuardianContactNo = studentTable.GuardianContactNo,
                    Nationality = studentTable.Nationality,
                    PreviousSchool = studentTable.PreviousSchool,
                    PreviousPercentage = studentTable.PreviousPercentage,
                };
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentTable);
        }

        // GET: StudentTables/Delete/5
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
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // POST: StudentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentTable studentTable = db.StudentTables.Find(id);
            db.StudentTables.Remove(studentTable);
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
