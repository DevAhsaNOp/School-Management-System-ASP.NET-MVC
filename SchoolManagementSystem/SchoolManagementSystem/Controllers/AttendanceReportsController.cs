using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AttendanceReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: AttendanceReports
        public ActionResult StudentAttendance(int? id)
        {
            var classid = db.StudentPromoteTables.Where(p => p.StudentID == id && p.IsActive == true).FirstOrDefault().ClassID;
            var studentattendance = db.AttendanceTables.Where(a => a.Student_ID == id && a.ClassID == classid).OrderByDescending(a=>a.AttendanceID);
            return View(studentattendance);
        }

        public ActionResult StudentAttendanceWise(int? id)
        {
            var attendance = db.AttendanceTables;
            return View(attendance);
        }
        public ActionResult StaffAttendanceWise(int? id)
        {
            var attendance = db.StaffAttendanceTables;
            return View(attendance);
        }
        public ActionResult StaffAttendance(int? id)
        {
            var staffattendance = db.StaffAttendanceTables.Where(a => a.StaffAttendanceID == id);
            return View(staffattendance);
        }
    }
}