
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JeddahSnipers.ViewModels
{
    public partial class StudentAndParent
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentNationality { get; set; }
        public DateTime StudentBirthDate { get; set; }
        public string StudentPassword { get; set; }
        public string StudentPhone { get; set; }
        public string StudentImage { get; set; }
        public string StudentAddress { get; set; }
        public IFormFile StudentNationalIDFile { get; set; }
        public string StudentApplicationFile { get; set; }
        public string StudentGender { get; set; }
        public byte StudentWeight { get; set; }
        public byte StudentHeight { get; set; }
        public string StudentBloodType { get; set; }
        public string StudentPowerOfSight { get; set; }
        public string StudentAllergicTo { get; set; }
        public string StudentFavoriteFoot { get; set; }
        public bool StudentVitaminDeficiency { get; set; }
        public bool StudentHealthProblems { get; set; }
        public string StudentHealthProblemsDesc { get; set; }
        public string StudentCategory { get; set; }
        public string StudentGroup { get; set; }


        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentPhone { get; set; }
        public string ParentEmergencyPhone { get; set; }
        public string Parentrelation { get; set; }
    }
}
