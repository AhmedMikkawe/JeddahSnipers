using JeddahSnipers.Models;
using System.Collections.Generic;

namespace JeddahSnipers.ViewModels
{
    public class CategoryListViewModel
    {
        public Category category { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
