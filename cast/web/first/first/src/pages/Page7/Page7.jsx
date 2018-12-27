import React, { Component } from 'react';
import QuillRichTextEditor from './components/QuillRichTextEditor';

export default class Page7 extends Component {
  static displayName = 'Page7';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="page7-page">
        <QuillRichTextEditor />
      </div>
    );
  }
}
