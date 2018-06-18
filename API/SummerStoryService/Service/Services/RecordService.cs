using DTO;
using IService;
using Service.Entities;
using Service.Repositories;
using System;
using System.Linq;

namespace Service.Services
{
    public class RecordService : IRecordService
    {
        IRepository<RecordEntity> rep;
        public RecordService(/*IRepository<RecordEntity> rep*/)
        {
            var rep = new RecordRepository();
            this.rep = rep;
        }
        public void Add(RecordDTO dto)
        {
            var entity = new RecordEntity
            {
                UserID=dto.UserID
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
            return entities.Select(a => ToDTO(a)).ToArray();
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
            if (entity.Images != null)
            {
                dto.ImageIDs = entity.Images.Select(a => a.ID).ToArray();
            }
            if (entity.Texts != null)
            {
                dto.TextIDs = entity.Texts.Select(a => a.ID).ToArray();
            }
            return dto;
        }
    }
}
