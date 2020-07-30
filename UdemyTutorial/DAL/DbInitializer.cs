using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.Model;

namespace UdemyTutorial.DAL
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if(!context.Department.Any())
                {
                    context.Department.AddRange(Departments.Values);
                }
                if(!context.Course.Any())
                {
                    var courseList = new List<Course>
                    {
                        new Course() {CourseName = "C#", Credits = 3, Department = departments["Programming"]},
                        new Course() {CourseName = "C++", Credits = 3, Department = departments["Programming"]},
                        new Course() {CourseName = "C", Credits = 3, Department = departments["Programming"]},
                        new Course() {CourseName = "Java", Credits = 3, Department = departments["Programming"]},
                        new Course() {CourseName = "Introduction To Networking", Credits = 3, Department = departments["Network"]},
                        new Course() {CourseName = "Object Oriented Design", Credits = 3, Department = departments["Design"]}
                    };
                    context.Course.AddRange(courseList);
                }

                context.SaveChanges();
            }
        }

        private static Dictionary<string, Department> departments;

        public static Dictionary<string, Department> Departments
        {
            get
            {
                if(departments != null)
                {
                    return departments;
                }

                var depList = new[]
                {
                    new Department(){DepartmentName = "Programming"},
                    new Department(){DepartmentName = "Design"},
                    new Department(){DepartmentName = "Network"}
                };

                departments = new Dictionary<string, Department>();

                foreach(var department in depList)
                {
                    departments.Add(department.DepartmentName, department);
                }

                return departments;
            }
        }
    }
}
