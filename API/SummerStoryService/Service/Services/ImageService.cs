﻿using DTO;
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
    public class ImageService : IImageService
    {
        IRepository<ImageEntity> rep;
        public ImageService(/*IRepository<ImageEntity> rep*/)
        {
            var rep = new ImageRepository();
            this.rep = rep;
        }
        public void Add(ImageDTO dto)
        {
            var entity = new ImageEntity
            {
                RecordID = dto.RecordID,
                Sequence=dto.Sequence,
                ImageName = dto.ImageName,
                ThumbNailName = dto.ThumbNailName
            };
            rep.Add(entity);
        }

        public ImageDTO[] GetByRecordID(long recordID)
        {
            var entities = rep.GetAll().Where(a => a.RecordID == recordID).OrderBy(a=>a.Sequence).ToList();
            return entities.Select(a => ToDTO(a)).ToArray();
        }

        private ImageDTO ToDTO(ImageEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("ImageEntity cannot be null");
            }

            var dto = new ImageDTO
            {
                ID = entity.ID,
                CreatedDateTime = entity.CreatedDateTime,
                RecordID = entity.RecordID,
                Sequence=entity.Sequence,
                ImageName = entity.ImageName,
                ThumbNailName = entity.ThumbNailName
            };
            return dto;
        }
    }
}