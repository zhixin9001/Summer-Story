using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ImageDTO
    {
        public long ID { get; set; }
        public long RecordID { get; set; }
        public string ImageName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
