using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using UdemyTutorial.DAL;
using UdemyTutorial.Model;
using UdemyTutorial.Model.ViewModel;
using UdemyTutorial.Services.IRepository;

namespace UdemyTutorial.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository course;
        private readonly IDepartmentRepository department;
        public CourseController(ICourseRepository course, IDepartmentRepository department)
        {
            this.course = course;
            this.department = department;
        }

        [HttpGet]
        public IActionResult Index(int pageindex = 1)
        {
            var CoureseDept = course.CoursesToDepartment();
            var model = PagingList.Create(CoureseDept, 5, pageindex);
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateCourse()
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            keyValuePairs.Add(0, " --- Select Department --- ");
            var Dept = department.GetAll().ToList();

            Dept.ForEach(
                    x =>
                    {
                        keyValuePairs.Add(x.DepartmentId, x.DepartmentName);
                    }
                );

            ViewBag.DepartmentId = new SelectList(keyValuePairs, "Key", "Value");
            return View();
        }

        [HttpPost, ActionName("CreateCourse")]
        public IActionResult CreateCourse(Course model)
        {
            if(ModelState.IsValid)
            {
                course.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateCourse");
            }
        }

        [HttpGet]
        public IActionResult UpdateCourse(int Id)
        {
            var Course = course.GetById(Id);
            if(Course!=null)
            {
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
                keyValuePairs.Add(0, " --- Select Department --- ");
                var Dept = department.GetAll().ToList();

                Dept.ForEach(
                        x =>
                        {
                            keyValuePairs.Add(x.DepartmentId, x.DepartmentName);
                        }
                    );

                ViewBag.DepartmentId = new SelectList(keyValuePairs, "Key", "Value");
                return View(Course);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult UpdateCourse(Course model)
        {
            var Course = course.GetById(model.CourseId);

            if(Course!=null)
            {
                course.Update(Course);
            }
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public IActionResult DeleteCourse(int CourseId)
        {
            var Course = course.GetById(CourseId);

            if(Course!=null)
            {
                course.Delete(Course);
            }
            return RedirectToAction("Index","Course");
        }
    }
}