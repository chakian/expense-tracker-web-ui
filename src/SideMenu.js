import React, { Component } from 'react';
import { Nav } from 'react-bootstrap';
import { Route, Link, Switch } from 'react-router-dom';
import { MdMenu } from "react-icons/md";

import Login from './user/Login';
import Register from './user/Register';
import Home from './Home'

export default class SideMenu extends Component {

    constructor(props) {
        super(props);

        this.state = {
            isVisible: false,
        };
    }

    updateModal(isVisible) {
        this.setState({
            isVisible: isVisible
        });
        this.forceUpdate();
    }

    render() {
        return (
            <>
            <MdMenu />
            <Nav variant="pills" defaultActiveKey="/" as="ul">
                <Nav.Item as="li">
                    <Nav.Link as={Link} eventKey="home" to="/">Home</Nav.Link>
                </Nav.Item>
                <Nav.Item as="li">
                    <Nav.Link as={Link} eventKey="login" to="/login">Login</Nav.Link>
                </Nav.Item>
                <Nav.Item as="li">
                    <Nav.Link as={Link} eventKey="register" to="/register">Register</Nav.Link>
                </Nav.Item>
            </Nav>

            <Switch>
                <Route exact path="/" component={Home} />
                <Route exact path="/login" component={Login} />
                <Route exact path="/register" component={Register} />
                <Route component={Home} />
            </Switch>
            </>
        );
    }
}
