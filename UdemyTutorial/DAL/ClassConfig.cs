using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.Model;

namespace UdemyTutorial.DAL
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(k => k.CourseId);
            builder.Property(p => p.CourseId).ValueGeneratedOnAdd();
            builder.Property(p => p.Credits).IsRequired();

            builder.HasOne(p => p.Department)
                .WithMany(c => c.Courses)
                .HasForeignKey(p => p.DepartmentId);
        }

    }

    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(k => k.DepartmentId);
            builder.Property(p => p.DepartmentId).ValueGeneratedOnAdd();
            builder.Property(p => p.DepartmentName).IsRequired().HasColumnType("Nvarchar(50)");
            builder.Property(p => p.Budget).IsRequired();

            //builder.HasMany(p => p.Courses)
            //    .WithOne(p => p.Department)
            //    .HasForeignKey(p => p.DepartmentId);
        }
    }
}
