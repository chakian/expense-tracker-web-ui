import React from 'react';
import { Route, Redirect } from 'react-router-dom';

export const PrivateRoute = ({ component: Component, layout: Layout, ...rest }) => {
    if(localStorage.getItem('user')){
        return (
            <Route {...rest} render={props => (
                <Layout {...props}>
                    <Component {...props} />
                </Layout>
            )} />
        );
    }
    else return (
        <Route {...rest} render={props => (
            localStorage.getItem('user')
                ? 
                <Layout {...props}>
                    <Component {...props} />
                </Layout>
                : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
        )} />
    )
}