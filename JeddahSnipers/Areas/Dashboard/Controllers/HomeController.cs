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

            stuobj.FirstName = studentAndparent.StudentFirstName;
            stuobj.LastName = studentAndparent.StudentLastName;
            stuobj.BirthDate = studentAndparent.StudentBirthDate;
            stuobj.Password = studentAndparent.StudentPassword;
            stuobj.Phone = studentAndparent.StudentPhone;
            stuobj.Address = studentAndparent.StudentAddress;
            stuobj.NationalIDFile = stdNIdFileName;
            stuobj.ApplicationFile = studentAndparent.StudentApplicationFile;
            stuobj.Gender = studentAndparent.StudentGender;
            stuobj.Weight = studentAndparent.StudentWeight;
            stuobj.Height = studentAndparent.StudentHeight;
            stuobj.BloodType = studentAndparent.StudentBloodType;
            stuobj.PowerOfSight = studentAndparent.StudentPowerOfSight;
            stuobj.AllergicTo = studentAndparent.StudentAllergicTo;
            stuobj.FavoriteFoot = studentAndparent.StudentFavoriteFoot;
            stuobj.VitaminDeficiency = studentAndparent.StudentVitaminDeficiency;
            stuobj.HealthProblems = studentAndparent.StudentHealthProblems;
            stuobj.HealthProblemsDesc = studentAndparent.StudentHealthProblemsDesc;
            stuobj.Nationality = studentAndparent.StudentNationality;
            stuobj.ParentRelation = studentAndparent.Parentrelation;

            stuobj.ParentFirstName = studentAndparent.ParentFirstName;
            stuobj.ParentLastName = studentAndparent.ParentLastName;
            stuobj.ParentPhone = studentAndparent.ParentPhone;
            stuobj.ParentEmergencyPhone = studentAndparent.ParentEmergencyPhone;

            _wonder.Students.Add(stuobj);
            _wonder.SaveChanges();

            return RedirectToAction("StudentMenu");


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
                O.StudentId = item.StudentId;
                O.StudentBirthDate = item.BirthDate;
                O.StudentPhone = item.Phone;
                O.StudentNationality = item.Nationality;
                O.StudentGender = item.Gender;
                O.StudentFirstName = item.FirstName + " " + item.LastName; //full name not first name
                if (item.Category != null)
                {
                    O.StudentCategory = item.Category.CategoryName;
                }
                if (item.Group != null)
                {
                    O.StudentGroup = item.Group.GroupName;
                }
                data.Add(O);
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
            return View();
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
