import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';

import { userLogout } from '../../user/actions'

class TopBar extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    componentDidMount() {
    }

    handleLogout = (e) => {
        const { logout } = this.props;
        logout();
    }

    render() {
        const { user } = this.props;
        return (
            <div>
                <h1>Topbar</h1>
                <span><button onClick={this.handleLogout}>Logout</button></span>
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
            logout: userLogout
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(TopBar);
