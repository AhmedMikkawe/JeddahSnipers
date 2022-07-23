using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeddahSnipers.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="يجب إدخال قيمة المصروف")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="يجب إدخال الوصف",AllowEmptyStrings =false)]
        public string Description { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="يجب إدخال جهة الصرف")]
        public string ResponsibleParty { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
    }
}
