using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyTutorial.Model.ViewModel
{
    public class CourseVModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
    }
}
