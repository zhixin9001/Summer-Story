using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerStoryService.Models.Requests
{
    public class AddRecordRequest
    {
        public string Content { get; set; }
        public float? Latitude { get; set; }  //纬度 x
        public float? Longitude { get; set; } //经度 y
    }
}