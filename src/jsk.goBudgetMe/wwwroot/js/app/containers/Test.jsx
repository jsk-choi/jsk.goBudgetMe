import React from 'react';

const Test = React.createClass({
    getInitialState () {
        return {
            values: [
                { type: "translateX", x: 10 },
                { type: "scaleX", x: 1.2 }
            ]
        }
    },
    handleChange (v) {
        console.log(v);
    },
    render () {
        return <div>
            {this.state.values.map(function (item, i) {
                return (
                    <div key={i}>
                      <span>{item.type}</span>
                      <input type="text" defaultValue={item.x} onChange={this.handleChange} />
                    </div>
                )
            }, this)}
            <pre>{JSON.stringify(this.state)}</pre>
        </div>;
    }
});

export default Test;