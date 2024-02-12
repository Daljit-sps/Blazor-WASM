namespace Shared.ViewModels
{
    public class GetStudentVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DegreeId { get; set; }
        public int RoleId { get; set; }
        public int RollNo { get; set; }

        public string Batch { get; set; } 

        public string Degree { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; } 

        public string Email { get; set; } 

        public string? Password { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Address { get; set; }

        public string FatherName { get; set; } 

        public string MotherName { get; set; } 

        public string? MobileNo { get; set; }


    }
}
