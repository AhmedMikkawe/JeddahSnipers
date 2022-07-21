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
            return View();
        }
        [HttpPost]
        public ActionResult AddNewStudent(StudentAndParent studentAndparent)
        {
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

                stuobj.ParentFirstName = studentAndparent.student.ParentFirstName;
                stuobj.ParentLastName = studentAndparent.student.ParentLastName;
                stuobj.ParentPhone = studentAndparent.student.ParentPhone;
                stuobj.ParentEmergencyPhone = studentAndparent.student.ParentEmergencyPhone;

                _wonder.Students.Add(stuobj);
                _wonder.SaveChanges();

                return RedirectToAction("StudentMenu");

            }
            return View(studentAndparent);

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

            List<Student> data = new List<Student>();
            var obj = _wonder.Students.Select(x => x).ToList();
            foreach (var item in obj)
            {
                //StudentAndParent O = new StudentAndParent();
                //O.student.StudentId = item.StudentId;   
                //O.student.BirthDate = item.BirthDate;
                //O.student.Phone = item.Phone;
                //O.student.Nationality = item.Nationality;
                //O.student.Gender = item.Gender;
                //O.StudentFirstName = item.FirstName + " " + item.LastName; //full name not first name
                //if (item.Category != null)
                //{
                //    O.student.Category.CategoryName = item.Category.CategoryName;
                //}
                //if (item.Group != null)
                //{
                //    O.student.Group.GroupName = item.Group.GroupName;
                //}
                data.Add(item);
            }
            return Json(data);
        }
        //الحضور اليومي
        public ActionResult DailyAttendance()
        {
            return View();
        }
        #endregion


        #region تصنيف المشتركين
        //قائمة المشتركين
        public ActionResult SubscriberList()
        {
            return View();
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
        //قائمة المدربين
        public ActionResult CoachsMenu()
        {
            return View();
        }
        #endregion

    }
}
