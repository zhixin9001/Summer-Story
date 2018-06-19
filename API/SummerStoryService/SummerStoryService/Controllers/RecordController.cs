using Common;
using DTO;
using IService;
using Service.Repositories;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SummerStoryService.Controllers
{
    public class RecordController : ApiController
    {
        IRecordService service;
        public RecordController(/*IUserService se*/)
        {
            this.service = new RecordService();
        }
        // GET: api/Record
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Record/5
        public string Get(int id)
        {
            return "a";
            //return SaveImgInCloud.Save();
            //return "value" + id;
        }

        // POST: api/Record
        public void Post([FromBody]string content)
        {
            var files = HttpContext.Current.Request.Files;
            if (files == null || files.Count <= 0)
            {
                throw new ArgumentException("There're no Images been uploaded");
            }

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var thumbnail = GenerateThumbnail.Generate(file.InputStream);
                var thumbnailUploadResult = SaveImgInCloud.Save(thumbnail);
                var imageUploadResult = SaveImgInCloud.Save(file.InputStream);
                if (thumbnailUploadResult == 200 && imageUploadResult == 200)
                {

                }
            }
        }
    }
}
