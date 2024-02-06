using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Address { get; set; }

    public string FatherName { get; set; } = null!;

    public string MotherName { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public string? MobileNo { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
