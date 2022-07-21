using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JeddahSnipers.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="This Field Can't be less than 2 characters")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field Can't be less than 2 characters")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field Can't be less than 8 characters")]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string NationalIDFile { get; set; }
        public string ApplicationFile { get; set; }
        public string Gender { get; set; }
        public byte? Weight { get; set; }
        public byte? Height { get; set; }
        public string BloodType { get; set; }
        public string PowerOfSight { get; set; }
        public string AllergicTo { get; set; }
        public string FavoriteFoot { get; set; }
        public bool? VitaminDeficiency { get; set; }
        public bool? HealthProblems { get; set; }
        public string HealthProblemsDesc { get; set; }
        public string ParentRelation { get; set; }
        public string Nationality { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        [Phone]
        public string ParentPhone { get; set; }
        [Phone]
        public string ParentEmergencyPhone { get; set; }
        public Category Category { get; set; }
        public Group Group { get; set; }
        public List<Attendance> Attendance { get; set; }
    }
}
