using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Repositories
{
    public interface IRepository<T> where T : class
    {
        SummerDbContext Ctx { get; }

        T GetById(long id);

        IQueryable<T> GetAll();

        long Add(T entity);

        void Update(T entity);

        void DeleteById(long id);
    }
}
