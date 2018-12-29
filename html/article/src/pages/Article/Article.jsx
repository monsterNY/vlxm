import React, { Component } from 'react';
import BasicDetailInfo from './components/BasicDetailInfo';

export default class Article extends Component {
  static displayName = 'Article';

  constructor(props) {
    super(props);
    this.state = {};
    // console.log(this.props.match.params.id);
  }

  render() {
    return (
      <div className="article-page">
        <BasicDetailInfo
          id={this.props.match.params.id}
        />
      </div>
    );
  }
}
