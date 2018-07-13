const config = require('../../config');
var app = getApp()

Page({

  /**
   * 页面的初始数据
   */
  data: {
    photos: [],
    photoCount: 0,
    photoLimit: 9,
    content: '',
    remain: {
      num: 540,
      hidden: true
    },
    location: '位置',
    x: 0,
    y: 0,
    recordID: 0
  },
  addPhoto: function () {
    let that = this;
    wx.showActionSheet({
      itemList: ['拍照', '从相册选择'],
      success: function (res) {
        if (!res.cancel) {
          if (res.tapIndex === 0) {
            that.chooseWxImage("camera");
          } else if (res.tapIndex === 1) {
            that.chooseWxImage("album");
          }
        }
      }
    });
  },
  chooseWxImage(sourceType) {
    let that = this;
    wx.chooseImage({
      count: that.data.photoLimit - that.data.photoCount,
      sizeType: ['original'],
      sourceType: [sourceType],
      success: function (res) {
        let addedPhotos = that.data.photos;
        res.tempFilePaths.forEach(a => {
          addedPhotos.unshift(a);
        });
        that.setData({
          photos: addedPhotos,
          photoCount: addedPhotos.length
        });
      }
    });
  },
  changeTips: function (e) {
    let value = e.detail.value;
    let remain = this.data.remain;

    if (remain.hidden) {
      remain.hidden = false;
    };
    remain.num = 540 - value.length;

    this.setData({
      remain: remain,
      content: value
    });
  },
  chooseLocation: function () {
    let _location = this.data.location;
    let _x = this.data.x;
    let _y = this.data.y;
    let _this = this;
    wx.chooseLocation({
      success: function (res) {
        _location = res.name;
        _x = res.latitude;
        _y = res.longitude

        _this.setData({
          location: _location,
          x: _x,
          y: _y
        });
      }
    })
  },
  bindTouchStart: function (e) {
    this.startTime = e.timeStamp;
  },
  bindTouchEnd: function (e) {
    this.endTime = e.timeStamp;
  },
  //点击预览
  previewImage: function (e) {
    if (this.endTime - this.startTime >= 350) {
      return;
    }
    var current = e.target.dataset;
    wx.previewImage({
      current: current,
      urls: this.data.photos,
    })
  },
  //长按删除
  deleteImage: function (e) {
    let _e = e;
    let that = this;
    wx.showActionSheet({
      itemList: ['删除选中'],
      success: function (res) {
        if (!res.cancel) {
          if (res.tapIndex === 0) {
            let index = that.data.photos.findIndex(a => a == _e.target.dataset);
            that.data.photos.splice(index);
            that.setData({
              photos: that.data.photos
            });
          }
        }
      }
    })
  },
  submitData: function (e) {
    let self = this;
    if (!self.data.content || self.data.content.length <= 0) {
      wx.showToast({
        title: '请添加图片或文字',
      })
      return;
    }
    wx.request({
      url: config.recordUrl,
      method: 'POST',
      header: { Authorization: app.globalData.token },
      data: {
        content: self.data.content,
        latitude: self.data.x,
        longitude: self.data.y
      },
      success: function (res) {
        self.setData({
          recordID: res.data
        });
        if (this.data.photos && this.data.photos.length > 0) {
          self.uploadPhoto();
        } else {
          self.activateRecord();
        }
      },
      fail: function (err) {
        console.log(err);
      }
    })
  },
  uploadPhoto: function (e) {
    var self = this;
    var promise = Promise.all(this.data.photos.map((tempFilePath, index) => {
      return new Promise(function (resolve, reject) {
        wx.uploadFile({
          url: config.uploadImageUrl,
          filePath: tempFilePath,
          name: index.toString(),
          formData: { recordID: self.data.recordID, sequence: index },
          header: { Authorization: app.globalData.token },
          success: function (res) {
            resolve(res.data);
          },
          fail: function (err) {
            reject(err.data);
          }
        })
      })
    }));
    self.activateRecord();
  },

  activateRecord() {
    wx.request({
      url: config.recordUrl,
      header: { Authorization: app.globalData.token },
      method: "PUT",
      data: { recordID: self.data.recordID },
      success: function () {
        wx.showToast({
          title: '保存成功',
        })
        
      },
      fail: function () {
        wx.showToast({
          title: '保存失败',
        })
      }
    })
  }

})