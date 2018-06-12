using System;

namespace DTO
{
    public class RecordDTO
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long[] TextIDs { get; set; }
        public long[] ImageIDs { get; set; }
    }
}
