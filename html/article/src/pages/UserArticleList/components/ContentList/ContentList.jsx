import React, { Component } from 'react';
import Filter from './Filter';
import Lists from './Lists';

export default class ContentList extends Component {
  static displayName = 'ContentList';

  static propTypes = {};

  static defaultProps = {};

  constructor(props) {
    super(props);
    this.state = {};
  }

  callBackFilter = (param) => {
    param.filterType = 2;
    console.log(param);
    this.listRef.loadData(param);
  }

  bindListRef = (ref) => {
    console.log(ref);
    this.listRef = ref;
  }

  render() {
    return (
      <div>
        <Filter callBackEvent={this.callBackFilter} />
        <Lists bindRef={this.bindListRef} />
      </div>
    );
  }
}
