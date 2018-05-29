// pages/detail/detail.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    photos: [
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2380677316.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2386736909.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2382076389.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2388681695.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2387538436.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2380677316.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2380677316.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2380677316.jpg" },
      { src: "https://img3.doubanio.com/view/movie_poster_cover/lpst/public/p2380677316.jpg" },
    ],
    photoCount: 0,
    photoLimit: 9,
    content: '',
    remain: {
      num: 540,
      hidden: true
    },
    location: '位置',
    x: 0,
    y: 0
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
      sizeType: ['original', 'compressed'],
      sourceType: [sourceType],
      success: function (res) {
        let addedPhotos = that.data.photos;
        addedPhotos.unshift(res.tempFilePaths);
        that.setData({
          photos: addedPhotos,
          photoCount=addedPhotos.length
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
  previewImage: function (e) {
    var current = e.target.dataset.src;
    wx.previewImage({
      current: current,
      urls: this.data.photos,
    })
  }
})