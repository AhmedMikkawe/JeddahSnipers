using System;

namespace JeddahSnipers.ViewModels
{
    public class LastGroupsViewModel
    {
        //x.GroupId, x.GroupName, x.Student.ToList().Count, x.StartTime, x.EndTime, x.Coach.FirstName, x.Coach.LastName
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? StudentsCount { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? CoachFirstName { get; set; }
        public string? CoachLastName { get; set; }

    }
}
