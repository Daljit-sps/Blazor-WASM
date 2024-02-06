using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class DegreeVM
    {
        public int Id { get; set; }

        public string DegreeName { get; set; } = null!;

        public decimal Duration { get; set; }

        public int DepartmentId { get; set; }
    }
}
