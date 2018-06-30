using Common;
using DTO;
using IService;
using Service.Repositories;
using Service.Services;
using SummerStoryService.Models.Requests;
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
            var userID = GetUserIDByToken();
            if (userID <= 0)
            {
                return null;
            }
            var records = recordService.GetPagedData(userID, startIndex, Consts.PAGE_SIZE);
            var response = new List<RecordResponse>();
            if (records != null)
            {
                for (var i = 0; i < records.Length; i++)
                {
                    var record = new RecordResponse
                    {
                        Images = new List<ImageModel>()
                    };
                    var text = textService.GetByRecordID(records[i].ID);
                    var images = imageService.GetByRecordID(records[i].ID);
                    if (text != null)
                    {
                        record.Content = text.Content;
                    }
                    if (images != null)
                    {
                        for (var j = 0; j < images.Length; j++)
                        {
                            var imageModel = new ImageModel
                            {
                                ImageURL = CloudImageManager.GetPrivateURL(images[i].ImageName),
                                ThumbnailURL = CloudImageManager.GetPrivateURL(images[i].ThumbNailName),
                            };
                            record.Images.Add(imageModel);
                        }
                    }
                    response.Add(record);
                }
            }
            return response;
        }

        // POST: api/Record
        public void Post(/*AddRecordRequest request*/)
        {
            var files = HttpContext.Current.Request.Files;
            if (files == null || files.Count <= 0)
            {
                throw new ArgumentException("There're no Images been uploaded");
            }
            var userID = GetUserIDByToken(addNewUser: true);

            if (userID < 0)
            {
                throw new Exception("Add User failed");
            }

            var recordDTO = new RecordDTO
            {
                UserID = userID
            };
            var recordID = recordService.Add(recordDTO);

            var textDTO = new TextDTO
            {
                RecordID = recordID,
                Content = "test"// request.Content
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
                if (thumbnailUploadResult == 200)
                {
                    var imageUploadResult = CloudImageManager.Save(file.InputStream, imageName + Consts.IMAGE_SUFFIX);
                    if (imageUploadResult == 200)
                    {
                        var imageDTO = new ImageDTO
                        {
                            RecordID = recordID,
                            ImageName = imageName + Consts.IMAGE_SUFFIX,
                            ThumbNailName = thumbnailName + Consts.IMAGE_SUFFIX
                        };
                        imageService.Add(imageDTO);
                    }
                    else
                    {
                        //rollback
                    }
                }
            }
        }

        private long GetUserIDByToken(bool addNewUser = false)
        {
            var auth = HttpContext.Current.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(auth))
            {
                return -1;
            }
            var split = auth.Split(new char[] { ' ' });
            string token = "";
            if (split != null && split.Length == 2)
            {
                token = split[1];
            }
            var openID = JWTManager.DecodeToken(token);
            var user = userService.GetByWxID(openID);
            if (user != null)
            {
                return user.ID;
            }
            else
            {
                if (addNewUser)
                {
                    var userDTO = new UserDTO
                    {
                        WxID = openID
                    };
                    return userService.Add(userDTO);
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
