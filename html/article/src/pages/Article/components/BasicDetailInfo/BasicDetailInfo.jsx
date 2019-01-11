import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { Grid, Button, Dialog, Form, Input } from '@icedesign/base';
import { withRouter } from 'react-router-dom';
import 'braft-editor/dist/braft.css';
import Comment from '../Comment';
import {
  FormBinderWrapper,
  FormBinder,
  FormError,
} from '@icedesign/form-binder';

const FormItem = Form.Item;
// import './editor.css';

// require('./editor.css');

const { Row, Col } = Grid;
const ButtonGroup = Button.Group;

// /**
//  * 渲染详情信息的数据
//  */
// const dataSource = {
//   title: '集盒家居旗舰店双十一活动',
//   shopName: '集盒家居旗舰店',
//   amount: '1000.00',
//   bounty: '200.00',
//   orderTime: '2017-10-18 12:20:07',
//   deliveryTime: '2017-10-18 12:20:07',
//   phone: '15612111213',
//   address: '杭州市文一西路',
//   status: '进行中',
//   remark: '暂无',
//   pics: [
//     require('./images/clothes.png'),
//     require('./images/dress.png'),
//     require('./images/dryer.png'),
//     require('./images/quilt.png'),
//   ],
// };

@withRouter
export default class BasicDetailInfo extends Component {
  static displayName = 'BasicDetailInfo';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      articleData: {},
      confirmVisible: false,
      likeCount: 0,
      collectCount: 0,
      isAttentionUser: false,
      attentionDialogVisible: false,
      disabledAttention: true,
    };
    this.articleId = this.props.id;
    console.log(this.articleId);
  }

  loadArticleDetail = () => {
    global.APIConfig.sendAjax({
      key: this.articleId,
    }, global.APIConfig.optMethod.GetArticleDetail, (resultData) => {
      this.setState({ articleData: resultData });
      this.authUser = resultData.userId;
      this.searchIsExistsAttentionAjax(this.authUser);
    });
  }

  cancelAttentionUserAjax = (key) => {
    global.APIConfig.sendAuthAjax(this, { key }, global.APIConfig.optAuthMethod.CancelAttentionUser, () => {
      this.setState({ isAttentionUser: false });
    });
  }

  attentionUserAjax = (param) => {
    global.APIConfig.sendAuthAjax(this, param, global.APIConfig.optAuthMethod.AttentionUser, () => {
      this.setState({ isAttentionUser: true, attentionDialogVisible: false });
    }, () => {
      this.setState({ isAttentionUser: false, attentionDialogVisible: false });
    });
  }

  searchIsExistsAttentionAjax = (key) => {
    global.APIConfig.sendAuthAjax(this, {
      key,
    }, global.APIConfig.optAuthMethod.SearchIsExistsAttention, (resultData) => {
      this.setState({ isAttentionUser: resultData, disabledAttention: false });
    });
  }

  getActionCount = (actionKey, callback) => {
    global.APIConfig.sendAuthAjax(this, {
      articleId: this.articleId,
      actionKey,
    }, global.APIConfig.optAuthMethod.SelectAction, (resultData) => {
      if (callback) {
        callback(resultData);
      }
    });
  }

  actionHandle = (actionKey, callback) => {
    global.APIConfig.sendAuthAjax(this, {
      articleId: this.articleId,
      actionKey,
    }, global.APIConfig.optAuthMethod.SingleAction, (data) => {
      if (callback) {
        callback(data);
      }
    });
  }

  likeHandle = () => {
    this.actionHandle('like', () => {
      if (this.state.likeCount > 0) {
        this.setState({ likeCount: 0 });
      } else {
        this.setState({ likeCount: 1 });
      }
    });
  }

  collectHandle = () => {
    this.actionHandle('collect', () => {
      if (this.state.collectCount > 0) {
        this.setState({ collectCount: 0 });
      } else {
        this.setState({ collectCount: 1 });
      }
    });
  }

  backToListEvent = () => {
    this.props.history.push('/post/list');
  }

  editEvent = () => {
    console.log('editEvent');
  }

  removeEvent = () => {
    this.setState({
      confirmVisible: true,
    });
  }

  addPvEvent = () => {
    global.APIConfig.sendAjax({
      key: this.articleId,
    }, global.APIConfig.optMethod.AddArticlePv, () => {
      console.log('addPvEvent');
    });
  }

  attentionEvent = () => {
    this.props.history.push('/post/list');
  }

  hideConfirm = () => {
    this.setState({
      confirmVisible: false,
    });
  }

  attentionHandle = () => {
    if (!this.authUser) {
      return;
    }
    if (this.state.isAttentionUser) {
      this.cancelAttentionUserAjax(this.authUser);
    } else {
      this.setState({ attentionDialogVisible: true });
    }
  }

  handleCloseAttentionPanel = () => {
    this.setState({ attentionDialogVisible: false });
  }

  removeHandler = () => {
    console.log('removeHandler');
  }

  submitAttentionPanelEvent = () => {
    this.postAttentionForm.validateAll((errors, values) => {
      console.log('errors', errors, 'values', values);
      if (errors) {
        return false;
      }

      values.attentionUser = this.authUser;

      console.log(values);

      this.attentionUserAjax(values);
    });
  }

  componentDidMount() {
    this.loadArticleDetail();
    this.getActionCount('like', (data) => {
      this.setState({ likeCount: data });
    });
    this.getActionCount('collect', (data) => {
      this.setState({ collectCount: data });
    });
    this.addPvEvent();
  }

  render() {
    return (
      <IceContainer>
        <Dialog
          visible={this.state.confirmVisible}
          onOk={this.removeHandler}
          onClose={this.hideConfirm}
          onCancel={this.hideConfirm}
          title="是否确定删除此信息？"
        />
        <h2 style={styles.basicDetailTitle}>文章详情</h2>

        <div>
          <img src={this.state.articleData.faceImg ? (global.APIConfig.imgBaseUrl + this.state.articleData.faceImg) : global.APIConfig.defaultImgUrl} alt="封面" />
        </div>

        <div style={styles.infoColumn}>
          <h5 style={styles.infoColumnTitle}>基本信息</h5>
          <Row wrap style={styles.infoItems}>
            <Col xxs="24" l="12" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>文章标题：</span>
              <span style={styles.infoItemValue}>{this.state.articleData.title}</span>
            </Col>
            <Col xxs="24" l="12" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>作者笔名：</span>
              <span style={styles.infoItemValue}> {this.state.articleData.author}</span>
            </Col>
            {/* <Col xxs="24" l="12" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>文章标签：</span>
              <span style={styles.infoItemValue}>{this.state.articleData.category}</span>
            </Col>
            <Col xxs="24" l="12" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>文章类型：</span>
              <span style={styles.infoItemValue}>{this.state.articleData.articleType}</span>
            </Col> */}
            <Col xxs="24" l="12" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>发布时间：</span>
              <span style={styles.infoItemValue}>{this.state.articleData.publishTime}</span>
            </Col>
            <Col xxs="24" l="24" style={styles.infoItem}>
              <span style={styles.infoItemLabel}>文章描述：</span>
              <span style={styles.infoItemValue}> {this.state.articleData.description}</span>
            </Col>
          </Row>
        </div>
        <div style={styles.infoColumn}>
          <h5 style={styles.infoColumnTitle}>正文</h5>
          <div style={styles.infoItems} className="BraftEditor-container" id="editor" dangerouslySetInnerHTML={{ __html: this.state.articleData.content }} />
        </div>
        <div id="articleComment">
          <Comment
            id={this.props.match.params.id}
            flag={global.APIConfig.optAuthMethod.InsertArticleComment}
          />
        </div>
        <div style={styles.infoColumn}>
          <hr />
          <Row wrap style={styles.infoItems}>
            <Col xxs="24" l="12" style={styles.infoItem}>
              <ButtonGroup>
                <Button type="primary" onClick={this.backToListEvent}>
                  返回列表
                </Button>
                <Button type="primary" onClick={this.likeHandle}>
                  {this.state.likeCount > 0 ? '取消点赞' : '点赞'}
                </Button>
                <Button type="primary" onClick={this.collectHandle}>
                  {this.state.collectCount > 0 ? '取消收藏' : '收藏'}
                </Button>
                <Button disabled={this.state.disabledAttention} type="primary" onClick={this.attentionHandle}>
                  {this.state.isAttentionUser ? '取消关注' : '关注'}
                </Button>
              </ButtonGroup>
            </Col>
            <Col xxs="24" l="7" />
            <Col xxs="24" l="3" style={styles.infoItem}>
              <span>
                已浏览{this.state.articleData.pageView}次
              </span>
            </Col>
            {/* <Col xxs="24" l="3" style={styles.infoItem}>
              <Button type="primary" onClick={this.editEvent}>
                编辑文章
              </Button>
            </Col>
            <Col xxs="24" l="3" style={styles.infoItem}>
              <Button type="primary" onClick={this.removeEvent}>
                删除文章
              </Button>
            </Col> */}

          </Row>
        </div>

        <Dialog
          visible={this.state.attentionDialogVisible}
          onOk={this.submitAttentionPanelEvent}
          onClose={this.handleCloseAttentionPanel}
          onCancel={this.handleCloseAttentionPanel}
          title="关注用户"
        >
          <FormBinderWrapper
            value={this.state.attentionValue}
            // onChange={this.formChange}
            ref={(refInstance) => {
              this.postAttentionForm = refInstance;
            }}
          >
            <div>
              <div style={styles.fromItem}>
                <span>分组名称：</span>
                <FormBinder name="GroupKey" required max={10} message="不能为空">
                  <Input style={{ width: 500 }} />
                </FormBinder>
              </div>
              <FormError style={{ marginLeft: 10 }} name="GroupKey" />
              {/* <FormError style={{ marginLeft: 10 }} name="avatar" /> */}
              <div style={styles.fromItem}>
                <span>关注描述：</span>
                <FormBinder name="description" max={200} message="">
                  <Input
                    multiple
                    hasLimitHint
                    maxLength={200}
                    style={{ width: 500 }}
                  />
                </FormBinder>
              </div>
              <FormError style={{ marginLeft: 10 }} name="description" />
            </div>
          </FormBinderWrapper>
        </Dialog>

      </IceContainer >
    );
  }
}

const styles = {
  basicDetailTitle: {
    margin: '10px 0',
    fontSize: '16px',
  },
  infoColumn: {
    marginLeft: '16px',
  },
  infoColumnTitle: {
    margin: '20px 0',
    paddingLeft: '10px',
    borderLeft: '3px solid #3080fe',
  },
  infoItems: {
    padding: 0,
    marginLeft: '25px',
  },
  infoItem: {
    marginTop: '10px',
    marginBottom: '18px',
    listStyle: 'none',
    fontSize: '14px',
  },
  infoItemLabel: {
    minWidth: '70px',
    color: '#999',
  },
  infoItemValue: {
    color: '#333',
  },
  attachLabel: {
    minWidth: '70px',
    color: '#999',
    float: 'left',
  },
  attachPics: {
    width: '80px',
    height: '80px',
    border: '1px solid #eee',
    marginRight: '10px',
  },
};
