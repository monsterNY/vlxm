import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { Rating, Pagination, Button, Progress, Feedback, Grid } from '@icedesign/base';
import BraftEditor from '../../../../layouts/CommonLayout/components/BraftEditor';

const { Row, Col } = Grid;
const ButtonGroup = Button.Group;

export default class Comment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      grade: 5,
      process: 0,
      dataList: [],
      replyItem: null,
    };
    this.flagId = this.props.id;
    this.actionFlag = this.props.flag;
    console.log(`flagId:${this.flagId},actionFlag:${this.actionFlag}`);
    this.pageParm = {
      pageSize: 10,
      pageNo: 1,
      total: 0,
      result: this.flagId,
    };
    this.btnStyle = { float: 'right' };
    this.processStyle = global.CusStyle.hideStyle;
  }

  componentDidMount() {
    this.loadArticleCommentPageList(this.pageParm);
  }

  // 加载文章列表
  loadArticleCommentPageList = (param) => {
    global.APIConfig.sendAjax(param, global.APIConfig.optMethod.GetArticleCommentPageList, (resultData) => {
      if (resultData.result) {
        this.pageParm.pageNo = resultData.pageNo;
        this.pageParm.pageSize = resultData.pageSize;
        this.pageParm.total = resultData.count;

        // 刷新渲染
        this.setState({ dataList: resultData.result });
      }
    });
  }

  changeProcessStyle = (isShow) => {
    if (isShow) {
      this.processStyle = {};
      this.btnStyle = global.CusStyle.hideStyle;
    } else {
      this.btnStyle = { float: 'right' };
      this.processStyle = global.CusStyle.hideStyle;
    }
    this.setState({
      process: 0,
    });
  }

  createCommentEvent = (param) => {
    global.APIConfig.sendAuthAjax(this, param, this.actionFlag, () => {
      this.setState({ process: 100 });
      if (this.state.replyItem) {
        // this.state.replyItem.replyCount += 1;
      } else {
        this.pageParm.pageNo = 1;
        this.loadArticleCommentPageList(this.pageParm);// 刷新评论
      }
      this.setState({ replyItem: null });
      setTimeout(() => {
        Feedback.toast.success('评论成功！');
        console.log(this.editor);
        this.editor.editorRef.current.clear();
        this.changeProcessStyle(false);
      }, 1000);
    }, () => {
      this.changeProcessStyle(false);
    });
  }

  commentHandler = () => {
    if (!this.editor.content) {
      Feedback.toast.error('评论内容不能为空！');
      return;
    }

    const param = {
      content: this.editor.content,
      joinKey: this.flagId,
      grade: this.state.grade,
      contentType: 0,
    };
    if (this.state.replyItem) {
      param.replyId = this.state.replyItem.id;
    }

    this.changeProcessStyle(true);
    this.createCommentEvent(param);
  }

  handleEditorRef = (ref) => {
    this.editor = ref;
  }

  handleGradeChange = (grade) => {
    // console.log(grade);
    this.setState({ grade });
  }

  handlePagination = (currentPage) => {
    this.pageParm.pageNo = currentPage;
    this.loadArticleCommentPageList(this.pageParm);
  }

  getReplyEvent = (param) => {
    global.APIConfig.sendAjax(param, global.APIConfig.optMethod.GetReplyCommentPageList, (resultData) => {
      if (resultData.result) {
        console.log(resultData.result);
      }
    });
  }

  replayCommentHandle = (item) => {
    console.log(item.id);
    // console.log(item);
    this.setState({ replyItem: item });
    this.editor.editorRef.current.focus();
  }

  renderReply = () => {
    if (this.state.replyItem) {
      return (
        <div>
          @{this.state.replyItem.nickName}
          <Button
            style={{ marginLeft: 10 }}
            onClick={() => {
              this.setState({ replyItem: null });
            }}
            shape="text"
          >
            取消
          </Button>
        </div>
      );
    }
  }

  renderItem = (item, idx) => {
    return (
      <div style={styles.item} key={idx}>
        <div style={styles.itemRow}>
          <span style={styles.title}>
            <img src={item.faceImg ? (global.APIConfig.imgBaseUrl + item.faceImg) : global.APIConfig.defaultImgUrl} style={styles.avatar} alt="avatar" />
            {item.nickName}
            <Rating
              defaultValue={item.grade}
              size="small"
              disabled
            />
          </span>
          <span style={styles.status}>{item.createTime}</span>
        </div>
        <div style={styles.infoItems} className="BraftEditor-container" dangerouslySetInnerHTML={{ __html: item.content }} />
        <Row wrap style={{ marginTop: 10, marginBottom: 10 }}>
          <Col xxs="24" l="16" />
          <Col xxs="24" l="6">
            <ButtonGroup>
              <Button type="primary" onClick={() => this.replayCommentHandle(item)}>
                回复评论
              </Button>
              <Button type="primary"
                onClick={() => {
                  if (item.replyCount > 0) {
                    this.getReplyEvent({
                      PageSize: 10,
                      pageIndex: 1,
                      result: item.id,
                    });
                  } else {
                    console.log('no reply');
                  }
                }}
              >
                查看回复({item.replyCount})
              </Button>
            </ButtonGroup>
          </Col>
        </Row>

      </div>
    );
  }

  render() {
    return (
      <IceContainer>

        <div style={styles.titleRow}>
          <h2 style={styles.cardTitle}>评论列表</h2>
          <span style={styles.status}>共{this.pageParm.total}条评论</span>
        </div>
        {this.state.dataList.map(this.renderItem)}
        {/* <div style={styles.allMessage}>
          <a href="##">查看全部消息</a>
        </div> */}
        <Pagination
          style={styles.pagination}
          current={this.pageParm.pageNo}
          onChange={this.handlePagination}
          total={this.pageParm.total}
          pageSize={this.pageParm.pageSize}
        />

        {/* <FormItem label="正文" required> */}
        {/* <IceFormBinder name="body"> */}
        {this.renderReply()}
        <div id="commentContent">
          <BraftEditor bindRef={this.handleEditorRef} controls={['italic', 'underline', 'separator', 'link', 'separator', 'emoji', 'hr', 'blockquote', 'code', 'split', 'link', 'clear']} />
          <div style={this.processStyle}>
            <Progress percent={this.state.process} />
          </div>
          <div style={this.btnStyle}>
            <Rating
              defaultValue={this.state.grade}
              onChange={this.handleGradeChange}
              // showInfo={val => val}
              size="large"
            // styles={{ marginLeft: 100 }}
            />
            <Button type="primary" style={{ marginLeft: 60, marginRight: 60 }} onClick={this.commentHandler}>
              发布评论
            </Button>
          </div>
        </div>
        {/* </IceFormBinder> */}
        {/* </FormItem> */}

      </IceContainer>
    );
  }
}

const styles = {
  pagination: {
    textAlign: 'center',
    marginTop: 10,
    marginBottom: '10px',
  },
  titleRow: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: '15px',
  },
  cardTitle: {
    margin: 0,
    fontSize: '18px',
    display: 'inline-flex',
  },
  title: {
    fontSize: '14px',
    display: 'inline-flex',
    lineHeight: '22px',
  },
  status: {
    display: 'flex',
    alignItems: 'center',
    color: '#999',
    fontSize: '12px',
  },
  itemRow: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  avatar: {
    width: '24px',
    height: '24px',
    borderRadius: '50%',
    marginRight: '10px',
  },
  item: {
    display: 'flex',
    flexDirection: 'column',
    paddingTop: '15px',
    borderBottom: '1px solid #fafafa',
  },
  message: {
    color: '#999',
    fontSize: '12px',
    paddingLeft: '34px',
    marginBottom: '15px',
    lineHeight: '22px',
  },
  allMessage: {
    textAlign: 'center',
    height: '50px',
    lineHeight: '50px',
  },
};
