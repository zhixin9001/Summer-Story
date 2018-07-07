﻿using DTO;
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
        public long Add(RecordDTO dto)
        {
            var entity = new RecordEntity
            {
                UserID = dto.UserID,
                LocationName=dto.LocationName,
                Longitude=dto.Longitude,
                Latitude=dto.Latitude,
                IsDeleted=dto.IsDeleted
            };
            return rep.Add(entity);
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

        public void MarkRecordEnable(long recordID)
        {
            var record = rep.Ctx.Records.FirstOrDefault(a => a.ID == recordID);
            if (record != null)
            {
                record.IsDeleted = false;
                rep.Ctx.SaveChanges();
            }
            else
            {
                throw new ArgumentException(string.Format("Record {0} cannot be found", recordID));
            }
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
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
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
