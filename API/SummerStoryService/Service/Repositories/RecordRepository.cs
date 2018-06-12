using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Repositories
{
    public class RecordRepository : IRepository<RecordEntity>
    {
        public SummerDbContext Ctx { get; }

        public long Add(RecordEntity entity)
        {
            Ctx.Records.Add(entity);
            Ctx.SaveChangesAsync();
            return entity.ID;
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RecordEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public RecordEntity GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(RecordEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
