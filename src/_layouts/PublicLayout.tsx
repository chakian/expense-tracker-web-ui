import React, { Component } from 'react';
import { Layout } from 'antd';

const { Header, Footer, Content } = Layout;

class PublicLayout extends Component {
    render() {
        return (
            <div>
                <Header style={{textAlign: "center"}}>
                    <h1>Harcama Takibi</h1>
                </Header>
                <Content>
                    <h2 style={{textAlign: "center"}}>Bütçenizi ayarlayın. Paranız sizi yönetmesin; siz paranızı yönetin.</h2>
                    {this.props.children}
                </Content>
                <Footer>
                    by Çağdaş Korkut
                </Footer>
            </div>
        );
    }
}

export { PublicLayout };