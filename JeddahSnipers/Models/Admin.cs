using System.ComponentModel.DataAnnotations;

namespace JeddahSnipers.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
