import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { Grid, Pagination } from '@icedesign/base';
import Masonry from 'react-masonry-component';
import { Link } from 'react-router-dom';

const { Row, Col } = Grid;

const dataSource = [
  {
    img: require('./images/ice-design-analysis.png'),
    title: 'ice design analysis',
  },
  {
    img: require('./images/ice-design-cms.png'),
    title: 'ice design cms',
  },
  {
    img: require('./images/ice-design-dashboard.png'),
    title: 'ice design dashboard',
  },
  {
    img: require('./images/ice-design-ecommerce.png'),
    title: 'ice design ecommerce',
  },
  {
    img: require('./images/ice-open-platform.png'),
    title: 'ice open platform',
  },
  {
    img: require('./images/ice-website-homepage.png'),
    title: 'ice website homepage',
  },
  {
    img: require('./images/iceworks-homepage.png'),
    title: 'iceworks homepage',
  },
  {
    img: require('./images/ice-design-school.png'),
    title: 'ice design school',
  },
  {
    img: require('./images/ice-creator-landingpage.png'),
    title: 'ice creator landingpage',
  },
];

export default class CustomMasonry extends Component {
  static displayName = 'CustomMasonry';

  static defaultProps = {
    dataSource,
  };

  constructor(props) {
    super(props);
    this.state = {
      articleArr: [],
    };
    this.pageParm = {
      pageSize: 18,
      pageNo: 1,
      total: 0,
      result: {
        filterType: 2,
      },
    };
  }

  componentDidMount() {
    this.loadArticlePageList(this.pageParm);
  }

  handlePagination = (currnet) => {
    this.pageParm.pageNo = currnet;
    this.loadArticlePageList(this.pageParm);
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


  render() {
    const childElements = this.state.articleArr.map((item, index) => {
      return (
        <Col l="4" key={index}>
            <div style={styles.itemBody}>
              <img src={global.APIConfig.imgBaseUrl + item.faceImg} style={styles.itemImg} alt="" />
              <Link to={`/article/detail/${item.id}`} target="_blank">
                <h3 style={styles.itemTitle}>{item.title}</h3>
              </Link>
            </div>
        </Col>
      );
    });

    const masonryOptions = {
      transitionDuration: 0,
    };

    return (
      <IceContainer style={styles.container}>
        <Row wrap>
          <Masonry options={masonryOptions} style={{ width: '100%' }}>
            {childElements}
          </Masonry>
        </Row>
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
  container: {
    background: '#F7F8FA',
  },
  itemBody: {
    margin: '10px',
    padding: '10px',
    background: '#fff',
    cursor: 'pointer',
  },
  itemImg: {
    maxWidth: '100%',
  },
  itemTitle: {
    margin: 0,
    padding: '10px 0 0',
    fontSize: '15px',
    textTransform: 'uppercase',
  },
  pagination: {
    marginTop: '20px',
    textAlign: 'right',
  },
};
