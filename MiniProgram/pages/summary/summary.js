//获取应用实例
const app = getApp()
const config = require('../../config');

Page({
  data: {
    page: 0,
    records: [],
    loading: false,
    hasMore: false
  },
  onLoad: function () {
    this.setData({ hasMore: true });
    this.loadMore();
  },
  loadMore() {
    if (!this.data.hasMore) return;
    this.setData({ loading: true, page: this.data.page + 1 });
    var self = this;
    wx.request({
      url: config.recordUrl,
      header: { Authorization: app.globalData.token },
      method: "GET",
      data: { page: this.data.page },
      success: function (res) {
        console.log(res.data);
        if (res.data.length > 0) {
          self.setData({ records: self.data.records.concat(res.data), loading: false });
        } else {
          self.setData({ hasMore: false, loading: false });
        }
      },
      fail: function (err) {
        console.log(err);
      }
    });
  },
  onReachBottom() {
    this.loadMore()
  }
})
