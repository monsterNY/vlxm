import React, { Component } from 'react';
import Waterfall from './components/Waterfall';

export default class PostList extends Component {
  static displayName = 'PostList';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return <Waterfall />;
  }
}
