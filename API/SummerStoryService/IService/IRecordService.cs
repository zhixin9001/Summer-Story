using DTO;
using System;

namespace IService
{
    public interface IRecordService : IServiceFlag
    {
        void Add(RecordDTO dto);
        RecordDTO[] GetPagedData(long userID, int startIndex, int pageSize);
    }
}
