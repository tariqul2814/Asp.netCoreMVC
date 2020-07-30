using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyTutorial.DAL;
using UdemyTutorial.Services.IRepository;

namespace UdemyTutorial.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private void Save() => context.SaveChanges();

        public void Add(T entity)
        {
            context.Add(entity);
            Save();
        }

        public int Count(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate).Count();
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public IEnumerable<T> GetByFiler(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
    }
}
