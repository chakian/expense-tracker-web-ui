import React from 'react';
import { Route, Redirect } from 'react-router-dom';

export const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
        localStorage.getItem('user')
            ? 
            <div>
                <div id="menu">
                    <span>Hesaplar<br/>Falanlar<br/>Filanlar</span>
                </div>
                <div id="content">
                    <Component {...props} />
                </div>
            </div>
            : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
    )} />
)