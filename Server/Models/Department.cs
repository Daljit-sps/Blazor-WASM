using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
