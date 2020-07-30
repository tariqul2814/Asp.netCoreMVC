using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.Model;

namespace UdemyTutorial.Services.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> CoursesToDepartment();
    }
}
