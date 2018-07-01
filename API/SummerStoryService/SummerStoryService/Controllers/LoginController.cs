﻿using Common;
using IService;
using Service.Services;
using SummerStoryService.App_Start;
using SummerStoryService.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SummerStoryService.Controllers
{
    public class LoginController : ApiController
    {
        IUserService userService;
        public LoginController()
        {
            this.userService = new UserService();
        }

        //POST: api/Login
        [AllowAnonymous]
        public string Get(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }
            var wxSessionResponse = HttpMethods.Get<WxSessionResponse>(
                string.Format(ConfigHelper.config.WxTokenURL
                                        , ConfigHelper.config.WxAppID
                                        , ConfigHelper.config.WxApp_Secret
                                        , code)
            );
            if (string.IsNullOrEmpty(wxSessionResponse.openid))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else
            {
                return JWTManager.GenerateToken(wxSessionResponse.openid);
            }
        }
    }
}