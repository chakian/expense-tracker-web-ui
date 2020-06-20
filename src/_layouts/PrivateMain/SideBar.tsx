import React, { Component, CSSProperties } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';

import { Link } from 'react-router-dom';

import {
    AccountBookOutlined,
    BankOutlined,
    CreditCardOutlined,
    LogoutOutlined,
    DashboardOutlined
} from '@ant-design/icons';

import { Layout, Button, Menu } from 'antd';
const { Sider } = Layout;
const { SubMenu } = Menu;

import { userLogout } from '../../user/actions';
import { AppState } from '../../_store/rootReducer';

const textWhite: CSSProperties = {
    color: "#ffffff"
}
const backRed: CSSProperties = {
    backgroundColor: "#ec9797"
}

class SideBar extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    state = {
        collapsed: false,
    };

    componentDidMount() {
    }

    onCollapse = collapsed => {
        console.log(collapsed);
        this.setState({ collapsed });
    };

    render() {
        const { user } = this.props;
        return (
            <Sider collapsible collapsed={this.state.collapsed} onCollapse={this.onCollapse}>
                <div style={textWhite}>Hi {user.name}!</div>
                <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline">

                    <Menu.Item key="1" icon={<DashboardOutlined />}><Link to="/Dashboard">Özet</Link></Menu.Item>

                    <SubMenu key="subAccounts" icon={<BankOutlined/>} title="Hesaplar">
                        <Menu.Item key="a_1" icon={<AccountBookOutlined />}>Hesap 1</Menu.Item>
                        <Menu.Item key="a_2" icon={<AccountBookOutlined />}>Hesap 2</Menu.Item>
                        <Menu.Item key="a_3" icon={<AccountBookOutlined />}>Hesap 3</Menu.Item>
                    </SubMenu>

                    <SubMenu key="subCreditCards" icon={<CreditCardOutlined/>} title="Kredi Kartları">
                        <Menu.Item key="c_1" icon={<CreditCardOutlined />}>Kart 1</Menu.Item>
                        <Menu.Item key="c_2" icon={<CreditCardOutlined />}>Kart 2</Menu.Item>
                        <Menu.Item key="c_3" icon={<CreditCardOutlined />}>Kart 3</Menu.Item>
                    </SubMenu>

                    <Menu.Item key="logout" icon={<LogoutOutlined/>}><Link to='/logout'><Button type="dashed" style={backRed}>Çıkış</Button></Link></Menu.Item>

                </Menu>
            </Sider>
        );
    }
}


const mapStateToProps = (state: AppState) => ({
    user: state.user
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
)(SideBar);
