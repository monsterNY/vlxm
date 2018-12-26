import React, { Component } from 'react';
import { Table, Pagination, Balloon, Icon } from '@icedesign/base';
// const getData = (callBackFunc) => {
//   // console.log(`len:${len} ${len.type} currentIndex:${currentIndex}`);

//   const paramObj = global.APIConfig.getParamFunc(global.APIConfig.optMethod.GetArticlePageList, pageParam);

//   // global.APIConfig.testFunc();

//   axios
//     .post(global.APIConfig.baseUrl, paramObj)
//     .then((response) => {
//       if (response.data.errorCode === global.APIConfig.resultCodeMap.success) {
//         console.log(response.data);

//         callBackFunc(response.data.result);
//       } else {
//         console.log(response.data);
//       }
//     })
//     .catch((error) => {
//       console.log(error);
//     });
// };

let getData;

const statusArr = [
  '未发布',
  '已发布',
  '异常状态',
];

export default class Home extends Component {
  static displayName = 'Home';

  callBackHandle = (data) => {
    if (data && data.result) {
      this.setState({
        current: data.pageNo,
        pageSize: data.pageSize,
        total: data.count,
        dataSource: data.result ? data.result : [],
      });
    }
  }

  constructor(props) {
    super(props);
    getData = props.getData;
    this.state = {
      current: 1,
      pageSize: 10,
      total: 50,
      dataSource: [],
    };
  }

  init = () => {
    this.state = {
      current: 1,
      pageSize: 10,
      total: 50,
      dataSource: [],
    };
    getData(1, this.callBackHandle);// 初始加载
  }

  componentDidMount() {
    getData(this.state.current, this.callBackHandle);// 初始加载
    this.props.onRefresh(this);
  }

  handlePagination = (current) => {
    getData(current, this.callBackHandle);
  };

  handleSort = (dataIndex, order) => {
    const dataSource = this.state.dataSource.sort((a, b) => {
      const result = a[dataIndex] - b[dataIndex];
      if (order === 'asc') {
        return result > 0 ? 1 : -1;
      }
      return result > 0 ? -1 : 1;
    });

    this.setState({
      dataSource,
    });
  };

  // renderCatrgory = (value) => {
  //   return (
  //     <Balloon
  //       align="lt"
  //       trigger={<div style={{ margin: '5px' }}>{value}</div>}
  //       closable={false}
  //       style={{ lineHeight: '24px' }}
  //     >
  //       {value}
  //       {/* 青霉素是抗菌素的一种，是能破坏细菌的细胞壁并在细菌细胞的繁殖期起杀菌作用的一类抗生素 */}
  //     </Balloon>
  //   );
  // };

  renderFlag = (value) => {
    return (
      <div style={styles.state}>
        <span style={styles.circle} />
        <span style={styles.stateText}>{global.APIConfig.ValidFlagArr[value]}</span>
      </div>
    );
  };

  renderStatus = (value) => {
    console.log(value);
    return (
      <div style={styles.state}>
        <span style={styles.circle} />
        <span style={styles.stateText}>{statusArr[value]}</span>
      </div>
    );
  };

  renderOper = () => {
    return (
      <div style={styles.oper}>
        <Icon type="edit" size="small" style={styles.editIcon} />
      </div>
    );
  };

  render() {
    const { dataSource, current, pageSize, total } = this.state;
    return (
      <div style={styles.tableContainer}>
        <Table
          dataSource={dataSource}
          onSort={this.handleSort}
          hasBorder={false}
          className="custom-table"
        >
          <Table.Column title="序列号" dataIndex="id" sortable align="center" />
          {/* <Table.Column title="调价单号" dataIndex="orderID" sortable /> */}
          <Table.Column title="标题" dataIndex="title" />
          <Table.Column title="作者" dataIndex="author" />
          <Table.Column title="发布日期" dataIndex="updateTime" />
          <Table.Column title="创建日期" dataIndex="createTime" />
          <Table.Column title="修改日期" dataIndex="updateTime" />
          {/* <Table.Column
            title="分类"
            dataIndex="category"
            cell={this.renderCatrgory}
          /> */}
          <Table.Column
            title="是否有效"
            dataIndex="validFlag"
            cell={this.renderFlag}
          />
          <Table.Column
            title="文章状态"
            dataIndex="status"
            cell={this.renderStatus}
          />
          {/* <Table.Column title="审核人" dataIndex="approver" /> */}
          {/* <Table.Column title="审核日期" dataIndex="approvalData" /> */}
          <Table.Column title="操作" cell={this.renderOper} />
        </Table>
        {/* 参数参考：https://ant.design/components/pagination-cn/ */}
        <Pagination
          style={styles.pagination}
          current={current}
          onChange={this.handlePagination}
          total={total}
          pageSize={pageSize}
        />
      </div>
    );
  }
}

const styles = {
  tableContainer: {
    background: '#fff',
    paddingBottom: '10px',
  },
  pagination: {
    margin: '20px 0',
    textAlign: 'center',
  },
  editIcon: {
    color: '#999',
    cursor: 'pointer',
  },
  circle: {
    display: 'inline-block',
    background: '#28a745',
    width: '8px',
    height: '8px',
    borderRadius: '50px',
    marginRight: '4px',
  },
  stateText: {
    color: '#28a745',
  },
};
