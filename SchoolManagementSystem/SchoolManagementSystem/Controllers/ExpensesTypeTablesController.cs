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
    public class ExpensesTypeTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExpensesTypeTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var expensesTypeTables = db.ExpensesTypeTables.Include(e => e.UserTable);
            return View(expensesTypeTables.ToList());
        }

        // GET: ExpensesTypeTables/Details/5
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
            ExpensesTypeTable expensesTypeTable = db.ExpensesTypeTables.Find(id);
            if (expensesTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTypeTable);
        }

        // GET: ExpensesTypeTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: ExpensesTypeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseTypeID,User_ID,Name,IsActive")] ExpensesTypeTable expensesTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            expensesTypeTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                db.ExpensesTypeTables.Add(expensesTypeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTypeTable.User_ID);
            return View(expensesTypeTable);
        }

        // GET: ExpensesTypeTables/Edit/5
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
            ExpensesTypeTable expensesTypeTable = db.ExpensesTypeTables.Find(id);
            if (expensesTypeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTypeTable.User_ID);
            return View(expensesTypeTable);
        }

        // POST: ExpensesTypeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseTypeID,User_ID,Name,IsActive")] ExpensesTypeTable expensesTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            expensesTypeTable.User_ID = userId; 

            if (ModelState.IsValid)
            {
                db.Entry(expensesTypeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTypeTable.User_ID);
            return View(expensesTypeTable);
        }

        // GET: ExpensesTypeTables/Delete/5
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
            ExpensesTypeTable expensesTypeTable = db.ExpensesTypeTables.Find(id);
            if (expensesTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTypeTable);
        }

        // POST: ExpensesTypeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpensesTypeTable expensesTypeTable = db.ExpensesTypeTables.Find(id);

            db.ExpensesTypeTables.Remove(expensesTypeTable);
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
