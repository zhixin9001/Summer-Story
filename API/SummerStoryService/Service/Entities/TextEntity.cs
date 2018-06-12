using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entities
{
    public class TextEntity : BaseEntity
    {
        public long RecordID { get; set; }
        public string Content { get; set; }
        public virtual RecordEntity Record { get; set; }
    }
}
