import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import { withRouter } from 'react-router-dom';
import { Input, Grid, Form, Button, Select, Feedback, Radio, Progress, Upload, DatePicker } from '@icedesign/base';
import {
  FormBinderWrapper as IceFormBinderWrapper,
  FormBinder as IceFormBinder,
  FormError as IceFormError,
} from '@icedesign/form-binder';

import BraftEditor from '../../../../layouts/CommonLayout/components/BraftEditor';

const { Row, Col } = Grid;
const FormItem = Form.Item;
const { Group: RadioGroup } = Radio;
const { CropUpload } = Upload;

@withRouter
export default class ContentEditor extends Component {
  static displayName = 'ContentEditor';

  static propTypes = {};

  static defaultProps = {};

  // 加载文章类型
  loadArticleTypeList = () => {
    global.APIConfig.sendAjax({
      pageSize: 15,
      pageNo: 1,
      result: {
        validFlag: 1,
      },
    }, global.APIConfig.optMethod.GetArticleTypePageList, (data) => {
      if (data.result) {
        // 重写数据结构
        const arr = data.result.map((item) => {
          return {
            label: item.typeName,
            value: item.id.toString(),
          };
        });
        // 刷新渲染
        this.setState({ articleTypeArr: arr });
      }
    });
  }

  // 加载文章标签
  loadArticleTagList = () => {
    global.APIConfig.sendAjax({
      pageSize: 30,
      pageNo: 1,
      result: {
        validFlag: 1,
      },
    }, global.APIConfig.optMethod.GetArticleTagPageList, (data) => {
      if (data.result) {
        // 重写数据结构
        const arr = data.result.map((item) => {
          return {
            label: item.tagName,
            value: item.id.toString(),
          };
        });
        // 刷新渲染
        this.setState({ articleTagArr: arr });
      }
    });
  };

  sendCreateArticleAjax = (param) => {
    global.APIConfig.sendAuthAjax(this, param, global.APIConfig.optMethod.InsertArticle, () => {
      this.setState({ process: 100 });
      setTimeout(() => {
        Feedback.toast.success('提交成功');
        this.props.history.push('/post/list');
      }, 1000);
    }, () => {
      this.changeProcessStyle(false);
    });
  }

  componentDidMount() {
    this.loadArticleTagList();
    this.loadArticleTypeList();
  }

  constructor(props) {
    super(props);
    this.state = {
      value: {
        title: '',
        desc: '',
        author: '',
        content: '',
        status: 0,
      },
      faceImg: global.APIConfig.defaultImgUrl,
      articleTypeArr: [],
      articleTagArr: [],
      process: 0,
    };
    this.btnStyle = {};
    this.processStyle = global.CusStyle.hideStyle;
  }

  changeProcessStyle = (isShow) => {
    if (isShow) {
      this.processStyle = {};
      this.btnStyle = global.CusStyle.hideStyle;
    } else {
      this.btnStyle = {};
      this.processStyle = global.CusStyle.hideStyle;
    }
    this.setState({
      process: 0,
    });
  }

  formChange = (value) => {
    console.log('value', value);
    this.setState({
      value,
    });
  };

  handleEditorRef = (ref) => {
    this.editor = ref;
  }

  handleSubmit = () => {
    // console.log(this.editor.content);
    this.postForm.validateAll((errors, values) => {
      console.log('errors', errors, 'values', values);
      if (errors) {
        return false;
      }

      if (!this.faceImg) {
        Feedback.toast.error('请上传文章封面图片');
        return false;
      }

      values.content = this.editor.content;
      values.faceImg = this.faceImg;
      values.userId = global.APIConfig.userInfo.id;

      this.changeProcessStyle(true);

      this.sendCreateArticleAjax(values);
      // ajax values
    });
  };

  render() {
    return (
      <div className="content-editor">
        <IceFormBinderWrapper
          ref={(refInstance) => {
            this.postForm = refInstance;
          }}
          value={this.state.value}
          onChange={this.formChange}
        >
          <IceContainer title="文章发布">
            <Form labelAlign="top" style={styles.form}>
              <Row>
                <Col span="11">
                  <FormItem label="封面图片" required>
                    <IceFormBinder>
                      <CropUpload
                        action={global.APIConfig.uploadUrl}
                        name="file"
                        preview
                        previewList={[80, 60, 40]}
                        minCropBoxSize={100}
                        onSuccess={(data) => {
                          if (data.errorCode === global.APIConfig.resultCodeMap.success) {
                            this.setState({
                              faceImg: (global.APIConfig.imgBaseUrl + data.result),
                            });
                            this.faceImg = data.result;
                          } else {
                            console.log(data);
                          }
                        }}
                      >
                        <div style={{ marginTop: '20px' }}>
                          <img
                            ref="targetViewer"
                            alt=""
                            src={this.state.faceImg}
                            width="120px"
                            height="120px"
                          />
                        </div>
                      </CropUpload>
                    </IceFormBinder>
                  </FormItem>
                </Col>
              </Row>
              <Row>
                <Col span="11">
                  <FormItem label="标题" required>
                    <IceFormBinder name="title" required message="标题必填">
                      <Input placeholder="这里填写文章标题" />
                    </IceFormBinder>
                    <IceFormError name="title" />
                  </FormItem>
                </Col>
                <Col span="11" offset="2">
                  <FormItem label="文章标签" required>
                    <IceFormBinder
                      name="category"
                      required
                      type="array"
                      message="标签必填支持多个"
                    >
                      <Select
                        style={styles.cats}
                        multiple
                        placeholder="请选择标签"
                        dataSource={this.state.articleTagArr}
                      />
                    </IceFormBinder>
                    <IceFormError
                      name="category"
                      render={(errors) => {
                        console.log('errors', errors);
                        return (
                          <div>
                            <span style={{ color: 'red' }}>
                              {errors.map(item => item.message).join(',')}
                            </span>
                            <span style={{ marginLeft: 10 }}>
                              不知道选择什么分类？请 <a href="#">点击这里</a>{' '}
                              查看
                            </span>
                          </div>
                        );
                      }}
                    />
                  </FormItem>
                </Col>
              </Row>
              <Row>
                <Col span="11">
                  <FormItem label="作者(笔名)" required>
                    <IceFormBinder
                      name="author"
                      required
                      message="作者信息必填"
                    >
                      <Input placeholder="填写作者名称" />
                    </IceFormBinder>
                    <IceFormError name="author" />
                  </FormItem>
                </Col>
                <Col span="11" offset="2">
                  <FormItem label="文章类型" required>
                    <IceFormBinder
                      name="articleType"
                      required
                      message="请选择文章类型"
                    >
                      <Select
                        style={styles.cats}
                        placeholder="请选择文章类型"
                        dataSource={this.state.articleTypeArr}
                      />
                    </IceFormBinder>
                  </FormItem>
                </Col>
              </Row>
              <FormItem label="描述">
                <IceFormBinder name="description">
                  <Input multiple placeholder="这里填写正文描述" />
                </IceFormBinder>
              </FormItem>
              <FormItem label="正文" required>
                <IceFormBinder name="body">
                  <BraftEditor bindRef={this.handleEditorRef} />
                </IceFormBinder>
              </FormItem>
              <Row>
                <Col span="11">
                  <FormItem label="发布时间" required>
                    <IceFormBinder name="publishTime_real">
                      {/* 存在时区问题。。。转换时会减少时区数 */}
                      <DatePicker
                        onChange={(val, str) => {
                          // fix bug
                          console.log(val, str);
                          const value = this.state.value;
                          value.publishTime = str;
                          this.setState(value);
                        }}
                        name="publishTime_real"
                        size="large"
                        style={{ width: '400px' }}
                      />
                    </IceFormBinder>
                  </FormItem>
                </Col>
              </Row>
              <Row>
                <Col span="11">
                  <FormItem label="状态" required>
                    <IceFormBinder>
                      <RadioGroup
                        name="status"
                        dataSource={[
                          {
                            value: 0,
                            label: '免费',
                          },
                          {
                            value: 1,
                            label: '收费',
                          },
                        ]}
                      />
                    </IceFormBinder>
                  </FormItem>
                </Col>
              </Row>
              <Row style={this.processStyle}>
                <Col offset="3">
                  <Progress percent={this.state.process} />
                </Col>
              </Row>
              <FormItem label=" " style={this.btnStyle}>
                <Button type="primary" onClick={this.handleSubmit}>
                  发布文章
                </Button>
              </FormItem>
            </Form>
          </IceContainer>
        </IceFormBinderWrapper>
      </div>
    );
  }
}

const styles = {
  form: {
    marginTop: 30,
  },
  cats: {
    width: '100%',
  },
};
