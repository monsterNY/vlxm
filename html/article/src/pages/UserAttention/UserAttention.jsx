import React, { Component } from 'react';
import ContentList from './components/ContentList';

export default class UserAttention extends Component {
  static displayName = 'UserAttention';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="user-attention-list-page">
        <ContentList />
      </div>
    );
  }
}
