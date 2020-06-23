import React, { Component, CSSProperties } from 'react';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { Layout } from 'antd';
const { Header, Footer, Content } = Layout;

import { AppState } from '../../_store/rootReducer';
import { checkLoggedIn } from '../../user/actions';
import TopBar from './TopBar';
import SideBar from './SideBar';

const textCenter: CSSProperties = {
    textAlign: "center"
};

class PrivateLayout extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    public componentDidMount(){
        const { checkUser } = this.props;
        checkUser();
    }

    render() {
        return(
            <Layout style={{ minHeight: '100vh' }}>
                <SideBar />
                <Layout>
                    <Header style={textCenter}><TopBar /></Header>
                    <Content>{this.props.children}</Content>
                    <Footer style={{borderTop: "1px solid", marginTop: "50px"}}>
                        by Çağdaş Korkut
                    </Footer>
                </Layout>
            </Layout>
        );
    }
}

const mapStateToProps = (state: AppState) => ({
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
            checkUser: checkLoggedIn
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(PrivateLayout);
