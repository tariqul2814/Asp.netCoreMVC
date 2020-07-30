using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.Model;
using UdemyTutorial.Model.ViewModel;

namespace UdemyTutorial.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Department> Department { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CourseConfig());

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
        }

        public DbSet<UdemyTutorial.Model.ViewModel.RoleVModel> RoleVModel { get; set; }

        public DbSet<UdemyTutorial.Model.ViewModel.UserVModel> UserVModel { get; set; }
    }
}
