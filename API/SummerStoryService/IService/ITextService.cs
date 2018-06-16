using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface ITextService:IServiceFlag
    {
        TextDTO GetByRecordID(long recordID);
        void Add(TextDTO dto);
    }
}
