import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { alertActions } from '../_actions';
import { PrivateRoute, PublicRoute } from '../_components';
import { LandingPage } from '../LandingPage';
import { LoginPage } from '../LoginPage';
import { Dashboard } from '../BudgetPages';
import { PublicLayout } from '../_layouts/PublicLayout';

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
        const { alert } = this.props;
        return (
            <div className="container">
                <div className="col-sm-12">
                    {alert.message &&
                        <div className={`alert ${alert.type}`}>{alert.message}</div>
                    }
                    <Router history={history}>
                        <Switch>
                            <PublicRoute exact path="/" component={LandingPage} layout={PublicLayout} />

                            <PublicRoute path="/login" component={LoginPage} layout={PublicLayout} />
                            
                            <PrivateRoute path="/Dashboard" component={Dashboard} />
                            {/* <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} /> */}
                        </Switch>
                    </Router>
                </div>
            </div>
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