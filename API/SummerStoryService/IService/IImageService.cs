using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IImageService : IServiceFlag
    {
        ImageDTO GetByRecordID(long recordID);
        void Add(ImageDTO dto);
    }
}
