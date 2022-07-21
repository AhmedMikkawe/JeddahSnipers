using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JeddahSnipers.Models
{
    public class Coach
    {
        [Key]
        public int CoachId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string CVFile { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }

        public List<Group> Group { get; set; }


    }
}
