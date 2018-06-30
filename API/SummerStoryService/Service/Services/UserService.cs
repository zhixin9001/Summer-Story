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
    public class UserService : IUserService
    {
        IRepository<UserEntity> rep;
        public UserService(/*IRepository<UserEntity> rep*/)
        {
            //this.rep = rep;
            this.rep = new UserRepository();
        }
        public long Add(UserDTO dto)
        {
            var entity = new UserEntity
            {
                WxID = dto.WxID
            };
            return rep.Add(entity);
        }

        public UserDTO GetByWxID(string WxID)
        {
            var user = rep.GetAll()
                .Where(a => a.WxID == WxID)
                .FirstOrDefault();
            return ToDTO(user);
        }

        private UserDTO ToDTO(UserEntity entity)
        {
            if (entity == null)
            {
                //throw new ArgumentException("UserEntity cannot be null");
                return null;
            }

            var dto = new UserDTO
            {
                ID = entity.ID,
                CreatedDateTime = entity.CreatedDateTime,
                WxID = entity.WxID
            };
            return dto;
        }
    }
}
