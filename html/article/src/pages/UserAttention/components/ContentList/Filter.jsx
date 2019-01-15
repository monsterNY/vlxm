import React, { Component } from 'react';
import IceContainer from '@icedesign/container';

export default class Filter extends Component {
  static displayName = 'Filter';

  constructor(props) {
    super(props);
    this.state = {
      activeItem: {
        filterType: 0,
      },
    };
  }

  componentDidMount() {
  }

  handleClick = (key, value) => {
    console.log(`key:${key},value:${value}`);
    const activeItem = this.state.activeItem;
    activeItem[key] = value;
    this.setState({
      activeItem,
    });
    console.log(this.state.activeItem);

    this.props.callBackEvent(this.state.activeItem);
    console.log(this.state.activeItem);
  };

  render() {
    const data = [
      {
        key: 'filterType',
        label: '筛选',
        value: [{
          key: 0,
          value: '关注我的',
        }, {
          key: 1,
          value: '我关注的',
        }],
      },
    ];

    return (
      <IceContainer title="精确筛选">
        <div style={styles.filterContent}>
          {data.map((item, index) => {
            const lastItem = index === data.length - 1;
            const lastItemStyle = lastItem ? { marginBottom: 0 } : null;
            return (
              <div
                style={{ ...styles.filterItem, ...lastItemStyle }}
                key={index}
              >
                <div style={styles.filterLabel}>{item.label}:</div>
                <div style={styles.filterList}>
                  {item.value.map((info, subIndex) => {
                    console.log(this.state.activeItem[item.key] === info.key);
                    const activeStyle = this.state.activeItem[item.key] === info.key ? styles.active : null;
                    // const activeStyle = styles.active;
                    return (
                      <span
                        key={subIndex}
                        onClick={() => this.handleClick(item.key, info.key)}
                        style={{ ...styles.filterText, ...activeStyle }}
                      >
                        {info.value}
                      </span>
                    );
                  })}
                </div>
              </div>
            );
          })}
        </div>
      </IceContainer>
    );
  }
}

const styles = {
  filterItem: {
    display: 'flex',
    alignItems: 'center',
    height: '28px',
    marginBottom: '20px',
  },
  filterLabel: {
    width: '120px',
    fontSize: '15px',
    fontWeight: '450',
  },
  filterText: {
    fontSize: '15px',
    marginRight: '15px',
    cursor: 'pointer',
  },
  active: {
    minWeight: '60px',
    borderRadius: '20px',
    padding: '5px 15px',
    background: '#2784fc',
    color: '#fff',
  },
};
