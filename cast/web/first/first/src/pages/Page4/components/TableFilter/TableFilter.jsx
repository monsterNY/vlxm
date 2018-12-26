import axios from 'axios';
import React, { Component } from 'react';
import CustomTable from './CustomTable';
import Filter from './Filter';

const pageParam = {
  pageSize: 10,
  pageNo: 1,
  result: {
    validFlag: -1,
  },
};

export default class TableFilter extends Component {
  getData = (pageNo, callBackFunc) => {
    pageParam.pageNo = pageNo;

    global.APIConfig.sendAjax(pageParam, global.APIConfig.optMethod.GetArticlePageList, callBackFunc);

    // // console.log(`len:${len} ${len.type} currentIndex:${currentIndex}`);

    // const paramObj = global.APIConfig.getParamFunc(global.APIConfig.optMethod.GetArticlePageList, pageParam);

    // // global.APIConfig.testFunc();

    // axios
    //   .post(global.APIConfig.baseUrl, paramObj)
    //   .then((response) => {
    //     if (response.data.errorCode === global.APIConfig.resultCodeMap.success) {
    //       callBackFunc(response.data.result);
    //     } else {
    //       console.log(response.data);
    //     }
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   });
  };

  static displayName = 'TableFilter';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {};
  }

  handleRefreshTable = (ref) => {
    this.table = ref;
  }

  handleSearch = (param) => {
    pageParam.result = param;
    // console.log(pageParam);
    this.table.init();
  }

  render() {
    return (
      <div>
        <Filter
          onSearch={this.handleSearch}
        />
        <CustomTable
          getData={this.getData}
          onRefresh={this.handleRefreshTable}
        />
      </div>
    );
  }
}
