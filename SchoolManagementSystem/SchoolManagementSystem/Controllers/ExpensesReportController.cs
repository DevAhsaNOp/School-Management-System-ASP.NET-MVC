using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExpensesReportController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: ExpensesReport
        public ActionResult ExpensesWise()
        {
            var expenses = db.ExpensesTables.ToList().OrderByDescending(e=>e.ExpensesID);
            return View(expenses);
        }

        public ActionResult CustomExpenses()
        {
            var expenses = db.ExpensesTables.Where(e => e.ExpDate >= DateTime.Now && e.ExpDate <= DateTime.Now).ToList().OrderByDescending(e => e.ExpensesID);
            return View(expenses);
        }

        [HttpPost]
        public ActionResult CustomExpenses(DateTime fromDate, DateTime toDate)
        {
            var expenses = db.ExpensesTables.Where(e => e.ExpDate >= fromDate && e.ExpDate <= toDate).ToList().OrderByDescending(e => e.ExpensesID);
            return View(expenses);
        }
    }
}