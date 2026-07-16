using ECommerce.Contract.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Presistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        protected AppDbContext AppDbContext => _context;
        public async Task Create(T t)
        {
            await AppDbContext.Set<T>().AddAsync(t);
        }
        public Task Create(List<T> t)
        {
            return AppDbContext.Set<T>().AddRangeAsync(t);
        }
        public void Update(T t)
        {
            AppDbContext.Set<T>().Update(t);
        }
        public void Update(List<T> t)
        {
            AppDbContext.Set<T>().UpdateRange(t);
        }
        public void Remove(T t)
        {
            AppDbContext.Set<T>().Remove(t);
        }
        public void Remove(List<T> t)
        {
            AppDbContext.Set<T>().RemoveRange(t);
        }
        public Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return AppDbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return AppDbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public void Attach(T t) => AppDbContext.Set<T>().Attach(t);
    }
}
