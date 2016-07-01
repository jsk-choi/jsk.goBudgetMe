import React, { PropTypes } from 'react';
import dtf from 'dateformat';

const DateBox = React.createClass({
    
    componentDidMount () {
        $('#' + this.props.DateBoxName + '').datebox({
            mode: "datebox",
            popupPosition: "window",
            closeCallback: (dt) => {
                dt.name = this.props.DateBoxName;
                this.props.onDateSelect(dt);
            }
        }).trigger('datebox', {
            'method': 'set', 
            'value': this.props.SetDate,
            'date': this.props.SetDate
        });
    },
    render () {
        return (
            <div className="dtbox">
                {this.props.Title}
                <input id={this.props.DateBoxName} type="text" className="form-control" />
            </div>
    	)
    }
});

DateBox.propTypes = {
    DateBoxName: PropTypes.string.isRequired,
    SetDate: PropTypes.any.isRequired,
    onDateSelect: PropTypes.func.isRequired,
    Title: PropTypes.string
};

export default DateBox;