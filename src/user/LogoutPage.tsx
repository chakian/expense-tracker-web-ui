import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators, AnyAction, Dispatch } from 'redux';
import { Redirect } from 'react-router';

import { userLogout } from './actions';
import { AppState } from '../_store/rootReducer';

class LogoutPage extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    public componentDidMount() {
    }
    public componentWillMount() {
        const { logout } = this.props;
        logout();
    }

    public render() {
        return (
            <Redirect to="/" />
        );
    }
}

const mapStateToProps = (state: AppState) => ({
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
            logout: userLogout
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LogoutPage);
