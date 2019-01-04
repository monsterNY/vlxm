import React, { Component } from 'react';
import Container from '@icedesign/container';
import { Button, Dialog, Input, Form, Upload, Feedback } from '@icedesign/base';
import { withRouter } from 'react-router-dom';
import {
  FormBinderWrapper,
  FormBinder,
  FormError,
} from '@icedesign/form-binder';

const FormItem = Form.Item;
const { CropUpload } = Upload;

@withRouter
export default class AccountPanel extends Component {
  static displayName = 'AccountPanel';

  constructor(props) {
    super(props);
    this.state = {
      open: false,
      faceImg: global.APIConfig.defaultImgUrl,
      userInfo: {},
    };
  }

  componentDidMount() {
    global.APIConfig.sendAuthAjax(this, {}, global.APIConfig.optAuthMethod.GetUserDetail, (data) => {
      this.setState({
        userInfo: data,
        faceImg: data.faceImg ? (global.APIConfig.imgBaseUrl + data.faceImg) : global.APIConfig.defaultImgUrl,
      });
    });
  }

  handleOpenEditPanel = () => {
    this.setState({ open: true });
    this.setState({
      value: {
        faceImg: this.state.userInfo.faceImg,
        displayName: this.state.userInfo.displayName,
        description: this.state.userInfo.description,
      },
    });
    this.faceImg = this.state.userInfo.faceImg;
  };

  handleCloseEditPanel = () => {
    console.log('close');
    this.setState({ open: false });
  };

  formChange = (value) => {
    console.log(value);
  };

  submitEdit = () => {
    // console.log(this.editor.content);
    this.postForm.validateAll((errors, values) => {
      console.log('errors', errors, 'values', values);
      if (errors) {
        return false;
      }

      if (!this.faceImg) {
        Feedback.toast.error('请上传用户头像');
        return false;
      }

      values.faceImg = this.faceImg;

      global.APIConfig.sendAuthAjax(this, values, global.APIConfig.optAuthMethod.UpdateUserInfo, () => {
        Feedback.toast.success('修改成功!');
        this.setState({
          userInfo: {
            faceImg: values.faceImg,
            displayName: values.displayName,
            description: values.description,
          },
          open: false,
        });
      });

      // this.changeProcessStyle(true);

      // this.sendCreateArticleAjax(values);
      // ajax values
    });
  };

  render() {
    const userInfo = this.state.userInfo;

    return (
      <Container>
        <div style={styles.header}>
          <h2 style={styles.title}>账号信息</h2>
          <div>
            <Button onClick={this.handleOpenEditPanel} type="primary">
              修改
            </Button>
          </div>
        </div>
        <div style={styles.infoRow}>
          <div style={styles.infoLabel}>账号类型</div>
          <div style={styles.infoDetail}>博主</div>
        </div>
        <div style={styles.infoRow}>
          <div style={styles.infoLabel}>账号名称</div>
          <div style={styles.infoDetail}>{userInfo.displayName}</div>
        </div>
        <div style={styles.infoRow}>
          {/* <div style={styles.infoLabel}>账号头像</div> */}
          <div style={styles.infoDetail}>
            <img
              src={userInfo.faceImg ? (global.APIConfig.imgBaseUrl + userInfo.faceImg) : global.APIConfig.defaultImgUrl}
              style={{ width: 120 }}
              alt=""
            />
          </div>
        </div>
        <div style={styles.infoRow}>
          <div style={styles.infoLabel}>账号简介</div>
          <div style={styles.infoDetail}>{userInfo.description ? userInfo.description : '暂无'}</div>
        </div>
        <Dialog
          visible={this.state.open}
          onOk={this.submitEdit}
          onClose={this.handleCloseEditPanel}
          onCancel={this.handleCloseEditPanel}
          title="修改账户信息"
        >
          <FormBinderWrapper
            value={this.state.value}
            onChange={this.formChange}
            ref={(refInstance) => {
              this.postForm = refInstance;
            }}
          >
            <div>

              <div style={styles.fromItem}>
                <FormItem label="用户头像" required>
                  <FormBinder>
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
                  </FormBinder>
                </FormItem>
              </div>
              <div style={styles.fromItem}>
                <span>账号名称：</span>
                <FormBinder name="displayName" required max={10} message="不能为空">
                  <Input style={{ width: 500 }} />
                </FormBinder>
              </div>
              <FormError style={{ marginLeft: 10 }} name="displayName" />
              {/* <FormError style={{ marginLeft: 10 }} name="avatar" /> */}
              <div style={styles.fromItem}>
                <span>账号简介：</span>
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
      </Container>
    );
  }
}

const styles = {
  header: {
    display: 'flex',
    justifyContent: 'space-between',
  },
  title: {
    fontSize: 20,
    margin: 0,
    paddingBottom: 20,
  },
  infoRow: {
    padding: '16px 0',
    display: 'flex',
    borderBottom: '1px solid #f6f6f6',
  },
  infoLabel: {
    flex: '0 0 100px',
    color: '#999',
  },
  infoDetail: {},

  fromItem: {
    display: 'flex',
    alignItems: 'flex-start',
    paddingBottom: 10,
  },
};
