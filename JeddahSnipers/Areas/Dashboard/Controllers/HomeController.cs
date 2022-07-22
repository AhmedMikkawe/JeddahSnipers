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
            return View();
        }
        #endregion


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
        public ActionResult CategoryList(CategoryListViewModel model)
        {
            if (ModelState.IsValid)
            {
                _wonder.Categories.Add(model.category);
                _wonder.SaveChanges();
                return RedirectToAction("CategoryList");

            }
            model.categories = _wonder.Categories.ToList();
            return View(model);
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
            return View();
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
        #endregion

    }
}
