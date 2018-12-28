import axios from 'axios';
import ReactDOM from 'react-dom';
// 载入默认全局样式 normalize 、.clearfix 和一些 mixin 方法等
import '@icedesign/base/reset.scss';
import router from './router';

const ICE_CONTAINER = document.getElementById('ice-container');

global.CusStyle = {
  hideStyle: {
    display: 'none',
  },
};

// 全局注册 Api 配置信息
global.APIConfig = {
  userInfo: {// 用户信息
    id: 2018,
  },
  baseUrl: 'http://api.moreover.manage/api/home',
  imgBaseUrl: 'http://api.moreover.manage/',
  uploadUrl: 'http://api.moreover.manage/api/util/UploadSingleImage',
  resultCodeMap: {
    success: 0,
  },
  optMethod: {
    GetArticlePageList: 'GetArticlePageList',
    InsertArticle: 'InsertArticle',
    GetArticleTypePageList: 'GetArticleTypePageList',
    GetArticleTagPageList: 'GetArticleTagPageList',
  },
  ValidFlagArr: [
    '无效',
    '有效',
  ],
  getSignFunc: (paramObj) => {
    return `no sign ${paramObj}`;
  },
  sendAjax: (paramObj, optFlag, callBack, errorFunc) => {
    paramObj = global.APIConfig.getParamFunc(optFlag, paramObj);

    console.log(paramObj);

    axios
      .post(global.APIConfig.baseUrl, paramObj)
      .then((response) => {
        console.log(response.data);
        if (response.data.errorCode === global.APIConfig.resultCodeMap.success) {
          callBack(response.data.result);
        } else if (errorFunc) {
          errorFunc(); // 异常回调
        }
      })
      .catch((error) => {
        console.log(error);
        if (errorFunc) {
          errorFunc(); // 异常回调
        }
      });
  },
  getParamFunc: (optFlag, paramObj) => {
    return {
      version: 1.0,
      operationFlag: optFlag,
      param: paramObj,
      sign: global.APIConfig.getSignFunc(paramObj),
    };
  },
  testFunc: () => {
    console.log('testFunc');
  },
};

if (!ICE_CONTAINER) {
  throw new Error('当前页面不存在 <div id="ice-container"></div> 节点.');
}

ReactDOM.render(router, ICE_CONTAINER);
