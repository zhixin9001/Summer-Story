using Common;
using DTO;
using IService;
using Service.Services;
using SummerStoryService.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SummerStoryService.Controllers
{
    public class ImageController : ApiController
    {
        //IRecordService recordService;
        //IImageService imageService;
        //public ImageController()
        //{
        //    this.recordService = new RecordService();
        //}

        //api/Image
        public void Post()
        {
            var recordIDStr = HttpContext.Current.Request.Form["recordID"];
            var sequenceStr = HttpContext.Current.Request.Form["sequence"];
            if (string.IsNullOrEmpty(recordIDStr) || string.IsNullOrEmpty(sequenceStr))
            {
                throw new ArgumentException("RecordID or Sequence hasn't been setted");
            }
            var files = HttpContext.Current.Request.Files;
            if (files == null || files.Count <= 0)
            {
                throw new ArgumentException("There're no Images been uploaded");
            }

            var file = files[0];
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
                        RecordID = Convert.ToInt64(recordIDStr),
                        Sequence = Convert.ToInt32(sequenceStr),
                        ImageName = imageName + Consts.IMAGE_SUFFIX,
                        ThumbNailName = thumbnailName + Consts.IMAGE_SUFFIX
                    };
                    //imageService.Add(imageDTO);
                }
                else
                {
                    //rollback
                }
            }
        }
    }
}
