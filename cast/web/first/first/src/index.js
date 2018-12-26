import axios from 'axios';
import React from 'react';
import ReactDOM from 'react-dom';

import {
  HashRouter
} from 'react-router-dom';

// 载入默认全局样式 normalize 、.clearfix 和一些 mixin 方法等
import '@icedesign/base/reset.scss';

import router from './router';

// 全局注册 Api 配置信息
global.APIConfig = {
  baseUrl: 'http://api.moreover.manage/api/home',
  resultCodeMap: {
    success: 0,
  },
  optMethod: {
    GetArticlePageList: 'GetArticlePageList',
    InsertArticle: 'InsertArticle',
  },
  ValidFlagArr: [
    '无效',
    '有效',
  ],
  getSignFunc: (paramObj) => {
    return `no sign ${paramObj}`;
  },
  sendAjax: (paramObj, optFlag, callBack) => {
    paramObj = global.APIConfig.getParamFunc(optFlag, paramObj);

    axios
      .post(global.APIConfig.baseUrl, paramObj)
      .then((response) => {
        if (response.data.errorCode === global.APIConfig.resultCodeMap.success) {
          callBack(response.data.result);
        } else {
          console.log(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
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

const ICE_CONTAINER = document.getElementById('ice-container');

if (!ICE_CONTAINER) {
  throw new Error('当前页面不存在 <div id="ice-container"></div> 节点.');
}

ReactDOM.render(
  <HashRouter>{router()}</HashRouter>,

  ICE_CONTAINER
);
