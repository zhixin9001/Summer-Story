using DTO;
using IService;
using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
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
        public void Add(UserDTO dto)
        {
            var entity = new UserEntity
            {
                WxID ="111wer"// dto.WxID
            };
            rep.Add(entity);
        }
    }
}
