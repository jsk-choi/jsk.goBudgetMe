import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, IndexRoute, browserHistory } from 'react-router';

import routes from './app/_config/routes.jsx';

ReactDOM.render(
	routes,
	document.getElementById('app')
)