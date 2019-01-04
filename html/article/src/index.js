import axios from 'axios';
import ReactDOM from 'react-dom';
// 载入默认全局样式 normalize 、.clearfix 和一些 mixin 方法等
import '@icedesign/base/reset.scss';
import router from './router';
import {
  Feedback
} from '@icedesign/base';

const ICE_CONTAINER = document.getElementById('ice-container');

global.CusStyle = {
  hideStyle: {
    display: 'none',
  },
};

// 全局注册 Api 配置信息
global.APIConfig = {
  userInfo: { // 用户信息
    id: 2018,
  },
  baseUrl: 'http://api.moreover.manage/api/home',
  baseAuthUrl: 'http://api.moreover.manage/api/auth',
  imgBaseUrl: 'http://api.moreover.manage/',
  uploadUrl: 'http://api.moreover.manage/api/util/UploadSingleImage',
  uploadBase64Url: 'http://api.moreover.manage/api/util/UploadBase64Image',
  userInfoCacheKey: '_vlxm_user_cache_info_',
  getUserCache: () => {
    const value = window.localStorage[global.APIConfig.userInfoCacheKey];
    if (value) {
      const userCacheInfo = JSON.parse(window.localStorage[global.APIConfig.userInfoCacheKey]);
      return userCacheInfo;
    }
  },
  setUserCache: (userInfo) => {
    if (userInfo) {
      window.localStorage[global.APIConfig.userInfoCacheKey] = JSON.stringify(userInfo);
    } else {
      window.localStorage[global.APIConfig.userInfoCacheKey] = null;
    }
  },
  defaultImgUrl: 'https://img.alicdn.com/tps/TB19O79MVXXXXcZXVXXXXXXXXXX-1024-1024.jpg',
  resultCodeMap: {
    success: 0,
  },
  optMethod: {
    GetArticlePageList: 'GetArticlePageList',
    InsertArticle: 'InsertArticle',
    GetArticleTypePageList: 'GetArticleTypePageList',
    GetArticleTagPageList: 'GetArticleTagPageList',
    GetArticleDetail: 'GetArticleDetail',
    CreateUserInfo: 'CreateUserInfo',
    UserLogin: 'UserLogin',
  },
  optAuthMethod: {
    GetUserDetail: 'GetUserDetail',
    UpdateUserInfo: 'UpdateUserInfo',
  },
  ValidFlagArr: [
    '无效',
    '有效',
  ],
  getSignFunc: (paramObj) => {
    return `no sign ${paramObj}`;
  },
  baseSendAjax: (url, paramObj, callBack, errorFunc, responseErrorFunc, token) => {
    axios
      .post(url, paramObj, {
        headers: {
          Authorization: token,
        },
      })
      .then((response) => {
        console.log(response.data);
        if (response.data.errorCode === global.APIConfig.resultCodeMap.success) {
          callBack(response.data.result);
        } else if (errorFunc) {
          errorFunc(response.data.message); // 异常回调
        }
      })
      .catch((error) => {
        console.log(error);
        if (responseErrorFunc) {
          responseErrorFunc(error); // 异常回调
        }
      });
  },
  sendAjax: (paramObj, optFlag, callBack, errorFunc) => {
    paramObj = global.APIConfig.getParamFunc(optFlag, paramObj);

    console.log(paramObj);

    global.APIConfig.baseSendAjax(global.APIConfig.baseUrl, paramObj, callBack, errorFunc);
  },
  sendAuthAjax: (localInstance, paramObj, optFlag, callBack, errorFunc, backUrl) => {
    paramObj = global.APIConfig.getParamFunc(optFlag, paramObj);

    console.log(paramObj);

    const userCacheInfo = global.APIConfig.getUserCache();

    if (!userCacheInfo) {
      Feedback.toast.error('尚未登录！');
      if (backUrl) {
        localInstance.props.history.push(backUrl);
      } else {
        localInstance.props.history.push('/user/login');
      }
    }

    console.log(userCacheInfo);

    const token = `${userCacheInfo.token_type} ${userCacheInfo.access_token}`;

    console.log(token);

    global.APIConfig.baseSendAjax(global.APIConfig.baseAuthUrl, paramObj, callBack, errorFunc, (error) => {
      if (error.response.status === 401) {
        Feedback.toast.error('登录信息已失效！');
        global.APIConfig.setUserCache();
        if (backUrl) {
          localInstance.props.history.push(backUrl);
        } else {
          localInstance.props.history.push('/user/login');
        }
      }
    }, token);
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
