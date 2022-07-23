namespace JeddahSnipers.ViewModels
{
    public class LastStudentsViewModel
    {
        //x.StudentId, x.Category.CategoryName, x.FirstName , x.LastName, x.Nationality, x.Group.Age
        public int? StudentId { get; set; }
        public string? CategoryName { get; set; }
        public string? StudentFirstName { get; set; }
        public string? StudentLastName { get; set; }
        public string? StudentNationality { get; set; }
        public int? StudentAge { get; set; }

    }
}
