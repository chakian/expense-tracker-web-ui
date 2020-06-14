import React from 'react';
import { Route, Redirect } from 'react-router-dom';

export const PrivateRoute = ({ component: Component, layout: Layout, ...rest }) => (
    <Route {...rest} render={props => (
        localStorage.getItem('user')
            ? 
            <Layout {...props}>
                <Component {...props} />
            </Layout>
            : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
    )} />
)