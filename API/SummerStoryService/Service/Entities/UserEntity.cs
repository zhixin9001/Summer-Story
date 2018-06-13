using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entities
{
    public class UserEntity : BaseEntity
    {
        public string WxID { get; set; }
        public virtual ICollection<RecordEntity> Records { get; set; }
    }
}
