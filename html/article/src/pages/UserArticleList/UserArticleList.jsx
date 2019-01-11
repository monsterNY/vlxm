import React, { Component } from 'react';
import ContentList from './components/ContentList';

export default class UserArticleList extends Component {
  static displayName = 'UserArticleList';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="user-article-list-page">
        <ContentList />
      </div>
    );
  }
}
