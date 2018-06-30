using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SummerStoryService.App_Start
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            return;
            //如果Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            var token = actionContext.Request.Headers.Authorization != null ? actionContext.Request.Headers.Authorization.Parameter : null;
            bool isAuthed = true;
            if (string.IsNullOrEmpty(token))
            {
                isAuthed = false;
            }
            else
            {
                var openID = JWTManager.DecodeToken(token);
                if (string.IsNullOrEmpty(openID))
                {
                    isAuthed = false;
                }
            }
            if (!isAuthed)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token Error!"));
            }
            /*
            GET http://localhost:1156/api/record/1 HTTP/1.1
            User-Agent: Fiddler
            Host: localhost:1156
            content-type: application/json
            authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJvcGVuSUQiOiJvTUxKcjVYNHREUTNwQzRSdndkRHNVbmh4dWY4IiwiZXhwIjoxNTMwMDk3NzU1LjB9.AyeVBZ7h0--6f93wtdBpXxu6ApjJGUwD-FkXC7qcbvo
             */
        }
    }
}