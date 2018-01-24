import React from 'react';
import { Router, Route, browserHistory } from 'react-router';

import Main from '../components/Main.jsx';
import MainContainer from '../containers/MainContainer.jsx';
import Test from '../containers/Test.jsx';

import DateBox from '../components/DateBox.jsx';

import Details from '../components/Details.jsx';
import Here from '../components/Here.jsx';
import Some from '../components/Some.jsx';

const routes = (
    <Router history={browserHistory}>
        <Route path="/" component={MainContainer}>
            <Route path="details" component={Details} />
            <Route path="here" component={Here} />
            <Route path="some" component={Some} />
        </Route>
    </Router>
);

export default routes;