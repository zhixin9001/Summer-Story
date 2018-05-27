//index.js
//获取应用实例
const app = getApp()

Page({
  data: {
    src: undefined,
    inputValue:undefined,
    leftWords: 0,
    test:undefined,
  },
  onLoad: function () {
  },
  //事件处理函数
  takePhoto: function () {
    let that = this;
    wx.chooseImage({
      sourceType: ['camera'],
      success: function (res) {
        let filePath = res.tempFilePaths[0];
        that.setData({ src: [filePath] });
      }
    })
  },
  selectPhoto: function () {
    let that = this;
    wx.chooseImage({
      count: 2,
      success: function (res) {
        let filePath = res.tempFilePaths[0];
        that.setData({ src: [filePath] });
      },
    })
  },
  wordsChanged: function (e) {
    console.log(e.detail.value);
    this.setData({ test: e.detail.value });
    this.setData({ inputValue: e.detail.value });
    this.setData({ leftWords: 1000 - e.detail.value.length });
  },
  saveRecord: function () {
    console.log(this.inputValue);
    console.log(this.test);
  }
})
