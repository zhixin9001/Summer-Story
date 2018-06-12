using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public interface IUserService : IServiceFlag
    {
        void Add(UserDTO dto);
    }
}
