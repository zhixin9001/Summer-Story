using DTO;
using IService;
using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class RecordService : IRecordService
    {
        IRepository<RecordEntity> rep;
        public RecordService(IRepository<RecordEntity> rep)
        {
            this.rep = rep;
        }
        public void Add(RecordDTO dto)
        {
            var entity = new RecordEntity
            {

            };
            rep.Add(entity);
        }

        public RecordDTO[] GetPagedData(long userID, int startIndex, int pageSize)
        {
            var entities = rep.GetAll()
                .Where(a => a.UserID == userID)
                .OrderByDescending(a => a.CreatedDateTime)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
            return entities
        }

        private RecordDTO ToDTO(RecordEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("RecordEntity cannot be null");
            }

            var dto = new RecordDTO
            {
                ID = entity.ID,
                CreatedDateTime = entity.CreatedDateTime,
            };
            if(entity.Images)
            return dto;
        }
    }
}
