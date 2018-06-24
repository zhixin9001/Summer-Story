using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerStoryService.Models.Responses
{
    public class WxSessionResponse
    {
        public string openid { get; set; }
        public string session_key { get; set; }
    }
}