using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerStoryService.Models.Requests
{
    public class AddImageRequest
    {
        public long RecordID { get; set; }
        public int Sequence { get; set; }
    }
}