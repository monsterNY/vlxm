import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import AutoResponsive from 'autoresponsive-react';
import { Tab, Pagination } from '@icedesign/base';
import './Waterfall.scss';

export default class Waterfall extends Component {
  static displayName = 'Waterfall';

  static propTypes = {
  };

  static defaultProps = {
  };

  constructor(props) {
    super(props);
    this.state = {
      articleTypeArr: [],
      articleInfoArr: [],
    };
    this.pageParm = {
      pageSize: 16,
      pageNo: 1,
      total: 0,
      result: {
        // 有效
        validFlag: 1,
        // 全部
        filterType: 0,
      },
    };
    this.contentRef = React.createRef();
    this.diffWidth = 400;
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
        const arr = resultData.result;
        arr.push({
          typeName: '全部',
          icon: 'icon/fiy.png',
          id: 0,
        });
        // 刷新渲染
        this.setState({ articleTypeArr: arr });
      }
    });
  }

  handleTabChange = (key) => {
    if (key) {
      this.pageParm.result.articleType = key;
      this.loadArticlePageList(this.pageParm);
    }
    console.log(key);
  }

  componentDidMount() {
    this.loadArticleTypeList();
    this.loadArticlePageList(this.pageParm);
  }

  // 加载文章列表
  loadArticlePageList = (param) => {
    global.APIConfig.sendAjax(param, global.APIConfig.optMethod.GetArticlePageList, (resultData) => {
      if (resultData.result) {
        this.pageParm.pageNo = resultData.pageNo;
        this.pageParm.pageSize = resultData.pageSize;
        this.pageParm.total = resultData.count;

        // 刷新渲染
        this.setState({ articleInfoArr: resultData.result });

        // 绑定自适应事件
        window.addEventListener(
          'resize',
          () => {
            this.setState({
              containerWidth: (this.contentRef.clientWidth - this.diffWidth),
            });
          },
          false
        );
      }
    });
  }

  handlePagination = (currnet) => {
    this.pageParm.pageNo = currnet;
    this.loadArticlePageList(this.pageParm);
  }

  getAutoResponsiveProps = () => {
    return {
      itemMargin: 10,
      containerWidth: (this.state.containerWidth || document.body.clientWidth) - this.diffWidth,
      itemClassName: 'item',
      gridWidth: 100,
      transitionDuration: '.5',
    };
  };

  render() {
    return (
      <div>
        <div style={styles.titleWrapper}>
          <span
            style={{
              fontSize: 16,
              fontWeight: 'bold',
              color: '#333',
              paddingRight: 20,
            }}
          >
            作品列表
          </span>
          <span style={{ fontSize: 12, color: '#999' }}>
            内容质量与粉丝效果好的作品可以得到更多频道曝光?
          </span>
        </div>
        <Tab
          onChange={this.handleTabChange}
          navStyle={{
            backgroundColor: '#fff',
            paddingTop: '20px',
          }}
          contentStyle={{
            backgroundColor: '#fff',
            marginTop: '20px',
            borderRadius: '6px',
          }}
        >
          {this.state.articleTypeArr.map((tab) => {
            return (
              <Tab.TabPane
                tabStyle={{ height: 60, padding: '0 15px' }}
                key={tab.id}
                tab={
                  <div style={styles.navItemWraper}>
                    <img
                      alt={tab.typeName}
                      src={global.APIConfig.imgBaseUrl + tab.icon}
                      style={{ width: 30, marginRight: 8 }}
                    />
                    {tab.typeName}
                  </div>
                }
              >
              </Tab.TabPane>
            );
          })}
        </Tab>
        <IceContainer>
          <div className="waterfall-panel" style={styles.content}>
            <AutoResponsive ref={this.contentRef} {...this.getAutoResponsiveProps()}>
              {this.state.articleInfoArr.map((item, index) => {
                const style = {
                  // width: item.status === 0 ? 190 : 390,
                  // height: item.status === 0 ? 240 : 490,
                  width: 190,
                  height: 240,
                };
                return (
                  <a
                    key={index}
                    href={`/#/article/detail/${item.id}`}
                    className={`${item.status === 0 ? 'w1' : 'w1'} album item`}
                    style={style}
                  >
                    <img
                      className="a-cont"
                      src="https://img.alicdn.com/tps/TB19O79MVXXXXcZXVXXXXXXXXXX-1024-1024.jpg"
                      alt=""
                    />
                    <img className="a-cover" src={`${global.APIConfig.imgBaseUrl}${item.faceImg}`} alt={item.description} />
                    <p className="a-layer">
                      <span className="al-brand">{item.author}</span>
                      <span className="al-title">{`《-${item.title}-》`}</span>
                      {/* <span className="al-count">{item.status === 0?}件商品</span> */}
                    </p>
                  </a>
                );
              })}
            </AutoResponsive>
            {/* 参数参考：https://ant.design/components/pagination-cn/ */}
            <Pagination
              style={styles.pagination}
              current={this.pageParm.pageNo}
              onChange={this.handlePagination}
              total={this.pageParm.total}
              pageSize={this.pageParm.pageSize}
            />
          </div>
        </IceContainer>

      </div>
    );
  }
}

const styles = {
  content: {
  },
};
