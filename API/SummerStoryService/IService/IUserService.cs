using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public interface IUserService : IServiceFlag
    {
        long Add(UserDTO dto);

        UserDTO GetByWxID(string WxID);
    }
}
