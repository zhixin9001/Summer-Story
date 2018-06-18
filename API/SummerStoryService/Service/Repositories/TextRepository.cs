using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class TextRepository : IRepository<TextEntity>
    {
        public SummerDbContext Ctx { get; }
        public TextRepository()
        {
            Ctx = new SummerDbContext();
        }

        public TextEntity GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TextEntity> GetAll()
        {
            return Ctx.Texts.Where(a => a.IsDeleted == false);
        }

        public long Add(TextEntity entity)
        {
            Ctx.Texts.Add(entity);
            Ctx.SaveChangesAsync();
            return entity.ID;
        }

        public void Update(TextEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
