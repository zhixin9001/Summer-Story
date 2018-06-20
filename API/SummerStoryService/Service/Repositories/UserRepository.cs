using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Repositories
{
    public class UserRepository : IRepository<UserEntity>
    {
        public SummerDbContext Ctx { get; }
        public UserRepository()
        {
            Ctx = new SummerDbContext();
        }

        public long Add(UserEntity entity)
        {
            Ctx.Users.Add(entity);
            Ctx.SaveChanges();
            return entity.ID;
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserEntity> GetAll()
        {
            return Ctx.Users.Where(u => u.IsDeleted == false);
        }

        public UserEntity GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
