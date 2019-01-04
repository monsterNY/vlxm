import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import BraftEditor from 'braft-editor';
import 'braft-editor/dist/braft.css';
// import { ContentUtils } from 'braft-utils';

// 使用指南:https://www.smwenku.com/a/5c0f474fbd9eee5e40badd1b
// 官方文档：https://braft.margox.cn/demos/basic

export default class CustomBraftEditor extends Component {
  static displayName = 'CustomBraftEditor';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {
      // error: createEditorState is not a function
      // editorState: BraftEditor.createEditorState(null),
    };
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

  // insertHello = () => {
  //   console.log(this.state.content);
  //   // this.setState({
  //   //   editorState: ContentUtils.insertText(this.state.editorState, '你好啊！'),
  //   // });
  // }

  render() {
    // const cusExtendControls = [
    //   {
    //     key: 'custom-button',
    //     type: 'button',
    //     text: '按钮',
    //     onClick: this.insertHello,
    //   }, {
    //     key: 'custom-dropdown',
    //     type: 'dropdown',
    //     text: '下拉组件',
    //     component: <div style={{ color: '#fff', padding: 10 }}>你好啊！</div>
    //   }, {
    //     key: 'custom-modal',
    //     type: 'modal',
    //     text: '模态框',
    //     modal: {
    //       id: 'my-moda-1',
    //       title: '你好啊',
    //       children: (
    //         <div style={{ width: 400, padding: '0 10px' }}>
    //           <img src="https://margox.cn/wp-content/uploads/2016/10/FA157E13E8B77E6E11290E9DF4C5ED7D-480x359.jpg" style={{ width: '100%', height: 'auto' }} />
    //         </div>
    //       ),
    //     },
    //   },
    // ];

    const editorProps = {
      height: 500,
      contentFormat: 'html',
      initialContent: '<p></p>',
      onChange: this.handleChange,
      onRawChange: this.handleRawChange,
      // extendControls: cusExtendControls,//不折腾了...
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
