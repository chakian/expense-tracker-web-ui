import React, { Component, CSSProperties } from 'react';

import { Layout } from 'antd';
const { Header, Footer, Content, Sider } = Layout;

import TopBar from './TopBar';

const textCenter: CSSProperties = {
    textAlign: "center"
};

class PrivateLayout extends Component {
    render() { 
        return(
            <Layout>
                <Sider>Sidebar Navigation</Sider>
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

export { PrivateLayout };