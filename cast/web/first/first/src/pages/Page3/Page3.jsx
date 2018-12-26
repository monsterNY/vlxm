import React, { Component } from 'react';
import UserTable from './components/UserTable';

export default class Page3 extends Component {
  static displayName = 'Page3';

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="page3-page">
        <UserTable />
      </div>
    );
  }
}
