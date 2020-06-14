import React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { AppState } from './_store/rootReducer';

class LandingPage extends React.Component<any, any> {

    render() {
        const { user } = this.props;
        if(user && user.token) {
            return (
                <div>
                    LOGGED IN
                {/* <Redirect to={{ pathname: '/Dashboard' }} /> */}
                </div>
            )
        }
        else{
            return (
                <div className="col-md-6 col-md-offset-3">
                    <h1>Hi!</h1>
                    <p>
                        <Link to={'/login'}>Login Now</Link>
                    </p>
                </div>
            );
        }
    }
}

function mapStateToProps(state: AppState) {
    const { user } = state;
    return {
        user
    };
}

export default connect(
    mapStateToProps
)(LandingPage);
