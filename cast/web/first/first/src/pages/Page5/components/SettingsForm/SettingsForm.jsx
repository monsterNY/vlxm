/* eslint  react/no-string-refs: 0 */
import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import ComplexDialog from '../ComplexDialog/ComplexDialog';
import { Input, Button, Radio, Switch, Upload, Grid, Progress } from '@icedesign/base';
import {
  FormBinderWrapper as IceFormBinderWrapper,
  FormBinder as IceFormBinder,
  FormError as IceFormError,
} from '@icedesign/form-binder';
import './SettingsForm.scss';

const { Row, Col } = Grid;
const { Group: RadioGroup } = Radio;
// const { ImageUpload } = Upload;

// function beforeUpload(info) {
//   console.log('beforeUpload callback : ', info);
// }

// function onChange(info) {
//   console.log('onChane callback : ', info);
// }

// function onSuccess(res, file) {
//   console.log('onSuccess callback : ', res, file);
// }

// function onError(file) {
//   console.log('onError callback : ', file);
// }

export default class SettingsForm extends Component {
  static displayName = 'SettingsForm';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      value: {
        title: '',
        author: '',
        articleType: 1,
        notice: false,
      },
      process: 0,
    };

    this.showStyle = { marginTop: 20 };

    this.hideStyle = { display: 'none' };

    this.btnSubmitStyle = this.showStyle;

    this.processStyle = this.hideStyle;
  }

  onDragOver = () => {
    console.log('dragover callback');
  };

  onDrop = (fileList) => {
    console.log('drop callback : ', fileList);
  };

  formChange = (value) => {
    // console.log('value', value);
    this.setState({
      value,
    });
  };

  validateAllFormField = () => {
    this.refs.form.validateAll((errors, values) => {

      if (!errors) {
        this.processStyle = this.showStyle;
        this.btnSubmitStyle = this.hideStyle;

        this.setState({
          process: 0,
        });

        global.APIConfig.sendAjax(values, global.APIConfig.optMethod.InsertArticle, (data) => {
          this.setState({
            process: 100,
          });
          setTimeout(() => {
            this.dialog.showDialog();
            console.log(data);
          }, 1000);
        });
      }
      console.log('errors', errors, 'values', values);
    });
  };

  bindDialog = (ref) => {
    console.log(ref);
    this.dialog = ref;
  }

  render() {
    return (
      <div className="settings-form">
        <ComplexDialog
          getRef={this.bindDialog}
        />
        <IceContainer>
          <IceFormBinderWrapper
            value={this.state.value}
            onChange={this.formChange}
            ref="form"
          >
            <div style={styles.formContent}>
              <h2 style={styles.formTitle}>基本设置</h2>

              <Row style={styles.formItem}>
                <Col xxs="6" s="3" l="3" style={styles.label}>
                  作者：
                </Col>
                <Col s="12" l="10">
                  <IceFormBinder name="author" required max={16} message="必填">
                    <Input size="large" placeholder="monster" />
                  </IceFormBinder>
                  <IceFormError name="author" />
                </Col>
              </Row>

              <Row style={styles.formItem}>
                <Col xxs="6" s="3" l="3" style={styles.label}>
                  标题：
                </Col>
                <Col s="12" l="10">
                  <IceFormBinder name="title" required max={16} message="必填">
                    <Input size="large" placeholder="emm..." />
                  </IceFormBinder>
                  <IceFormError name="title" />
                </Col>
              </Row>
              <Row style={styles.formItem}>
                <Col xxs="6" s="3" l="3" style={styles.label}>
                  文章类型：
                </Col>
                <Col s="12" l="10">
                  <IceFormBinder name="articleType" required message="必填">
                    <RadioGroup>
                      <Radio value={1}>生活</Radio>
                      <Radio value={2}>科技</Radio>
                    </RadioGroup>
                  </IceFormBinder>
                  <IceFormError name="articleType" />
                </Col>
              </Row>

              <Row style={styles.formItem}>
                <Col xxs="6" s="3" l="3" style={styles.label}>
                  立即发布：
                </Col>
                <Col s="12" l="10">
                  <IceFormBinder type="boolean" name="notice">
                    <Switch />
                  </IceFormBinder>
                  <IceFormError name="notice" />
                </Col>
              </Row>
            </div>
          </IceFormBinderWrapper>

          <Row style={this.btnSubmitStyle}>
            <Col offset="3">
              <Button
                size="large"
                type="primary"
                style={{ width: 100 }}
                onClick={this.validateAllFormField}
              >
                提 交
              </Button>
            </Col>
          </Row>

          <Row style={this.processStyle}>
            <Col offset="3">
              <Progress percent={this.state.process} />
            </Col>
          </Row>

        </IceContainer>
      </div>
    );
  }
}

const styles = {
  label: {
    textAlign: 'right',
  },
  formContent: {
    width: '100%',
    position: 'relative',
  },
  formItem: {
    alignItems: 'center',
    marginBottom: 25,
  },
  formTitle: {
    margin: '0 0 20px',
    paddingBottom: '10px',
    borderBottom: '1px solid #eee',
  },
};
