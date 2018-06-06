using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    class ConfigHelper
    {
        public static ConfigModel config=ConfigHelper.InitConfig();
        public static ConfigModel InitConfig()
        {
            var config = new ConfigModel();

            return config;
        }
    }

    public class ConfigModel
    {
        public string AccessKey;
        public string SecretKey;
        public string CloudDomain;
    }
}
