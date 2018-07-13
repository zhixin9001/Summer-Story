var app = getApp();
// pages/record/record.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    record: undefined
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (param) {
    if (!param || !param.recordID
      || !app.globalData || !app.globalData.records
      || app.globalData.records.length <= 0) {
      return;
    }
    let record = app.globalData.records.find(a => a.RecordID = param.recordID);
    this.setData({ record: record });
    console.log(this.data.record);
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})