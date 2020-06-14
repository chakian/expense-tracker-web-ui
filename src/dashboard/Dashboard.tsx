import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';

import MonthSelection from './components/MonthSelection';

class Dashboard extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
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
    const { user } = state;
    return {
        user
    };
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Dashboard);

