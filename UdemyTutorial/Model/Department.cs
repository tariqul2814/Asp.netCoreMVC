using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyTutorial.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
