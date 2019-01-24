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
      articleTypeArr: [],
      articleArr: [],
      removeDialogVisible: false,
      optKey: 0,
    };
    this.initParam();
  }

  loadData = (param) => {
    this.pageParm.result = param;
    if (this.canLoadFlag) {
      this.loadArticlePageList(this.pageParm);
    }
  }

  // 加载文章类型
  loadArticleTypeList = () => {
    global.APIConfig.sendAjax({
      pageSize: 8,
      pageNo: 1,
      result: {
        validFlag: 1,
      },
    }, global.APIConfig.optMethod.GetArticleTypePageList, (resultData) => {
      if (resultData.result) {
        // 刷新渲染
        this.setState({ articleTypeArr: resultData.result });
      }
    });
  }

  initParam = () => {
    this.pageParm = {
      pageSize: 6,
      pageNo: 1,
      total: 0,
    };
    this.canLoadFlag = false;
  }

  componentDidMount() {
    this.canLoadFlag = true;
    if (this.props.bindRef) {
      this.props.bindRef(this);// 绑定子组件的引用
    }
    this.loadArticleTypeList();
    this.loadData({
      filterType: 2,
    });
  }

  removeItemAuthAjax = (param) => {
    global.APIConfig.sendAuthAjax(this, param, global.APIConfig.optAuthMethod.RemoveArticle, () => {
      Feedback.toast.success('删除成功！');
      let articleList = this.state.articleArr;

      articleList = articleList.filter(u => u.id !== this.removeKey);

      // 刷新渲染
      this.setState({ optKey: 0, articleArr: articleList });
    });
  }

  // 加载文章列表
  loadArticlePageList = (param) => {
    global.APIConfig.sendAuthAjax(this, param, global.APIConfig.optAuthMethod.GetArticlePageList, (resultData) => {
      if (resultData.result) {
        this.pageParm.pageNo = resultData.pageNo;
        this.pageParm.pageSize = resultData.pageSize;
        this.pageParm.total = resultData.count;

        // 刷新渲染
        this.setState({ articleArr: resultData.result });
      }
    });
  }

  // handleEditEvent = (key) => {
  //   Feedback.toast.error('尚未开放此功能！');
  //   console.log(`handleEditEvent:${key}`);
  // }

  handleRemoveEvent = (key) => {
    if (this.state.optKey > 0) {
      return;
    }
    this.removeKey = key;
    this.setState({
      removeDialogVisible: true,
    });
  }

  removeHandler = () => {
    this.removeItemAuthAjax({
      key: this.removeKey,
    });
    this.setState({ optKey: this.removeKey });
    this.hideRemoveConfirmEvent();
  }

  hideRemoveConfirmEvent = () => {
    this.setState({
      removeDialogVisible: false,
    });
  }

  handlePagination = (currnet) => {
    this.pageParm.pageNo = currnet;
    this.loadArticlePageList(this.pageParm);
  }

  searchTypeNameEvent = (searchKey) => {
    if (this.state.articleTypeArr) {
      const filterArr = this.state.articleTypeArr.filter(u => u.id === searchKey);
      if (filterArr && filterArr.length === 1) {
        return filterArr[0].typeName;
      }
    }
    return searchKey;
  }

  renderLodingOrMenu = (key) => {
    if (this.state.optKey) {
      if (key === this.state.optKey) {
        return (
          <Loading shape="flower" tip="loading...">
            <div style={{
              width: 50,
              textAlign: 'center',
            }}
            />
          </Loading>
        );
      }
    }
  }

  render() {
    const data = this.state.articleArr;
    return (
      <IceContainer>
        <Dialog
          visible={this.state.removeDialogVisible}
          onOk={this.removeHandler}
          onClose={this.hideRemoveConfirmEvent}
          onCancel={this.hideRemoveConfirmEvent}
          title="是否确定删除此信息？"
        />
        <h4 style={styles.cardTitle}>文章列表</h4>
        <div style={styles.contentList}>
          {data.map((item, index) => {
            return (
              <div style={styles.item} key={index}>
                <Link target="_blank" style={{ marginLeft: 20 }} to={`/article/detail/${item.id}`}>
                  标题：{item.title}
                </Link>
                <Row>
                  <Col l="18">
                    <div style={styles.metaWrap}>
                      <div style={styles.meta}>
                        <span>评论数量: </span>
                        <span>{item.commentCount}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>点赞数量: </span>
                        <span>{item.likeCount}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>浏览次数: </span>
                        <span>{item.pageView}</span>
                      </div>
                    </div>
                    <div style={styles.metaWrap}>
                      <div style={styles.meta}>
                        <span>笔名: </span>
                        <span>{item.author}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>文章类型: </span>
                        <span>{this.searchTypeNameEvent(item.articleType)}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>发布时间: </span>
                        <span>{item.publishTime}</span>
                      </div>
                      <div style={styles.meta}>
                        <span>类别: </span>
                        <span>{item.status === 1 ? '收费' : '免费'}</span>
                      </div>
                    </div>
                    <div style={styles.metaWrap}>
                      <div style={styles.meta}>
                        <span>描述信息: </span>
                        <span>{item.description}</span>
                      </div>
                    </div>
                  </Col>
                  <Col l="4">
                    <div style={styles.operWrap}>
                      <div
                        style={styles.oper}
                        // onClick={() => { this.handleEditEvent(item.id); }}
                      >
                        <Icon size="xs" type="edit" style={styles.operIcon} />
                        <Link target="_blank" style={{ marginLeft: 20 }} to={`/article/edit/${item.id}`}>
                          <span style={styles.operText}>编辑</span>
                        </Link>
                      </div>
                      <div style={styles.oper} onClick={() => { this.handleRemoveEvent(item.id); }}>
                        <Icon size="xs" type="ashbin" style={styles.operIcon} />
                        <span style={styles.operText}>删除</span>
                      </div>
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
