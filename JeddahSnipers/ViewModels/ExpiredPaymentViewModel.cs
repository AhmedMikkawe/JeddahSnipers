using System;

namespace JeddahSnipers.ViewModels
{
    public class ExpiredPaymentViewModel
    {
        public string StudentName { get; set; }
        public int Duration { get; set; }
        public DateTime EndDate { get; set; }
        public float Amount { get; set; }
    }
}
