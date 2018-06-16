using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entities
{
    public class ImageEntity : BaseEntity
    {
        public long RecordID { get; set; }
        public string ImageName { get; set; }
        public string ThumbNailName { get; set; }
        public virtual RecordEntity Record { get; set; }
    }
}
