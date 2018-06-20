using DTO;
using IService;
using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TextService : ITextService
    {
        IRepository<TextEntity> rep;
        public TextService(/*IRepository<TextEntity> rep*/)
        {
            var rep = new TextRepository();
            this.rep = rep;
        }
        public void Add(TextDTO dto)
        {
            var entity = new TextEntity
            {
                RecordID = dto.RecordID,
                Content = dto.Content
            };
            rep.Add(entity);
        }

        public TextDTO GetByRecordID(long recordID)
        {
            var entity = rep.GetAll().Where(a => a.RecordID == recordID).FirstOrDefault();
            return ToDTO(entity);
        }

        private TextDTO ToDTO(TextEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("TextEntity cannot be null");
            }

            var dto = new TextDTO
            {
                ID = entity.ID,
                CreatedDateTime = entity.CreatedDateTime,
                RecordID = entity.RecordID,
                Content = entity.Content
            };
            return dto;
        }
    }
}
