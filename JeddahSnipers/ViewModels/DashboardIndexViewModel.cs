using JeddahSnipers.Models;
using System.Collections.Generic;
using System.Linq;

namespace JeddahSnipers.ViewModels
{
    public class DashboardIndexViewModel
    {
        public int? studentsCount { get; set; }
        public int? coachesCount { get; set; }
        public int? groupsCount { get; set; }
        public IQueryable<LastStudentsViewModel> LastStudents { get; set; }
        public IQueryable<LastGroupsViewModel> LastGroups { get; set; }

    }
}
