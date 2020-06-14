import React from 'react';
import { Route } from 'react-router-dom';

export const PublicRoute = ({ component: Component, layout: Layout, ...rest }) => (
    <Route {...rest} render={props => (
        <div className="jumbotron">
            <div className="container">
                <div className="col-sm-8 col-sm-offset-2">
                    <Layout {...props}>
                        <Component {...props} />
                    </Layout>
                </div>
            </div>
        </div>
        
    )} />
)
