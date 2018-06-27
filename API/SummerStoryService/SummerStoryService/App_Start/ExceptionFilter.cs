using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace SummerStoryService.App_Start
{
    public class ExceptionFilter : IExceptionFilter
    {
        public bool AllowMultiple => true;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            LogHelper.WriteLog(actionExecutedContext.Exception);
            return null;
        }        
    }
}