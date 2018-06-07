using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    class ConfigHelper
    {
        public static ConfigModel config = ConfigHelper.InitConfig();
        public static ConfigModel InitConfig()
        {
            var config = new ConfigModel();
            var configPath = HttpContext.Current.Server.MapPath("~/Config/Config.json");
            using (var fs = new StreamReader(configPath))
            {
                return JsonConvert.DeserializeObject<ConfigModel>(fs.ReadToEnd());
            }
        }
    }

    public class ConfigModel
    {
        public string AccessKey;
        public string SecretKey;
        public string CloudDomain;
    }
}
