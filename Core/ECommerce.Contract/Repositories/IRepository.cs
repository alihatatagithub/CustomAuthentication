using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Attach(T t);
        Task Create(List<T> t);
        Task Create(T t);
        Task<List<T>> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<T> FindOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Remove(T t);
        void Remove(List<T> t);
        void Update(List<T> t);
        void Update(T t);
    }
}
