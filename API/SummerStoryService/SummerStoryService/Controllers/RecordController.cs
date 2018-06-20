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
        IRecordService recordService;
        ITextService textService;
        IImageService imageService;
        public RecordController(/*IUserService se*/)
        {
            this.recordService = new RecordService();
            this.textService = new TextService();
            this.imageService = new ImageService();
        }
        // GET: api/Record
        public IEnumerable<string> Get(int startIndex)
        {
            var records = recordService.GetPagedData(1, startIndex, 10);
            if (records != null)
            {
                for (var i = 0; i < records.Length; i++)
                {
                    var text = textService.GetByRecordID(records[i].ID);
                    var images = imageService.GetByRecordID(records[i].ID);
                }
            }
            return new string[] { "value1", "value2" };
        }

        // GET: api/Record/5
        public string Get(string id)
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
            var recordDTO = new RecordDTO
            {
                UserID = 1,
            };
            var recordID = recordService.Add(recordDTO);
            var textDTO = new TextDTO
            {
                RecordID = recordID,
                Content = content
            };
            textService.Add(textDTO);

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var imageName = Guid.NewGuid() + "-" + DateTime.Now.ToString();
                var thumbnailName = imageName + "-thumbnail";

                var thumbnail = GenerateThumbnail.Generate(file.InputStream);
                var thumbnailUploadResult = SaveImgInCloud.Save(thumbnail, thumbnailName);
                var imageUploadResult = SaveImgInCloud.Save(file.InputStream, imageName);
                if (thumbnailUploadResult == 200 && imageUploadResult == 200)
                {

                }
                var imageDTO = new ImageDTO
                {
                    RecordID = recordID,
                    ImageName = imageName,
                    ThumbNailName = thumbnailName
                };
                imageService.Add(imageDTO);
            }
        }
    }
}
