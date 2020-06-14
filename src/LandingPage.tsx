import React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { AppState } from './_store/rootReducer';
import { checkLoggedIn } from './user/actions';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';

class LandingPage extends React.Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>> {
    componentDidMount(){
        const { checkUser } = this.props;
        checkUser();
    }
    render() {
        const { user } = this.props;
        if(user && user.token) {
            return (
                <div>
                    <Redirect to={{ pathname: '/Dashboard' }} />
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

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
            checkUser: checkLoggedIn
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LandingPage);
