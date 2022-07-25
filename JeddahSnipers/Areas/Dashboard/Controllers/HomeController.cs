using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeddahSnipers.ViewModels;
using JeddahSnipers.Models;
using JeddahSnipers.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using JeddahSnipers.Classes;

namespace JeddahSnipers.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        readonly ApplicationDbContext _wonder;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ApplicationDbContext wonder, IHostingEnvironment hostingEnvironment)
        {
            _wonder = wonder;
            this.hostingEnvironment = hostingEnvironment;
        }
        [SessionFilter]
        public IActionResult Index()
        {
            var students = _wonder.Students.Where(x => x.Status == "hold" || x.Status == "active").ToList();
            foreach (var item in students)
            {
                int counter = 0;
                var payments = _wonder.Payments.Where(x => x.StudentId == item.StudentId).ToList();
                foreach (var x in payments)
                {
                    if (x.EndDate > DateTime.UtcNow)
                    {
                        counter++;
                    }
                }
                if (counter == 0)
                {
                    item.Status = "expired";
                    _wonder.Students.Update(item);
                    _wonder.SaveChanges();
                }
            }
            DashboardIndexViewModel model = new DashboardIndexViewModel();
            model.coachesCount = _wonder.Coachs.ToList().Count;
            model.studentsCount = _wonder.Students.ToList().Count;
            model.groupsCount = _wonder.Groups.ToList().Count;

            model.LastStudents = _wonder.Students.Select(x => new LastStudentsViewModel { StudentId = x.StudentId, CategoryName = x.Category.CategoryName, StudentFirstName = x.FirstName, StudentLastName = x.LastName, StudentNationality = x.Nationality, StudentAge = x.Group.Age }).OrderByDescending(x => x.StudentId).Take(5);
            model.LastGroups = _wonder.Groups.Select(x => new LastGroupsViewModel { GroupId = x.GroupId, GroupName = x.GroupName, StudentsCount = x.Student.ToList().Count, StartTime = x.StartTime, EndTime = x.EndTime, CoachFirstName = x.Coach.FirstName, CoachLastName = x.Coach.LastName }).OrderByDescending(x => x.GroupId).Take(5);
            model.HoldAttendance = _wonder.Students.Where(x => x.Status == "hold").Select(x => new HoldAttendanceViewModel { StudentId = x.StudentId, CategoryName = x.Category.CategoryName, StudentFirstName = x.FirstName, StudentLastName = x.LastName, StudentPhone = x.Phone }).OrderByDescending(x => x.StudentId).Take(5);
            model.ExpiredPayments = _wonder.Payments.Where(x => x.EndDate < DateTime.UtcNow).OrderByDescending(x => x.PaymentId).Select(x => new ExpiredPaymentViewModel { StudentName = x.Student.FirstName + " " + x.Student.LastName, Amount = x.Amount, Duration = x.Duration, EndDate = x.EndDate }).Take(5);
            return View(model);

        }
        #region الادمن
        [SessionFilter]
        public ActionResult UpdateAdminProfile()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            int adminId = HttpContext.Session.GetInt32("AdminID").GetValueOrDefault();
            return View(_wonder.Admins.Where(x => x.Id == adminId).FirstOrDefault());
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult UpdateAdminProfile(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _wonder.Entry(admin).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("UpdateAdminProfile");
            }

        }

        #endregion

        #region تسجيل الدخول والخروج 
        public IActionResult Login()
        {
            return View();
        }
        public ActionResult EnterAccount(Admin admin)
        {
            if (_wonder.Admins.Where(x => x.Email == admin.Email).FirstOrDefault() != null)
            {
                if (_wonder.Admins.Where(x => x.Email == admin.Email && x.Password == admin.Password).FirstOrDefault() != null)
                {
                    var id = _wonder.Admins.Where(x => x.Email == admin.Email).Select(x => x.Id).FirstOrDefault();
                    HttpContext.Session.SetInt32("AdminID", id);
                    HttpContext.Session.SetString("AdminName", _wonder.Admins.Where(x => x.Id == id).Select(x => x.FullName).FirstOrDefault());
                    return Json("return to index");
                }
                else
                {
                    return Json("wrong password");
                }
            }
            else
                return Json("this email isn't exist");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("AdminID");
            return RedirectToAction("Login");
        }

        #endregion

        #region الطلاب
        //اضف جديد
        [SessionFilter]
        public ActionResult AddNewStudent()
        {
            StudentAndParent model = new StudentAndParent();
            model.categories = _wonder.Categories.ToList();
            model.groups = _wonder.Groups.ToList();
            return View(model);

        }
        [HttpPost]
        [SessionFilter]
        public ActionResult AddNewStudent(StudentAndParent studentAndparent)
        {
            if (ModelState.IsValid)
            {
                if (studentAndparent.StudentNationalIDFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(studentAndparent.StudentNationalIDFile.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    studentAndparent.StudentNationalIDFile.CopyTo(new FileStream(path, FileMode.Create));
                    studentAndparent.student.NationalIDFile = newfileName;
                }
                if (studentAndparent.StudentApplicationFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(studentAndparent.StudentApplicationFile.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    studentAndparent.StudentApplicationFile.CopyTo(new FileStream(path, FileMode.Create));
                    studentAndparent.student.ApplicationFile = newfileName;
                }
                if (studentAndparent.studentImage != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(studentAndparent.studentImage.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    studentAndparent.studentImage.CopyTo(new FileStream(path, FileMode.Create));
                    studentAndparent.student.Image = newfileName;
                }

                _wonder.Students.Add(studentAndparent.student);
                _wonder.SaveChanges();
                return RedirectToAction("StudentMenu");
            }
            else
            {
                studentAndparent.categories = _wonder.Categories.ToList();
                studentAndparent.groups = _wonder.Groups.ToList();
                return View(studentAndparent);
            }
        }

        //قائمة الطلاب
        [SessionFilter]
        public ActionResult StudentMenu()
        {


            return View();
        }
        public ActionResult StudentsData()
        {
            List<StudentAndParent> data = new List<StudentAndParent>();
            var obj = _wonder.Students.Select(x => x).ToList();
            foreach (var item in obj)
            {
                StudentAndParent O = new StudentAndParent();
                O.student = item;

                O.StudentName = item.FirstName + " " + item.LastName; //full name not first name
                O.GroupName = _wonder.Groups.Where(x => x.GroupId == item.GroupId).Select(x => x.GroupName).FirstOrDefault();
                O.CategoryName = _wonder.Categories.Where(x => x.CategoryId == item.CategoryId).Select(x => x.CategoryName).FirstOrDefault();

                data.Add(O);
            }
            return Json(data);
        }
        public ActionResult DeleteStudent(int StudentId)
        {
            var studentRow = _wonder.Students.Where(x => x.StudentId == StudentId).FirstOrDefault();

            if (studentRow != null)
            {
                _wonder.Students.Remove(studentRow);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        [SessionFilter]
        public IActionResult UpdateStudent(int id)
        {

            UpdateStudentViewModel model = new UpdateStudentViewModel();
            model.student = _wonder.Students.Where(x => x.StudentId == id).FirstOrDefault();
            model.groups = _wonder.Groups.ToList();
            model.categories = _wonder.Categories.ToList();

            return View(model);
        }

        [HttpPost]
        [SessionFilter]
        public IActionResult UpdateStudent(UpdateStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NationalIDFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(model.NationalIDFile.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    model.NationalIDFile.CopyTo(new FileStream(path, FileMode.Create));
                    model.student.NationalIDFile = newfileName;
                }
                if (model.ApplicationFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(model.ApplicationFile.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    model.ApplicationFile.CopyTo(new FileStream(path, FileMode.Create));
                    model.student.ApplicationFile = newfileName;
                }
                if (model.studentImage != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(model.studentImage.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    model.studentImage.CopyTo(new FileStream(path, FileMode.Create));
                    model.student.Image = newfileName;
                }
                _wonder.Entry(model.student).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("StudentMenu");
            }
            else
            {
                UpdateStudentViewModel x = new UpdateStudentViewModel();
                x.student = model.student;
                x.groups = _wonder.Groups.ToList();
                x.categories = _wonder.Categories.ToList();

                return View(x);//RedirectToAction("UpdateStudent", "Home", new {id = student.StudentId ,student});
            }
        }

        //الحضور اليومي
        [SessionFilter]
        public ActionResult DailyAttendance()
        {

            AttendanceViewModel model = new AttendanceViewModel();
            model.students = _wonder.Students.ToList();
            model.attendanceDate = DateTime.UtcNow.ToString("D");
            return View(model);
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult AddAttendance(AttendanceViewModel attendanceViewModel)
        {
            if (ModelState.IsValid)
            {
                Attendance attendance = new Attendance();
                attendance.AttendanceDate = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
                attendance.AttendanceStatus = attendanceViewModel.attendanceStatus;
                attendance.StudentId = attendanceViewModel.studentId;
                var registeredAttendance = _wonder.Attendances.Where(x => x.AttendanceDate == attendance.AttendanceDate && x.StudentId == attendance.StudentId).ToList().Count;
                if (registeredAttendance != 0)
                {
                    TempData["attendanceRecordAlreadySaved"] = "لا تستطيع تسجيل نفس الطالب أكثر من مرة في نفس اليوم";
                    return RedirectToAction("DailyAttendance");
                }
                _wonder.Attendances.Add(attendance);
                _wonder.SaveChanges();
                return RedirectToAction("DailyAttendance");

            }
            return RedirectToAction("DailyAttendance");
        }
        #endregion

        #region الحضور
        public ActionResult getAttendances()
        {

            List<AttendanceViewModel> att = new List<AttendanceViewModel>();
            var attendances = _wonder.Attendances.ToList();
            foreach (var item in attendances)
            {
                AttendanceViewModel obj = new AttendanceViewModel();
                obj.StudentName = _wonder.Students.Where(x => x.StudentId == item.StudentId).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Students.Where(x => x.StudentId == item.StudentId).Select(x => x.LastName).FirstOrDefault();
                obj.attendance = item;
                att.Add(obj);
            }

            //model.students.Select(x=>x.Attendance)
            return Json(att);
        }
        public ActionResult DeleteAttendance(int id)
        {
            var categoryRow = _wonder.Attendances.Where(x => x.AttendanceId == id).FirstOrDefault();

            if (categoryRow != null)
            {
                _wonder.Attendances.Remove(categoryRow);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult UpdateAttendance(AttendanceViewModel attendance)
        {
            var attendanceRow = _wonder.Attendances.Where(x => x.AttendanceId == attendance.attendanceId).FirstOrDefault();

            if (attendanceRow != null)
            {
                attendanceRow.AttendanceStatus = attendance.attendanceStatus;
                _wonder.Attendances.Update(attendanceRow);
                _wonder.SaveChanges();
                return RedirectToAction("DailyAttendance");
            }
            else
            {
                return RedirectToAction("DailyAttendance");
            }
        }

        #endregion

        #region تصنيف المشتركين
        //قائمة المشتركين
        [SessionFilter]
        public ActionResult CategoryList()
        {
            return View();
        }
        public ActionResult getCategories()
        {
            CategoryListViewModel model = new CategoryListViewModel();
            model.categories = _wonder.Categories.ToList();
            return Json(model);
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult CategoryList(Category model)
        {
            if (ModelState.IsValid)
            {
                _wonder.Categories.Add(model);
                _wonder.SaveChanges();
                return RedirectToAction("CategoryList");

            }
            List<Category> model2 = _wonder.Categories.ToList();
            return View(model2);
        }
        public ActionResult DeleteCategory(int id)
        {
            var categoryRow = _wonder.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            if (categoryRow != null)
            {
                _wonder.Categories.Remove(categoryRow);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }

        [HttpPost]
        [SessionFilter]
        public IActionResult UpdateCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                _wonder.Entry(cat).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return RedirectToAction("CategoryList");
            }

        }
        #endregion


        #region المجموعات
        //اضف جديد
        [SessionFilter]
        public ActionResult AddNewGroup()
        {
            AddNewGroupViewModel model = new AddNewGroupViewModel();
            model.categories = _wonder.Categories.ToList();
            model.coaches = _wonder.Coachs.ToList();
            return View(model);
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult AddNewGroup(AddNewGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                _wonder.Groups.Add(model.group);
                _wonder.SaveChanges();
                return RedirectToAction("GroupsMenu");
            }
            else
            {
                model.categories = _wonder.Categories.ToList();
                model.coaches = _wonder.Coachs.ToList();
                return View(model);
            }
        }

        //قائمة المجموعات
        [SessionFilter]
        public ActionResult GroupsMenu()
        {
            GroupListViewModel g = new GroupListViewModel();
            g.coaches = _wonder.Coachs.Select(x => x).ToList();
            return View(g);
        }
        public ActionResult getGroups()
        {

            List<GroupListViewModel> data = new List<GroupListViewModel>();
            var obj = _wonder.Groups.Select(x => x).ToList();
            foreach (var item in obj)
            {
                GroupListViewModel O = new GroupListViewModel();
                O.group = item;
                O.studentsNumber = _wonder.Students.Where(x => x.GroupId == item.GroupId).Count();
                O.time = "From " + _wonder.Groups.Where(x => x.GroupId == item.GroupId).Select(x => x.StartTime).FirstOrDefault() + " To " + _wonder.Groups.Where(x => x.GroupId == item.GroupId).Select(x => x.EndTime).FirstOrDefault();
                O.coachName = _wonder.Coachs.Where(x => x.CoachId == item.CoachId).Select(x => x.FirstName).FirstOrDefault() + "  " + _wonder.Coachs.Where(x => x.CoachId == item.CoachId).Select(x => x.LastName).FirstOrDefault();
                if (item.Saturday == true)
                {
                    O.days += " Saturday ";
                }
                if (item.Sunday == true)
                {
                    O.days += " Sunday ";
                }
                if (item.Monday == true)
                {
                    O.days += " Monday ";
                }
                if (item.Tuesday == true)
                {
                    O.days += " Tuesday ";
                }
                if (item.Wednesday == true)
                {
                    O.days += " Wednesday ";
                }
                if (item.Thursday == true)
                {
                    O.days += " Thursday ";
                }
                if (item.Friday == true)
                {
                    O.days += " Friday ";
                }
                data.Add(O);
            }
            return Json(data);
        }
        public ActionResult DeleteGroup(int id)
        {
            var groupRow = _wonder.Groups.Where(x => x.GroupId == id).FirstOrDefault();

            if (groupRow != null)
            {
                _wonder.Groups.Remove(groupRow);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        [SessionFilter]
        public IActionResult UpdateGroup(int id)
        {

            var group = _wonder.Groups.Where(x => x.GroupId == id).FirstOrDefault();
            ViewBag.coaches = _wonder.Coachs.Select(x => x).ToList();
            ViewBag.categories = _wonder.Categories.Select(x => x).ToList();
            return View(group);
        }
        [HttpPost]
        [SessionFilter]
        public IActionResult UpdateGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                _wonder.Entry(group).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("GroupsMenu");
            }
            else
            {
                return RedirectToAction("UpdateGroup", group.GroupId);
            }

        }

        #endregion


        #region المدربين
        //اضف جديد
        [SessionFilter]
        public ActionResult AddNewCoach()
        {

            return View();
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult AddNewCoach(AddNewCoachViewModel model)
        {
            if (ModelState.IsValid)
            {
                string cvFileName = string.Empty;
                string imageFileName = string.Empty;
                if (model.CV != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString();
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    cvFileName = model.CV.FileName;
                    string fullPath = Path.Combine(uploads, cvFileName + newfileName + Path.GetExtension(cvFileName));
                    model.CV.CopyTo(new FileStream(fullPath, FileMode.Create));
                    cvFileName = cvFileName + newfileName + Path.GetExtension(cvFileName);
                    model.coach.CVFile = cvFileName;
                }
                if (model.Image != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString();
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    imageFileName = model.Image.FileName;
                    string fullPath = Path.Combine(uploads, imageFileName + newfileName + Path.GetExtension(imageFileName));
                    model.Image.CopyTo(new FileStream(fullPath, FileMode.Create));
                    imageFileName = imageFileName + newfileName + Path.GetExtension(imageFileName);
                    model.coach.Image = imageFileName;
                }
                _wonder.Coachs.Add(model.coach);
                _wonder.SaveChanges();
                return RedirectToAction("CoachsMenu");
            }
            else
            {
                return View(model);
            }

        }
        //قائمة المدربين
        [SessionFilter]
        public ActionResult CoachsMenu()
        {
            return View();
        }
        public ActionResult getCoaches()
        {

            var coaches = _wonder.Coachs.ToList();
            foreach (var item in coaches)
            {
                item.FirstName = item.FirstName + " " + item.LastName; //full name not first name
            }

            return Json(coaches);
        }
        public ActionResult DeleteCoach(int CoachId)
        {
            var coachRow = _wonder.Coachs.Where(x => x.CoachId == CoachId).FirstOrDefault();

            if (coachRow != null)
            {
                _wonder.Coachs.Remove(coachRow);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        [SessionFilter]
        public IActionResult UpdateCoach(int id)
        {
            AddNewCoachViewModel viewdata = new AddNewCoachViewModel();
            viewdata.coach = _wonder.Coachs.Where(x => x.CoachId == id).FirstOrDefault();

            return View(viewdata);
        }

        [HttpPost]
        [SessionFilter]
        public IActionResult UpdateCoach(AddNewCoachViewModel coachvm)
        {
            if (ModelState.IsValid)
            {
                if (coachvm.CV != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(coachvm.CV.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    coachvm.CV.CopyTo(new FileStream(path, FileMode.Create));
                    coachvm.coach.CVFile = newfileName;
                }
                if (coachvm.Image != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString() + Path.GetExtension(coachvm.Image.FileName);
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads", newfileName);
                    coachvm.Image.CopyTo(new FileStream(path, FileMode.Create));
                    coachvm.coach.Image = newfileName;
                }
                _wonder.Entry(coachvm.coach).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("CoachsMenu");
            }
            else
            {
                return RedirectToAction("UpdateCoach", coachvm.coach.CoachId);
            }



        }
        #endregion


        #region المصروفات
        [SessionFilter]
        public ActionResult AddExpense()
        {
            return View();
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult AddExpense(Expense expense)
        {
            if (ModelState.IsValid)
            {
                Expense ex = new Expense();
                ex.Amount = expense.Amount;
                ex.Description = expense.Description;
                ex.ResponsibleParty = expense.ResponsibleParty;
                ex.Date = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
                _wonder.Expenses.Add(ex);
                _wonder.SaveChanges();
                return RedirectToAction("ExpenseList");
            }

            return View(expense);
        }
        [SessionFilter]
        public ActionResult UpdateExpense(int id)
        {

            Expense expense = _wonder.Expenses.Where(x => x.ExpenseId == id).SingleOrDefault();
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult UpdateExpense(Expense exp)
        {
            if (ModelState.IsValid)
            {
                _wonder.Entry(exp).State = EntityState.Modified;
                _wonder.SaveChanges();
                Expense expense = _wonder.Expenses.Where(x => x.ExpenseId == exp.ExpenseId).SingleOrDefault();
                expense.Date = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
                _wonder.Expenses.Update(expense);
                _wonder.SaveChanges();
                return RedirectToAction("ExpenseList");

            }
            return View(exp);
        }
        [SessionFilter]
        public ActionResult ExpenseList()
        {

            return View();
        }
        public ActionResult GetExpenseList()
        {

            var exp = _wonder.Expenses.ToList();
            return Json(exp);
        }
        public ActionResult DeleteExpense(int id)
        {
            var exp = _wonder.Expenses.Where(x => x.ExpenseId == id).FirstOrDefault();

            if (exp != null)
            {
                _wonder.Expenses.Remove(exp);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        #endregion


        #region الدفع 
        [SessionFilter]
        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult PaymentData()
        {
            var students = _wonder.Students.ToList();
            List<StudentAndParent> StudentWithPayDate = new List<StudentAndParent>();
            foreach (var item in students)
            {
                StudentAndParent obj = new StudentAndParent();
                obj.student = item;
                obj.StudentName = item.FirstName + " " + item.LastName;
                obj.PaymentDate = _wonder.Payments.OrderByDescending(x => x.PaymentId).Where(x => x.StudentId == item.StudentId).Select(x => x.PaymentDate).FirstOrDefault();
                StudentWithPayDate.Add(obj);
            }
            return Json(StudentWithPayDate);
        }

        public IActionResult HoldStudentPayment(int StudentId)
        {
            var student = _wonder.Students.Where(x => x.StudentId == StudentId).FirstOrDefault();
            student.Status = "hold";
            _wonder.Students.Update(student);
            _wonder.SaveChanges();
            return Json("holded successfully");
        }
        public IActionResult PlayStudentPayment(int StudentId)
        {
            var student = _wonder.Students.Where(x => x.StudentId == StudentId).FirstOrDefault();
            student.Status = "active";
            _wonder.Students.Update(student);
            _wonder.SaveChanges();
            return Json("played successfully");
        }
        [SessionFilter]

        public ActionResult StudentPayment(int id)
        {
            if (id != null)
            {
                StudentPaymentViewModel stpvm = new StudentPaymentViewModel();
                stpvm.studentId = id;
                return View(stpvm);

            }
            return NotFound();
        }
        [HttpPost]
        [SessionFilter]

        public ActionResult StudentPayment(StudentPaymentViewModel stpvm)
        {
            stpvm.payment.PaymentDate = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
            if (ModelState.IsValid)
            {
                stpvm.payment.EndDate = stpvm.payment.StartDate.AddMonths(stpvm.payment.Duration);
                stpvm.payment.StudentId = stpvm.studentId;
                _wonder.Payments.Add(stpvm.payment);
                _wonder.Students.Where(x => x.StudentId == stpvm.studentId).FirstOrDefault().Status = "active";
                _wonder.SaveChanges();
                return RedirectToAction("Payment");

            }
            return View(stpvm);
        }

        public ActionResult StudentPaymentsData(int id)
        {
            var payment = _wonder.Payments.Where(x => x.StudentId == id).ToList();
            return Json(payment);
        }

        public ActionResult DeleteStudentPayment(int id)
        {

            var p = _wonder.Payments.Where(x => x.PaymentId == id).FirstOrDefault();

            if (p != null)
            {
                _wonder.Payments.Remove(p);
                _wonder.SaveChanges();
                return Json("Deleted Done");
            }
            else
            {
                return Json("Error");
            }
        }
        [SessionFilter]

        public ActionResult UpdateStudentPayment(int id)
        {
            var p = _wonder.Payments.Where(x => x.PaymentId == id).FirstOrDefault();
            return View(p);
        }
        [HttpPost]
        [SessionFilter]
        public ActionResult UpdateStudentPayment(Payment p)
        {
            p.PaymentDate = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
            if (ModelState.IsValid)
            {
                p.EndDate = p.StartDate.AddMonths(p.Duration);
                _wonder.Entry(p).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("Payment");

            }
            return View(p);
        }

        #endregion

    }  
}
