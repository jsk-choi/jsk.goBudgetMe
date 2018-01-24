import React from 'react';
import { Link } from 'react-router';

var Main = React.createClass({
    render: function () {
        return (
            <div>
                <h1>main component with stuff on bottom</h1>
	            <Link to="/">root</Link><br />
	            <Link to="/add">add</Link><br />
	            <Link to="/some">some</Link><br />
	            <Link to="/details">details</Link><br />
	            <Link to="/here">here</Link><br />
                {this.props.children}
            </div>
        )
    }
});

export default Main;