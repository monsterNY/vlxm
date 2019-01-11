/* eslint react/no-string-refs:0 */
import React, { Component } from 'react';
import { withRouter, Link } from 'react-router-dom';
import { Input, Button, Grid, Feedback } from '@icedesign/base';
import {
  FormBinderWrapper as IceFormBinderWrapper,
  FormBinder as IceFormBinder,
  FormError as IceFormError,
} from '@icedesign/form-binder';
import IceIcon from '@icedesign/icon';

const { Row, Col } = Grid;

@withRouter
class UserRegister extends Component {
  static displayName = 'UserRegister';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      value: {
        name: '',
        email: '',
        passwd: '',
        rePasswd: '',
      },
    };
  }

  checkPasswd = (rule, values, callback) => {
    if (!values) {
      callback('请输入正确的密码');
    } else if (values.length < 6) {
      callback('密码必须大于6位');
    } else if (values.length > 16) {
      callback('密码必须小于16位');
    } else {
      callback();
    }
  };

  checkPasswd2 = (rule, values, callback, stateValues) => {
    if (!values) {
      callback('请输入正确的密码');
    } else if (values && values !== stateValues.loginPwd) {
      callback('两次输入密码不一致');
    } else {
      callback();
    }
  };

  checkUserName = (rule, values, callback) => {
    if (!values) {
      callback('请输入登录名');
    } else if (!(/\w+/.test(values))) {
      callback('登录名不能出现中文');
    } else if (values.length > 12) {
      callback('登录名不能超过12位');
    } else {
      callback();
    }
  }

  formChange = (value) => {
    this.setState({
      value,
    });
  };

  handleSubmit = () => {
    this.refs.form.validateAll((errors, values) => {
      if (errors) {
        console.log('errors', errors);
        return;
      }
      global.APIConfig.sendAjax(values, global.APIConfig.optMethod.CreateUserInfo, () => {
        Feedback.toast.success('注册成功');
        this.props.history.push('/user/login');
      }, (msg) => {
        if (msg) {
          Feedback.toast.error(msg);
        }
      });
    });
  };

  render() {
    return (
      <div className="user-register">
        <div className="formContainer">
          <h4 className="formTitle">注 册</h4>
          <IceFormBinderWrapper
            value={this.state.value}
            onChange={this.formChange}
            ref="form"
          >
            <div className="formItems">
              <Row className="formItem">
                <Col className="formItemCol">
                  <IceIcon type="person" size="small" className="inputIcon" />
                  <IceFormBinder
                    name="userName"
                    required
                    // message="请输入正确的登录名"
                    validator={this.checkUserName}
                  >
                    <Input size="large" placeholder="登录名" />
                  </IceFormBinder>
                </Col>
                <Col>
                  <IceFormError name="userName" />
                </Col>
              </Row>

              <Row className="formItem">
                <Col className="formItemCol">
                  <IceIcon type="mail" size="small" className="inputIcon" />
                  <IceFormBinder
                    type="email"
                    name="email"
                    required
                    message="请输入正确的邮箱"
                  >
                    <Input size="large" maxLength={20} placeholder="邮箱" />
                  </IceFormBinder>
                </Col>
                <Col>
                  <IceFormError name="email" />
                </Col>
              </Row>

              <Row className="formItem">
                <Col className="formItemCol">
                  <IceIcon type="lock" size="small" className="inputIcon" />
                  <IceFormBinder
                    name="loginPwd"
                    required
                    validator={this.checkPasswd}
                  >
                    <Input
                      htmlType="password"
                      size="large"
                      placeholder="至少6位密码"
                    />
                  </IceFormBinder>
                </Col>
                <Col>
                  <IceFormError name="loginPwd" />
                </Col>
              </Row>

              <Row className="formItem">
                <Col className="formItemCol">
                  <IceIcon type="lock" size="small" className="inputIcon" />
                  <IceFormBinder
                    name="rePasswd"
                    required
                    validator={(rule, values, callback) =>
                      this.checkPasswd2(
                        rule,
                        values,
                        callback,
                        this.state.value
                      )
                    }
                  >
                    <Input
                      htmlType="password"
                      size="large"
                      placeholder="确认密码"
                    />
                  </IceFormBinder>
                </Col>
                <Col>
                  <IceFormError name="rePasswd" />
                </Col>
              </Row>

              <Row className="formItem">
                <Button
                  type="primary"
                  onClick={this.handleSubmit}
                  className="submitBtn"
                >
                  注 册
                </Button>
              </Row>

              <Row className="tips">
                <Link to="/user/login" className="tips-text">
                  使用已有账户登录
                </Link>
              </Row>
            </div>
          </IceFormBinderWrapper>
        </div>
      </div>
    );
  }
}

export default UserRegister;
