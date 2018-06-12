using DTO;
using IService;
using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
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

        public RecordDTO[] GetPagedData(int startIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
