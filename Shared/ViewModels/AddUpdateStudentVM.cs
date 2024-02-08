using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels
{
    public class AddUpdateStudentVM
    {

        [Required]

        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;


        public string? Password { get; set; }

        public string? MobileNo { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DOB { get; set; }


        [Required]

        public string FatherName { get; set; } = null!;

        [Required]
        public string MotherName { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int DegreeId { get; set; }

        [Required]
        public int RollNo { get; set; }

        [Required]
        public string Batch { get; set; } = null!;

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        public string Degree {  get; set; }

    }
}
