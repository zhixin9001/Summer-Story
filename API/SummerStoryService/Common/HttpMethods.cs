using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpMethods
    {
        public static T Get<T>(string url/*, int Timeout*/)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            //request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string resString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            if (string.IsNullOrEmpty(resString))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(resString);
            }
        }
    }
}
