using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.DAL;
using UdemyTutorial.Model;
using UdemyTutorial.Services.IRepository;

namespace UdemyTutorial.Services.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {}
    }
}
