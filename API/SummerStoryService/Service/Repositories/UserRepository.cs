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

        public long Add(UserEntity entity)
        {
            Ctx.Users.Add(entity);
            Ctx.SaveChangesAsync();
            return entity.ID;
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserEntity> GetAll()
        {
            throw new NotImplementedException();
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
