import React, { Component } from 'react';
import IceContainer from '@icedesign/container';
import BraftEditor from 'braft-editor';
import 'braft-editor/dist/braft.css';
import {
  Feedback,
} from '@icedesign/base';

import './RichEditor.scss';

export default class RichEditor extends Component {
  static displayName = 'RichEditor';

  constructor(props) {
    super(props);

    // 加载初始数据，通常从接口中获取或者默认为空
    this.state = {
    };

    this.content = '';
  }

  componentDidMount() {
    if (this.props.bindRef) {
      this.props.bindRef(this);// 绑定子组件的引用
    }
  }

  // 富文本编辑事件 S
  handleRawChange = (content) => {
    console.log(content);
  };

  handleChange = (rawContent) => {
    console.log(rawContent);
    this.content = rawContent;
  };
  // 富文本编辑事件 E

  render() {
    const editorProps = {
      height: 500,
      contentFormat: 'html',
      initialContent: '<p></p>',
      onChange: this.handleChange,
      onRawChange: this.handleRawChange,
    };

    return (
      <div className="rich-editor" >
        <IceContainer>
          <BraftEditor {...editorProps} />
        </IceContainer>
      </div>
    );
  }
}

// const styles = {
//   editor: {
//     minHeight: 200,
//   },
// };
