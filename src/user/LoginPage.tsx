import React, { Component, useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators, AnyAction, Dispatch } from 'redux';

import { userLogin } from './actions';
import { AppState } from '../_store/rootReducer';
import { Redirect } from 'react-router';

class LoginPage extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    state = {
        email: "",
        password: "",
        submitted: false
    };

    public componentDidMount() {
    }

    handleChange: React.ReactEventHandler<HTMLInputElement> = (e) => {
        //e.persist();
        const { name, value } = e.currentTarget;
        if (name == 'email') {
            this.setState({ email: value });
        }
        else if (name == 'password') {
            this.setState({ password: value });
        }
        // this.setState({ [name]: value });
    }

    handleSubmit = (e) => {
        e.preventDefault();

        this.setState({ submitted: true });

        const { login } = this.props;
        const { email, password } = this.state;

        if (email && password) {
            login(email, password);
        }
    }

    public render() {
        const { email, password, submitted } = this.state;
        const { user } = this.props;

        if(user != undefined && user.token != undefined && user.token != ""){
            return(
                <Redirect to={"/Dashboard"} />
            );
        }
        else{
            return (
                <div>
                    <h2>Login</h2>
                    <form name="form" onSubmit={this.handleSubmit}>
                        <div className={'form-group' + (submitted && !email ? ' has-error' : '')}>
                            <label htmlFor="email">Email</label>
                            <input type="text" name="email" value={email} onChange={this.handleChange.bind(this)} />
                            {submitted && !email &&
                                <div className="help-block">Email is required</div>
                            }
                        </div>
                        <div className={'form-group' + (submitted && !password ? ' has-error' : '')}>
                            <label htmlFor="password">Password</label>
                            <input type="password" name="password" value={password} onChange={this.handleChange} />
                            {submitted && !password &&
                                <div className="help-block">Password is required</div>
                            }
                        </div>
                        <div className="form-group">
                            <button className="btn btn-primary">Login</button>
                        </div>
                    </form>
                </div>
            );
        }
    }
}

const mapStateToProps = (state: AppState) => ({
    user: state.user
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
            login: userLogin
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginPage);
