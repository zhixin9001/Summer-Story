using Common;
using DTO;
using IService;
using Service.Repositories;
using Service.Services;
using SummerStoryService.Models.Responses;
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
        IUserService userService;
        IRecordService recordService;
        ITextService textService;
        IImageService imageService;
        public RecordController(/*IUserService se*/)
        {
            this.userService = new UserService();
            this.recordService = new RecordService();
            this.textService = new TextService();
            this.imageService = new ImageService();
        }
        // GET: api/Record
        public IEnumerable<RecordResponse> Get(int startIndex)
        {
            var records = recordService.GetPagedData(2, startIndex, 10);
            var response = new List<RecordResponse>();
            if (records != null)
            {
                for (var i = 0; i < records.Length; i++)
                {
                    var record = new RecordResponse();
                    record.Images = new List<ImageModel>();
                    var text = textService.GetByRecordID(records[i].ID);
                    var images = imageService.GetByRecordID(records[i].ID);
                    record.Content = text.Content;
                    for (var j = 0; j < images.Length; j++)
                    {
                        var imageModel = new ImageModel
                        {
                            ImageURL = CloudImageManager.GetPrivateURL(images[i].ImageName),
                            ThumbnailURL = CloudImageManager.GetPrivateURL(images[i].ThumbNailName),
                        };
                        record.Images.Add(imageModel);
                    }
                    response.Add(record);
                }
            }
            return response;
        }

        // GET: api/Record/5
        public string Get(string id)
        {
            return "a";
            //return SaveImgInCloud.Save();
            //return "value" + id;
        }

        // POST: api/Record
        public void Post()
        {
            var files = HttpContext.Current.Request.Files;
            if (files == null || files.Count <= 0)
            {
                throw new ArgumentException("There're no Images been uploaded");
            }
            var user = userService.GetByWxID("zhixin9001");
            long userID;
            if (user == null)
            {
                var userDTO = new UserDTO
                {
                    WxID = "zhixin9001"
                };
                userID = userService.Add(userDTO);
            }
            else
            {
                userID = user.ID;
            }

            var recordDTO = new RecordDTO
            {
                UserID = userID,
            };
            var recordID = recordService.Add(recordDTO);
            var textDTO = new TextDTO
            {
                RecordID = recordID,
                Content = "asdf"
            };
            textService.Add(textDTO);

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var imageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + DateTime.Now.ToString();
                var thumbnailName = imageName + Consts.THUMBNAIL_FLAG;

                var thumbnail = GenerateThumbnail.Generate(file.InputStream);
                var thumbnailUploadResult = CloudImageManager.Save(thumbnail, thumbnailName + Consts.IMAGE_SUFFIX);
                file.InputStream.Position = 0;
                var imageUploadResult = CloudImageManager.Save(file.InputStream, imageName + Consts.IMAGE_SUFFIX);
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
