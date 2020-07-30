using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.DAL;
using UdemyTutorial.Model;
using UdemyTutorial.Services.IRepository;

namespace UdemyTutorial.Services.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {}

        public IEnumerable<Course> CoursesToDepartment()
        {
            return context.Course.Include(x => x.Department).ToList();
        }
    }
}
