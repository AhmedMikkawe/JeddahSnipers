using JeddahSnipers.Models;
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
        public Student student { get; set; }
        public string StudentName { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Group> groups { get; set; }

        public IFormFile StudentNationalIDFile { get; set; }
        public IFormFile StudentApplicationFile { get; set; }
    }
}
