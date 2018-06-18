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
        public void Post()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                //var fileName = Path.GetFileName(file.FileName);

                //var path = Path.Combine(
                //    HttpContext.Current.Server.MapPath("~/uploads"),
                //    fileName
                //);

                //file.SaveAs(path);
            }
        }
    }
}
