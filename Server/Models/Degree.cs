using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Degree
{
    public int Id { get; set; }

    public string DegreeName { get; set; } = null!;

    public decimal Duration { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
