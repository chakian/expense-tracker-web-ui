import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';

import Login from '../user/Login';
import Register from '../user/Register';
import Home from '../Home'

export default class Routing extends Component {

    constructor(props) {
        super(props);

        this.state = {
        };
    }

    render() {
        return (
            <>
            <Switch>
                <Route exact path="/" component={Home} />
                <Route exact path="/login" component={Login} />
                <Route exact path="/register" component={Register} />
                <Route component={Home} />
            </Switch>
            </>
        );
    }
}
