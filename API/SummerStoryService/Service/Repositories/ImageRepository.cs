using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class ImageRepository : IRepository<ImageEntity>
    {
        public SummerDbContext Ctx { get; }
        public ImageRepository()
        {
            Ctx = new SummerDbContext();
        }

        public ImageEntity GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImageEntity> GetAll()
        {
            return Ctx.Images.Where(a=>a.IsDeleted==false);
        }

        public long Add(ImageEntity entity)
        {
            Ctx.Images.Add(entity);
            Ctx.SaveChanges();
            return entity.ID;
        }

        public void Update(ImageEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
