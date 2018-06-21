using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerStoryService.Models.Responses
{
    public class RecordResponse
    {
        public long RecordID { get; set; }
        public string Content { get; set; }
        public List<ImageModel> Images { get; set; }
    }

    public class ImageModel
    {
        public string ImageURL { get; set; }
        public string ThumbnailURL { get; set; }

    }
}