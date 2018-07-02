/**
 * 小程序配置文件
 */
var host = "http://localhost:1156"

var config = {
  loginUrl: `${host}/api/Login`,
  addRecordUrl: `${host}/api/Record`,
  markRecordEnableUrl: `${host}/api/Record`,
  uploadImageUrl: `${host}/api/Image`,
  downloadFileUrl: `${host}/static/weapp.jpg`
};

module.exports = config
