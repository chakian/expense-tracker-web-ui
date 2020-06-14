// React and Redux imports
import * as React from "react";
import { connect } from "react-redux";
import { BrowserRouter, Switch } from 'react-router-dom';

// App imports
import { AppState } from "./_store/rootReducer";
import "./main.css";

// Generic imports
import { PrivateRoute, PublicRoute } from './_routes';
import { PublicLayout, PrivateLayout } from './_layouts';

// Page imports
import LandingPage from './LandingPage';
import LoginPage from './user/LoginPage';
// import { Dashboard } from './BudgetPages';

// Props for App
interface AppProps {
}

// Class implenmentation
class App extends React.Component<AppProps> {
    componentDidMount() {
    }

    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <PublicRoute exact path="/" component={LandingPage} layout={PublicLayout} />

                    <PublicRoute path="/login" component={LoginPage} layout={PublicLayout} />
                    
                    {/* <PrivateRoute path="/Dashboard" component={Dashboard} layout={PrivateLayout} /> */}
                </Switch>
            </BrowserRouter>
        );
    }
}

const mapStateToProps = (state: AppState) => ({
});

export default connect(
    mapStateToProps
)(App);
