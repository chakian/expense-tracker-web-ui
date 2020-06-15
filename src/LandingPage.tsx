import React, { CSSProperties } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';

import { AppState } from './_store/rootReducer';
import { checkLoggedIn } from './user/actions';

const hugeImageCss: CSSProperties = {
    fontSize: "10rem"
};

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
                <div>
                    <p style={{textAlign:"center"}}>
                        <Link id="loginLink" to={'/login'} style={hugeImageCss}>Giriş</Link>
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
