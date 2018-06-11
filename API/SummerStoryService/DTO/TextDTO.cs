using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TextDTO
    {
        public long ID { get; set; }
        public long RecordID { get; set; }
        public string Content { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
