import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { alertActions } from '../_actions';
import { PrivateRoute, PublicRoute } from '../_components';
import { LandingPage } from '../LandingPage';
import { LoginPage } from '../LoginPage';
import { Dashboard } from '../BudgetPages';

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
                        <div>
                            <PublicRoute exact path="/" component={LandingPage} />

                            <PublicRoute path="/login" component={LoginPage} />
                            
                            <PrivateRoute path="/Dashboard" component={Dashboard} />
                            {/* <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} />
                            <PrivateRoute exact path="/" component={HomePage} /> */}
                        </div>
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