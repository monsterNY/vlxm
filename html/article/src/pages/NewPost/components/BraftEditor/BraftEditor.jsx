import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import BraftEditor from 'braft-editor';
import 'braft-editor/dist/braft.css';

// 使用指南:https://www.smwenku.com/a/5c0f474fbd9eee5e40badd1b

export default class CustomBraftEditor extends Component {
  static displayName = 'CustomBraftEditor';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {};
    this.content = '';
    if (this.props.bindRef) {
      this.props.bindRef(this);// 绑定子组件的引用
    }
  }

  handleRawChange = (content) => {
    console.log(content);
  };

  handleChange = (rawContent) => {
    console.log(rawContent);
    this.content = rawContent;
  };

  handleUpload = (param) => {
    console.log(param);
    // param.error({
    //   msg: '测试中...',
    // });

    const r = new FileReader(param.file);// 本地预览
    r.onload = function () {
      console.log(r.result);// 图片的base64
      global.APIConfig.baseSendAjax(global.APIConfig.uploadBase64Url, {
        key: r.result,
      }, (data) => {
        console.log(data);
        param.success({
          url: global.APIConfig.imgBaseUrl + data,
        });
      }, () => {
        param.error({
          msg: '上传异常...',
        });
      });
    };
    r.readAsDataURL(param.file);// Base64
  }


  render() {
    const editorProps = {
      height: 500,
      contentFormat: 'html',
      initialContent: '<p></p>',
      onChange: this.handleChange,
      onRawChange: this.handleRawChange,
      media: {
        uploadFn: this.handleUpload,
      },
    };

    return (
      <IceContainer>
        <BraftEditor {...editorProps} />
      </IceContainer>
    );
  }
}
