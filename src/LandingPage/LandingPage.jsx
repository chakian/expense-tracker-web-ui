import React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';

class LandingPage extends React.Component {
    componentDidMount() {
    }

    render() {
        const { user } = this.props;
        if(user && user.token) {
            return (
                <Redirect to={{ pathname: '/Dashboard' }} />
            )
        }
        else{
            return (
                <div className="col-md-6 col-md-offset-3">
                    <h1>Hi!</h1>
                    <p>
                        <Link to="/login">Login Now</Link>
                    </p>
                </div>
            );
        }
    }
}

function mapStateToProps(state) {
    const { authentication } = state;
    const { user } = authentication;
    return {
        user
    };
}

const connectedLandingPage = connect(mapStateToProps)(LandingPage);
export { connectedLandingPage as LandingPage };
