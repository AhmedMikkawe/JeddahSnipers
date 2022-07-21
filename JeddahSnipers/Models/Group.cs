using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JeddahSnipers.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Range(4,80,ErrorMessage ="This age is out of range")]
        public byte Age { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public Coach Coach { get; set; }
        public int? CoachId { get; set; }
        public List<Student> Student { get; set; }
    }
}
