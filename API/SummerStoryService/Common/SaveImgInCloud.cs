using Common;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.IO;
using System.Web;

public class SaveImgInCloud
{
    public static int Save(Stream imageStream, string imageName)
    {

        Mac mac = new Mac(ConfigHelper.config.AccessKey, ConfigHelper.config.SecretKey);
        // 本地文件路径
        //string filePath =  HttpContext.Current.Server.MapPath("~/Config/Capture.JPG");
        // 存储空间名
        string Bucket = "summerystory";
        // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
        PutPolicy putPolicy = new PutPolicy();
        putPolicy.Scope = Bucket;
        putPolicy.SetExpires(3600);
        putPolicy.DeleteAfterDays = 1;
        string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

        Config config = new Config();
        // 设置上传区域
        config.Zone = Zone.ZONE_CN_North;
        // 设置 http 或者 https 上传
        config.UseHttps = false;
        config.UseCdnDomains = false;
        config.ChunkSize = ChunkUnit.U4096K;
        // 表单上传
        FormUploader target = new FormUploader(config);
        //HttpResult result = target.UploadFile(filePath, key, token, null);
        var result = target.UploadStream(imageStream, imageName, token, null);
        return result.Code;
    }
}