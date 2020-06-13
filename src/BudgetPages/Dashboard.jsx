import React from 'react';
import { connect } from 'react-redux';
import { MonthSelection } from './components';

class Dashboard extends React.Component {
    componentDidMount() {
    }

    render() {
        const { user } = this.props;
        return (
            <div>
                <MonthSelection />
                <h1>Hi {user.name}!</h1>
                <p>
                    Hallo!
                </p>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { authentication } = state;
    const { user } = authentication;
    return {
        user
    };
}

const connectedDashboard = connect(mapStateToProps)(Dashboard);
export { connectedDashboard as Dashboard };
