// React and Redux imports
import * as React from "react";
import { connect } from "react-redux";
import { BrowserRouter, Switch } from 'react-router-dom';

// App imports
import { AppState } from "./_store/rootReducer";

// Generic imports
import { PrivateRoute } from './_routes/PrivateRoute';
import { PublicRoute } from './_routes/PublicRoute';
import { PublicLayout } from './_layouts/PublicLayout';
import PrivateLayout from './_layouts/PrivateMain/PrivateLayout';

// Page imports
import LandingPage from './LandingPage';
import LoginPage from './user/LoginPage';
import LogoutPage from './user/LogoutPage';
import Dashboard from './dashboard/Dashboard';
import SwitchBudget from './budget/SwitchBudget';

// Props for App
interface AppProps {
}

// Class implenmentation
class App extends React.Component<AppProps> {
    componentDidMount() {
    }

    render() {
        return (
            <div>
                <BrowserRouter>
                    <Switch>
                        <PublicRoute exact path="/" component={LandingPage} layout={PublicLayout} />

                        <PublicRoute path="/login" component={LoginPage} layout={PublicLayout} />
                        <PublicRoute path="/logout" component={LogoutPage} layout={PublicLayout} />
                        
                        <PrivateRoute path="/Dashboard" component={Dashboard} layout={PrivateLayout} />

                        <PrivateRoute exact path="/switchbudget" component={SwitchBudget} layout={PrivateLayout} />
                    </Switch>
                </BrowserRouter>
            </div>
        );
    }
}

const mapStateToProps = (state: AppState) => ({
});

export default connect(
    mapStateToProps
)(App);
