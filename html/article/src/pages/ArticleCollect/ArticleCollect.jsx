import React, { Component } from 'react';
import Masonry from './components/Masonry';

export default class ArticleCollect extends Component {
  static displayName = 'ArticleCollect';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="article-collect-page">
        <Masonry />
      </div>
    );
  }
}
