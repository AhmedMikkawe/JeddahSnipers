using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeddahSnipers.Models;


namespace JeddahSnipers.ViewModels
{
    public class GroupListViewModel
    {
        public int studentsNumber { get; set; }
        public string days { get; set; }
        public string time { get; set; }
        public string coachName { get; set; }
        public Group group { get; set; }
        public Coach coach { get; set; }
        public IEnumerable<Group> groups { get; set; }
        public IEnumerable<Coach> coaches { get; set; }
    }
}
