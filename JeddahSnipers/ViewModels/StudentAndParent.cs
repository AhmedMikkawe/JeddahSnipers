
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
        public string StudentFirstName { get; set; }
        public IFormFile StudentNationalIDFile { get; set; }
    }
}
