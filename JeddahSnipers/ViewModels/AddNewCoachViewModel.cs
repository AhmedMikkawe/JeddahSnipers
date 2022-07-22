using JeddahSnipers.Models;
using Microsoft.AspNetCore.Http;

namespace JeddahSnipers.ViewModels
{
    public class AddNewCoachViewModel
    {
        public Coach coach { get; set; }
        public IFormFile CV { get; set; }
        public IFormFile Image { get; set; }
    }
}
