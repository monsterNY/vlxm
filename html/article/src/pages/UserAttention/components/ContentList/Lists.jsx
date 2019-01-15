import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { Grid, Icon, Pagination, Dialog, Loading, Feedback } from '@icedesign/base';
import { Link } from 'react-router-dom';

const { Row, Col } = Grid;

export default class Lists extends Component {
  static displayName = 'Lists';

  constructor(props) {
    super(props);
    this.state = {
      dataArr: [],
      removeDialogVisible: false,
      optKey: 0,
    };
    this.initParam();
  }

  loadData = (param) => {
    this.pageParm.result = param;
    if (this.canLoadFlag) {
      this.loadAttentionList(this.pageParm);
    }
  }

  initParam = () => {
    this.pageParm = {
      pageSize: 6,
      pageNo: 1,
      total: 0,
      result: {},
    };
    this.canLoadFlag = false;
  }

  loadAttentionList = (param) => {
    global.APIConfig.sendAuthAjax(this, param, global.APIConfig.optAuthMethod.GetAttentionPageList, (resultData) => {
      if (resultData.result) {
        this.pageParm.pageNo = resultData.pageNo;
        this.pageParm.pageSize = resultData.pageSize;
        this.pageParm.total = resultData.count;

        // 刷新渲染
        this.setState({ dataArr: resultData.result });
      }
    });
  }

  componentDidMount() {
    this.canLoadFlag = true;
    if (this.props.bindRef) {
      this.props.bindRef(this);// 绑定子组件的引用
    }
    this.loadAttentionList(this.pageParm);
  }

  hideConfirmDialogEvent = () => {
    this.setState({
      confirmDialogVisible: false,
    });
  }

  cancelAttentionEvent = (key) => {
    this.optKey = key;
    this.setState({
      confirmDialogVisible: true,
    });
  }

  cancelAttentionUserAjax = (key) => {
    global.APIConfig.sendAuthAjax(this, { key }, global.APIConfig.optAuthMethod.CancelAttentionUser, () => {

    });
  }

  cancelAttentionEvent = () => {
    this.hideConfirmDialogEvent();
    if (this.optKey) {
      this.CancelAttentionUser(this.optKey);
    }
  }

  renderLodingOrMenu = (key) => {
    // if (this.state.optKey) {
    //   if (key === this.state.optKey) {
    //     return (
    //       <Loading shape="flower" tip="loading...">
    //         <div style={{
    //           width: 50,
    //           textAlign: 'center',
    //         }}
    //         />
    //       </Loading>
    //     );
    //   }
    // }
    return (
      <div style={styles.oper} onClick={() => { this.cancelAttentionEvent(key); }}>
        <Icon size="xs" type="favorite" style={styles.operIcon} />
        <span style={styles.operText}>取消关注</span>
      </div>
    );
  }

  render() {
    const data = this.state.dataArr;
    return (
      <IceContainer>
        <Dialog
          visible={this.state.confirmDialogVisible}
          // onOk={this.}
          onClose={this.hideConfirmDialogEvent}
          onCancel={this.hideConfirmDialogEvent}
          title="是否确定取消关注此用户？"
        />
        <h4 style={styles.cardTitle}>用户列表</h4>
        <div style={styles.contentList}>
          {data.map((item, index) => {
            return (
              <div style={styles.item} key={index}>
                <Link target="_blank" style={{ marginLeft: 20 }} to={`/account/detail/${item.userInfo.id}`}>
                  <img style={{
                    borderRadius: '50%',
                    width: 58,
                    height: 58,
                  }}
                    src={global.APIConfig.getImgSrc(item.userInfo.faceImg)}
                    alt=""
                  />
                  名称：{item.userInfo.displayName}
                </Link>
                <Row>
                  <Col l="18">
                    <div style={styles.metaWrap}>
                      <div style={styles.meta}>
                        <span>分组: </span>
                        <span>{item.groupKey}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>备注: </span>
                        <span>{item.description}</span>
                      </div>
                    </div>
                  </Col>
                  <Col l="4">
                    <div style={styles.operWrap}>
                      {this.renderLodingOrMenu(item.id)}
                    </div>
                  </Col>
                </Row>
              </div>
            );
          })}
        </div>
        <Pagination
          style={styles.pagination}
          current={this.pageParm.pageNo}
          onChange={this.handlePagination}
          total={this.pageParm.total}
          pageSize={this.pageParm.pageSize}
        />
      </IceContainer>
    );
  }
}

const styles = {
  cardTitle: {
    height: '16px',
    lineHeight: '16px',
    fontSize: '16px',
    color: 'rgb(51, 51, 51)',
    fontWeight: 'bold',
    margin: '0',
    padding: '0',
  },
  item: {
    position: 'relative',
    borderBottom: '1px solid #eee',
    padding: '20px 0',
  },
  title: {
    margin: '0 0 10px',
  },
  metaWrap: {
    marginTop: 10,
    display: 'flex',
    paddingLeft: '15px',
  },
  meta: {
    fontSize: '13px',
    color: '#999',
    marginRight: '15px',
  },
  operWrap: {
    position: 'absolute',
    right: '0',
    top: '36px',
    display: 'flex',
    cursor: 'pointer',
  },
  oper: {
    marginLeft: '15px',
    fontSize: '13px',
    color: '#999',
  },
  operIcon: {
    marginRight: '8px',
  },
  pagination: {
    marginTop: '20px',
    textAlign: 'right',
  },
};
