using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;
using Magnum.FileSystem;

namespace SchoolManagementSystem.Controllers
{
    public class StaffTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StaffTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var staffTables = db.StaffTables.Include(s => s.DesignationTable).Include(s => s.UserTable);
            return View(staffTables.ToList());
        }

        // GET: StaffTables/Details/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // GET: StaffTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title");
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: StaffTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,User_ID,Name,Designation_ID,ContactNo,EmailAddress,Address,Qualification,Photo,Description,IsActive,Gender,BasicSalary,RegistrationDate, PhotoFile")] StaffTable staffTable, HttpPostedFileBase image)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            staffTable.User_ID = userId;
            staffTable.Photo = "/Content/StaffPhoto/default.png";

            if (image != null)
            {
                string fileName = image.FileName;
                string _path = Path.Combine(Server.MapPath("/Content/StaffPhoto"), fileName);
                image.SaveAs(_path);
                staffTable.Photo = "/Content/StaffPhoto/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.StaffTables.Add(staffTable);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.User_ID);
            return View(staffTable);
        }

        // GET: StaffTables/Edit/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.User_ID);
            return View(staffTable);
        }

        // POST: StaffTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,User_ID,Name,Designation_ID,ContactNo,EmailAddress,Address,Qualification,Photo,Description,IsActive,Gender,BasicSalary,RegistrationDate")] StaffTable staffTable, HttpPostedFileBase image)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            staffTable.User_ID = userId;

            if (image != null)
            {
                string fileName = image.FileName;
                string _path = Path.Combine(Server.MapPath("/Content/StaffPhoto"), fileName);
                image.SaveAs(_path);
                staffTable.Photo = "/Content/StaffPhoto/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(staffTable).State = EntityState.Modified;
               

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.User_ID);
            return View(staffTable);
        }

        // GET: StaffTables/Delete/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // POST: StaffTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffTable staffTable = db.StaffTables.Find(id);
            db.StaffTables.Remove(staffTable);
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
