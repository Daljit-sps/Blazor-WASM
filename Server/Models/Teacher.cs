using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DeptId { get; set; }

    public string Designation { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
