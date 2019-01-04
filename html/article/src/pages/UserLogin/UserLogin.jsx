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
class UserLogin extends Component {
  static displayName = 'UserLogin';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      value: {
        username: 'monster',
        loginPwd: 'monster',
        // checkbox: false,
      },
    };
  }

  formChange = (value) => {
    this.setState({
      value,
    });
  };

  componentDidMount() {
    if (global.APIConfig.getUserCache()) {
      Feedback.toast.success('您已登录!');
      this.props.history.push('/');
    }
  }

  handleSubmit = (e) => {
    e.preventDefault();
    this.refs.form.validateAll((errors, values) => {
      if (errors) {
        console.log('errors', errors);
        return;
      }

      global.APIConfig.sendAjax(values, global.APIConfig.optMethod.UserLogin, (data) => {
        global.APIConfig.setUserCache(data);// 更新本地缓存
        Feedback.toast.success('登录成功');
        this.props.history.push('/');
        // window.localStorage.userInfo = data;// localStorage以键值对保存 值类型都是string
        // window.localStorage.userInfo = JSON.stringify(data);
        // console.log(JSON.parse(window.localStorage.userInfo));
      }, (msg) => {
        if (msg) {
          Feedback.toast.error(msg);
        }
      });

      // console.log(values);
      // Feedback.toast.success('登录成功');
      // this.props.history.push('/');
    });
  };

  render() {
    return (
      <div className="formContainer">
        <h4 className="formTitle">登 录</h4>
        <IceFormBinderWrapper
          value={this.state.value}
          onChange={this.formChange}
          ref="form"
        >
          <div className="formItems">
            <Row className="formItem">
              <Col className="formItemCol">
                <IceIcon type="person" size="small" className="inputIcon" />
                <IceFormBinder name="username" required message="必填">
                  <Input size="large" maxLength={20} placeholder="用户名" />
                </IceFormBinder>
              </Col>
              <Col>
                <IceFormError name="username" />
              </Col>
            </Row>

            <Row className="formItem">
              <Col className="formItemCol">
                <IceIcon type="lock" size="small" className="inputIcon" />
                <IceFormBinder name="loginPwd" required message="必填">
                  <Input size="large" htmlType="password" placeholder="密码" />
                </IceFormBinder>
              </Col>
              <Col>
                <IceFormError name="loginPwd" />
              </Col>
            </Row>

            {/* <Row className="formItem">
              <Col>
                <IceFormBinder name="checkbox">
                  <Checkbox className="checkbox">记住账号</Checkbox>
                </IceFormBinder>
              </Col>
            </Row> */}

            <Row className="formItem">
              <Button
                type="primary"
                onClick={this.handleSubmit}
                className="submitBtn"
              >
                登 录
              </Button>
              {/* <p className="account">
                <span className="tips-text" style={{ marginRight: '20px' }}>
                  管理员登录：admin/admin
                </span>
                <span className="tips-text">学生登录：user/user</span>
              </p> */}
            </Row>

            <Row className="tips">
              <Link to="/user/register" className="tips-text">
                立即注册
              </Link>
            </Row>
          </div>
        </IceFormBinderWrapper>
      </div>
    );
  }
}

export default UserLogin;
