using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entities
{
    public class BaseEntity
    {
        public long ID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
