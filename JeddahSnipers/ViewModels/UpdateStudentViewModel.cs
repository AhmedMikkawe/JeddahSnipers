using JeddahSnipers.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace JeddahSnipers.ViewModels
{
    public class UpdateStudentViewModel
    {
        public Student student { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Group> groups { get; set; }

        public IFormFile NationalIDFile { get; set; }
        public IFormFile studentImage { get; set; }
        public Category category { get; set; }
        public Group group { get; set; }
        public IFormFile ApplicationFile { get; set; }

    }
}
