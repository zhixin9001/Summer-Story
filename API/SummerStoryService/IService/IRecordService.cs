using DTO;
using System;

namespace IService
{
    public interface IRecordService : IServiceFlag
    {
        long Add(RecordDTO dto);
        RecordDTO[] GetPagedData(long userID, int startIndex, int pageSize);
        void MarkRecordEnable(long recordID);
    }
}
