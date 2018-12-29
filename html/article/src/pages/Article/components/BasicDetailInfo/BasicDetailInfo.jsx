import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { Grid, Button } from '@icedesign/base';
import { withRouter } from 'react-router-dom';

const { Row, Col } = Grid;

/**
 * 渲染详情信息的数据
 */
const dataSource = {
  title: '集盒家居旗舰店双十一活动',
  shopName: '集盒家居旗舰店',
  amount: '1000.00',
  bounty: '200.00',
  orderTime: '2017-10-18 12:20:07',
  deliveryTime: '2017-10-18 12:20:07',
  phone: '15612111213',
  address: '杭州市文一西路',
  status: '进行中',
  remark: '暂无',
  pics: [
    require('./images/clothes.png'),
    require('./images/dress.png'),
    require('./images/dryer.png'),
    require('./images/quilt.png'),
  ],
};

@withRouter
export default class BasicDetailInfo extends Component {
  static displayName = 'BasicDetailInfo';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      articleData: {},
    };
    this.articleId = this.props.id;
    console.log(this.articleId);
  }

  loadArticleDetail = () => {
    global.APIConfig.sendAjax({
      key: this.articleId,
    }, global.APIConfig.optMethod.GetArticleDetail, (resultData) => {
      this.setState({ articleData: resultData });
    });
  }

  backToListEvent = () => {
    this.props.history.push('/post/list');
  }

  attentionEvent = () => {
    this.props.history.push('/post/list');
  }

  componentDidMount() {
    this.loadArticleDetail();
  }

  render() {
    return (
      <IceContainer>
        <h2 style={styles.basicDetailTitle}>文章详情</h2>

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
          <div style={styles.infoItems} dangerouslySetInnerHTML={{ __html: this.state.articleData.content }} />
        </div>
        <div style={styles.infoColumn}>
          <hr />
          <Row wrap style={styles.infoItems}>
            <Col xxs="24" l="3" style={styles.infoItem}>
              <Button type="primary" onClick={this.backToListEvent}>
                返回列表
              </Button>
            </Col>
            <Col xxs="24" l="3" style={styles.infoItem}>
              <Button type="primary" onClick={this.attentionEvent}>
                关注我
              </Button>
            </Col>
          </Row>
        </div>
      </IceContainer>
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
