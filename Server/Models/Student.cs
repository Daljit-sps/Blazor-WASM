using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Student
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DegreeId { get; set; }

    public int RollNo { get; set; }

    public string Batch { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Degree Degree { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
