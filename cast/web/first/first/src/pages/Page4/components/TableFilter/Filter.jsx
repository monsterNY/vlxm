import React, { Component } from 'react';
import { Button, DatePicker, Select } from '@icedesign/base';

export default class TableFilter extends Component {
  static displayName = 'TableFilter';

  constructor(props) {
    super(props);
    // console.log(props);
    this.state = {
      startValue: null,
      endValue: null,
      endOpen: false,
      validFlag: -1,
      onSearchFunc: props.onSearch,
    };
  }

  disabledStartDate = (startValue) => {
    const { endValue } = this.state;
    if (!startValue || !endValue) {
      return false;
    }
    return startValue.valueOf() > endValue.valueOf();
  };

  disabledEndDate = (endValue) => {
    const { startValue } = this.state;
    if (!endValue || !startValue) {
      return false;
    }
    return endValue.valueOf() <= startValue.valueOf();
  };

  onChange = (field, value) => {
    this.setState({
      [field]: value,
    });
  };

  onStartChange = (value) => {
    this.onChange('startValue', value);
  };

  onEndChange = (value) => {
    this.onChange('endValue', value);
  };

  handleStartOpenChange = (open) => {
    if (!open) {
      this.setState({ endOpen: true });
    }
  };

  handleEndOpenChange = (open) => {
    this.setState({ endOpen: open });
  };

  handleSelectChange = (value) => {
    this.setState({ validFlag: value });
  };

  onSearch = () => {
    this.state.onSearchFunc(this.state);
  }

  render() {
    const { startValue, endValue, endOpen } = this.state;
    return (
      <div style={styles.tableFilter}>
        <div style={styles.filterItem}>
          <span style={styles.filterLabel}>调价日期：</span>
          <DatePicker
            disabledDate={this.disabledStartDate}
            value={startValue}
            placeholder="Start"
            onChange={this.onStartChange}
            onOpenChange={this.handleStartOpenChange}
          />
          &nbsp;&nbsp;
          <DatePicker
            disabledDate={this.disabledEndDate}
            value={endValue}
            placeholder="End"
            onChange={this.onEndChange}
            open={endOpen}
            onOpenChange={this.handleEndOpenChange}
          />
        </div>
        <div style={styles.filterItem}>
          <span style={styles.filterLabel}>状态：</span>
          <Select
            onChange={this.handleSelectChange}
          >
            <Select.Option value={-1}>全部</Select.Option>
            <Select.Option value={1} >有效</Select.Option>
            <Select.Option value={0}>无效</Select.Option>
          </Select>
        </div>
        <Button
          type="primary"
          style={styles.submitButton}
          onClick={this.onSearch}
        >
          查询
        </Button>
      </div>
    );
  }
}

const styles = {
  tableFilter: {
    display: 'flex',
    background: '#fff',
    padding: '20px 0',
    marginBottom: '20px',
  },
  filterItem: {
    display: 'flex',
    alignItems: 'center',
    marginLeft: '15px',
  },
  submitButton: {
    marginLeft: '15px',
  },
};
