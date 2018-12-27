import React, { Component } from 'react';
import ContentEditor from './components/ContentEditor';

export default class NewPost extends Component {
  static displayName = 'NewPost';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return <ContentEditor />;
  }
}
