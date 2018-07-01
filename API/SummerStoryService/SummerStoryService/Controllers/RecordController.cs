﻿using Common;
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
        public long Post(AddRecordRequest request)
        {
            var userID = GetUserIDByToken(addNewUser: true);
            var recordDTO = new RecordDTO
            {
                UserID = userID,
                IsDeleted = true //set this flag as true after adding text and images successfully
            };
            var recordID = recordService.Add(recordDTO);
            var textDTO = new TextDTO
            {
                RecordID = recordID,
                Content = request.Content
            };
            textService.Add(textDTO);
            return recordID;
        }

        public void Put(MarkRecordEnableRequest request)
        {
            recordService.MarkRecordEnable(request.RecordID);
        }

        private long GetUserIDByToken(bool addNewUser = false)
        {
            long userID = -1;
            var auth = HttpContext.Current.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(auth))
            {
                userID = -1;
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
                userID = user.ID;
            }
            else
            {
                if (addNewUser)
                {
                    var userDTO = new UserDTO
                    {
                        WxID = openID
                    };
                    userID = userService.Add(userDTO);
                }
                else
                {
                    userID = -1;
                }
            }
            if (userID >= 0)
            {
                return userID;
            }
            else
            {
                throw new Exception("Add User failed");
            }
        }
    }
}