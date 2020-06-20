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
            <div>
                <Header style={textCenter}>
                    <TopBar />
                </Header>
                <Content>
                    <Sider>Sidebar Navigation</Sider>
                    {this.props.children}
                </Content>
                <Footer>
                    by Çağdaş Korkut
                </Footer>
            </div>
        );
    }
}

export { PrivateLayout };