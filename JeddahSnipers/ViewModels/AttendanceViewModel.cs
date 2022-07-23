using JeddahSnipers.Models;
using System;
using System.Collections.Generic;

namespace JeddahSnipers.ViewModels
{
    public class AttendanceViewModel
    {
        public IEnumerable<Student> students { get; set; }
        public int studentId { get; set; }
        public string StudentName { get; set; }
        public string attendanceDate { get; set; }
        public bool attendanceStatus { get; set; }
        public Attendance attendance { get; set; }
        public int attendanceId { get; set; }
        public IEnumerable<Attendance> attendances { get; set; }
    }
}
