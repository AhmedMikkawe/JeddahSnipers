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
        public IActionResult Index()
        {
            return View();
        }


        #region الطلاب
        //اضف جديد
        public ActionResult AddNewStudent()
        {
            StudentAndParent model = new StudentAndParent();
            model.categories = _wonder.Categories.ToList();
            model.groups = _wonder.Groups.ToList();
            return View(model);
            
        }
        [HttpPost]
        public ActionResult AddNewStudent(StudentAndParent studentAndparent)
        {
            /*
            if (ModelState.IsValid)
            {
                Student stuobj = new Student();

                string stdNIdFileName = string.Empty;
                if (studentAndparent.StudentNationalIDFile != null)
                {
                    Guid guid = Guid.NewGuid();

                    string newfileName = guid.ToString();


                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    stdNIdFileName = studentAndparent.StudentNationalIDFile.FileName;
                    string fullPath = Path.Combine(uploads, stdNIdFileName + newfileName + Path.GetExtension(stdNIdFileName));
                    studentAndparent.StudentNationalIDFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    stdNIdFileName = stdNIdFileName + newfileName + Path.GetExtension(stdNIdFileName);
                }

                stuobj.FirstName = studentAndparent.student.FirstName;
                stuobj.LastName = studentAndparent.student.LastName;
                stuobj.BirthDate = studentAndparent.student.BirthDate;
                stuobj.Password = studentAndparent.student.Password;
                stuobj.Phone = studentAndparent.student.Phone;
                stuobj.Address = studentAndparent.student.Address;
                stuobj.NationalIDFile = stdNIdFileName;
                stuobj.ApplicationFile = studentAndparent.student.ApplicationFile;
                stuobj.Gender = studentAndparent.student.Gender;
                stuobj.Weight = studentAndparent.student.Weight;
                stuobj.Height = studentAndparent.student.Height;
                stuobj.BloodType = studentAndparent.student.BloodType;
                stuobj.PowerOfSight = studentAndparent.student.PowerOfSight;
                stuobj.AllergicTo = studentAndparent.student.AllergicTo;
                stuobj.FavoriteFoot = studentAndparent.student.FavoriteFoot;
                stuobj.VitaminDeficiency = studentAndparent.student.VitaminDeficiency;
                stuobj.HealthProblems = studentAndparent.student.HealthProblems;
                stuobj.HealthProblemsDesc = studentAndparent.student.HealthProblemsDesc;
                stuobj.Nationality = studentAndparent.student.Nationality;
                stuobj.ParentRelation = studentAndparent.student.ParentRelation;
                stuobj.Category = studentAndparent.groups;
                //stuobj.Group.GroupId= _wonder.Groups.Where(x => x.GroupId == studentAndparent.Group.GroupId).Select(x => x.GroupId).FirstOrDefault(); 

                stuobj.ParentFirstName = studentAndparent.student.ParentFirstName;
                stuobj.ParentLastName = studentAndparent.student.ParentLastName;
                stuobj.ParentPhone = studentAndparent.student.ParentPhone;
                stuobj.ParentEmergencyPhone = studentAndparent.student.ParentEmergencyPhone;

                _wonder.Students.Add(stuobj);
                _wonder.SaveChanges();

                return RedirectToAction("StudentMenu");

            }
            return View(studentAndparent);
            */
            if (ModelState.IsValid)
            {
                string stdNIdFileName = string.Empty;
                string stdAppFileName = string.Empty;
                string imageFileName = string.Empty;
                if (studentAndparent.StudentNationalIDFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString();
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    stdNIdFileName = studentAndparent.StudentNationalIDFile.FileName;
                    string fullPath = Path.Combine(uploads, stdNIdFileName + newfileName + Path.GetExtension(stdNIdFileName));
                    studentAndparent.StudentNationalIDFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    stdNIdFileName = stdNIdFileName + newfileName + Path.GetExtension(stdNIdFileName);
                    studentAndparent.student.NationalIDFile = stdNIdFileName;
                }
                if (studentAndparent.StudentApplicationFile != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString();
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    stdAppFileName = studentAndparent.StudentApplicationFile.FileName;
                    string fullPath = Path.Combine(uploads, stdAppFileName + newfileName + Path.GetExtension(stdAppFileName));
                    studentAndparent.StudentNationalIDFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    stdAppFileName = stdAppFileName + newfileName + Path.GetExtension(stdAppFileName);
                    studentAndparent.student.ApplicationFile = stdAppFileName;
                }
                if (studentAndparent.studentImage != null)
                {
                    Guid guid = Guid.NewGuid();
                    string newfileName = guid.ToString();
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    imageFileName = studentAndparent.studentImage.FileName;
                    string fullPath = Path.Combine(uploads, imageFileName + newfileName + Path.GetExtension(imageFileName));
                    studentAndparent.studentImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                    imageFileName = imageFileName + newfileName + Path.GetExtension(imageFileName);
                    studentAndparent.student.Image = imageFileName;
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
            //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            //if (Photo_1 != null)
            //{
            //    string fileName = Photo_1.FileName;

            //    var fileNameWithPath = Path.Combine(filePath, fileName);

            //    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            //    {
            //        Photo_1.CopyTo(stream);
            //    }
            //    _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = obj.CaseCode });

            //    _wonder.SaveChanges();
            //}

        }

        //قائمة الطلاب
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
        public IActionResult UpdateStudent(int id)
        {
            var student = _wonder.Students.Where(x => x.StudentId == id).FirstOrDefault();
            ViewBag.Categories = _wonder.Categories.ToList();
            ViewBag.Groups = _wonder.Groups.ToList();
            return View(student);
        }

        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                //Student Edit = _wonder.Students.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                _wonder.Entry(student).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("StudentMenu");
            }
            else
            {
                return RedirectToAction("UpdateStudent", student.StudentId);
            }
            
            

        }

        //الحضور اليومي
        public ActionResult DailyAttendance()
        {
            AttendanceViewModel model = new AttendanceViewModel();
            model.students = _wonder.Students.ToList();
            model.attendanceDate =  DateTime.UtcNow.ToString("D");
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAttendance(AttendanceViewModel attendanceViewModel)
        {
            if (ModelState.IsValid)
            {
                Attendance attendance = new Attendance();
                attendance.AttendanceDate = Convert.ToDateTime(DateTime.UtcNow.ToString("D"));
                attendance.AttendanceStatus = attendanceViewModel.attendanceStatus;
                attendance.StudentId = attendanceViewModel.studentId;
                var registeredAttendance = _wonder.Attendances.Where(x=>x.AttendanceDate == attendance.AttendanceDate && x.StudentId == attendance.StudentId).ToList().Count;
                if(registeredAttendance != 0)
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
        public ActionResult getAttendances()
        {
            List<AttendanceViewModel> att = new List<AttendanceViewModel>();
            var attendances = _wonder.Attendances.ToList();
            foreach (var item in attendances)
            {
                AttendanceViewModel obj = new AttendanceViewModel();
                obj.StudentName =_wonder.Students.Where(x => x.StudentId == item.StudentId).Select(x=>x.FirstName).FirstOrDefault()+" "+ _wonder.Students.Where(x => x.StudentId == item.StudentId).Select(x => x.LastName).FirstOrDefault();
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

        #region تصنيف المشتركين
        //قائمة المشتركين
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
        public ActionResult AddNewGroup()
        {
            AddNewGroupViewModel model = new AddNewGroupViewModel();
            model.categories = _wonder.Categories.ToList();
            model.coaches = _wonder.Coachs.ToList();
            return View(model);
        }
        [HttpPost]
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
        public ActionResult GroupsMenu()
        {
            GroupListViewModel g = new GroupListViewModel();
            g.coaches= _wonder.Coachs.Select(x => x).ToList();
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
        public IActionResult UpdateGroup(int id)
        {
            var group = _wonder.Groups.Where(x => x.GroupId == id).FirstOrDefault();
            ViewBag.coaches = _wonder.Coachs.Select(x => x).ToList();
            ViewBag.categories = _wonder.Categories.Select(x => x).ToList();
            return View(group);
        }
        [HttpPost]
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
        public ActionResult AddNewCoach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewCoach(AddNewCoachViewModel model) {
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
                if (model.Image != null) {
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
        public IActionResult UpdateCoach(int id)
        {
            var coach = _wonder.Coachs.Where(x => x.CoachId == id).FirstOrDefault();
            return View(coach);
        }

        [HttpPost]
        public IActionResult UpdateCoach(Coach coach)
        {
            if (ModelState.IsValid)
            {
                _wonder.Entry(coach).State = EntityState.Modified;
                _wonder.SaveChanges();
                return RedirectToAction("CoachsMenu");
            }
            else
            {
                return RedirectToAction("UpdateCoach", coach.CoachId);
            }



        }
        #endregion

    }
}
