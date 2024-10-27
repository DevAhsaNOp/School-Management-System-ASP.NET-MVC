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
    public class ExpensesTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExpensesTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var expensesTables = db.ExpensesTables.Include(e => e.ExpensesTypeTable).Include(e => e.UserTable);
            return View(expensesTables.ToList());
        }

        // GET: ExpensesTables/Details/5
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
            ExpensesTable expensesTable = db.ExpensesTables.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTable);
        }

        // GET: ExpensesTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ExpenseType_ID = new SelectList(db.ExpensesTypeTables, "ExpenseTypeID", "Name");
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: ExpensesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesID,ExpenseType_ID,User_ID,Amount,InvoiceNo,Reason,ExpDate,Descrption")] ExpensesTable expensesTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            expensesTable.User_ID = userId;

            if (ModelState.IsValid)
            {
                db.ExpensesTables.Add(expensesTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpenseType_ID = new SelectList(db.ExpensesTypeTables, "ExpenseTypeID", "Name", expensesTable.ExpenseType_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTable.User_ID);
            return View(expensesTable);
        }

        // GET: ExpensesTables/Edit/5
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
            ExpensesTable expensesTable = db.ExpensesTables.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpenseType_ID = new SelectList(db.ExpensesTypeTables, "ExpenseTypeID", "Name", expensesTable.ExpenseType_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTable.User_ID);
            return View(expensesTable);
        }

        // POST: ExpensesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesID,ExpenseType_ID,User_ID,Amount,InvoiceNo,Reason,ExpDate,Descrption")] ExpensesTable expensesTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            expensesTable.User_ID = userId;
            if (ModelState.IsValid)
            {
                db.Entry(expensesTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseType_ID = new SelectList(db.ExpensesTypeTables, "ExpenseTypeID", "Name", expensesTable.ExpenseType_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", expensesTable.User_ID);
            return View(expensesTable);
        }

        // GET: ExpensesTables/Delete/5
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
            ExpensesTable expensesTable = db.ExpensesTables.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTable);
        }

        // POST: ExpensesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpensesTable expensesTable = db.ExpensesTables.Find(id);
            db.ExpensesTables.Remove(expensesTable);
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
