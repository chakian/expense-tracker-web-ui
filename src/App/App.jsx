import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { PrivateRoute, PublicRoute } from '../_routes';
import { LandingPage } from '../LandingPage';
import { LoginPage } from '../user/LoginPage';
import { Dashboard } from '../BudgetPages';
import { PublicLayout, PrivateLayout } from '../_layouts';

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
        history.listen((location, action) => {
            // clear alert on location change
            dispatch(alertActions.clear());
        });
    }

    render() {
        // const { alert } = this.props;
        return (
            
            // {alert.message &&
            //     <div className={`alert ${alert.type}`}>{alert.message}</div>
            // }
            <Router history={history}>
                <Switch>
                    <PublicRoute exact path="/" component={LandingPage} layout={PublicLayout} />

                    <PublicRoute path="/login" component={LoginPage} layout={PublicLayout} />
                    
                    <PrivateRoute path="/Dashboard" component={Dashboard} layout={PrivateLayout} />
                </Switch>
            </Router>
        );
    }
}

function mapStateToProps(state) {
    const { alert } = state;
    return {
        alert
    };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 