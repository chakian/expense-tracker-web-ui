import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators, AnyAction, Dispatch } from 'redux';

import { userLogin } from './actions';
import { AppState } from '../_store/rootReducer';
import { Redirect } from 'react-router';

import { Form, Input, Button, Row, Col } from 'antd';

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};
const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};

class LoginPage extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    public componentDidMount() {
    }

    onFinish = values => {
        console.log('Success:', values);
        const { login } = this.props;
        const { email, password } = values;

        if (email && password) {
            login(email, password);
        }
    };

    onFinishFailed = errorInfo => {
        console.log('Failed:', errorInfo);
    };

    public render() {
        const { user } = this.props;

        if (user != undefined && user.token != undefined && user.token != "") {
            return (
                <Redirect to={"/Dashboard"} />
            );
        }
        else {
            document.title = "Giriş";
            return (
                <Row>
                    <Col span={8} offset={8}>
                        <Form {...layout} name="login" size="large"
                            onFinish={this.onFinish}
                            onFinishFailed={this.onFinishFailed}>
                            <Form.Item label="Email" name="email" rules={[{ required: true, message: 'Email adresi zorunludur!' }, { type: "email", message: "Email adresi geçersiz!" }]}>
                                <Input />
                            </Form.Item>
                            <Form.Item label="Şifre" name="password" rules={[{ required: true, message: 'Şifre zorunludur!' }]}>
                                <Input.Password />
                            </Form.Item>
                            <Form.Item {...tailLayout}>
                                <Button type="primary" htmlType="submit">Giriş</Button>
                            </Form.Item>
                        </Form>
                    </Col>
                </Row>
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
