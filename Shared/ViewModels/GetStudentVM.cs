﻿namespace Shared.ViewModels
{
    public class GetStudentVM
    {

        public int UserId { get; set; }

        public int RollNo { get; set; }

        public string Batch { get; set; } = null!;

        public string Degree { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly? Dob { get; set; }

        public string? Address { get; set; }

        public string FatherName { get; set; } = null!;

        public string MotherName { get; set; } = null!;

        public string? MobileNo { get; set; }


    }
}