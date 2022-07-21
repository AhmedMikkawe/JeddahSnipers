using JeddahSnipers.Models;
using System.Collections.Generic;

namespace JeddahSnipers.ViewModels
{
    public class AddNewGroupViewModel
    {
        public Group group { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Coach> coaches { get; set; }

    }
}
