using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entities
{
    public class RecordEntity : BaseEntity
    {
        public long UserID { get; set; }
        public virtual ICollection<TextEntity> Texts {get;set;}
        public virtual ICollection<ImageEntity> Images { get; set; }
    }
}
