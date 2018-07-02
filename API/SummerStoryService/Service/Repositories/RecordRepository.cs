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
        public RecordRepository()
        {
            Ctx = new SummerDbContext();
        }

        public long Add(RecordEntity entity)
        {
            Ctx.Records.Add(entity);
            Ctx.SaveChanges();
            return entity.ID;
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RecordEntity> GetAll()
        {
            return Ctx.Records.Where(a => a.IsDeleted == false);
        }

        public RecordEntity GetById(long id)
        {
            return GetAll().FirstOrDefault(a => a.ID == id);
        }

        public void Update(RecordEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
