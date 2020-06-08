import React from 'react';
import { Route, Redirect } from 'react-router-dom';

export const PublicRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
        <div className="jumbotron">
            <div className="container">
                <div className="col-sm-8 col-sm-offset-2">
                    <Component {...props} />
                </div>
            </div>
        </div>
        
    )} />
)