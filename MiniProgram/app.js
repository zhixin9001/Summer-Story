const config = require('./config');

App({
  globalData: {
    token: null,
    records: []
  },
  onLaunch: function () {
    var self = this;
    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId
        wx.request({
          url: config.loginUrl,
          method: "GET",
          data: { code: res.code },
          success: function (result) {
            self.globalData.token = "Bearer " + result.data;
          },
          fail: function () {
            wx.showModal({
              title: '登录失败',
              content: '登录失败，请尝试重新打开应用',
              showCancel: false,
              success: function (res) {
                wx.navigateBack({
                  delta: -1
                })
              }
            })
          }
        })
      }
    })
  },

})