import React, { Component } from 'react';
import { Nav } from 'react-bootstrap';
import { Link} from 'react-router-dom';
// import { MdMenu } from "react-icons/md";

export default class SideMenu extends Component {

    constructor(props) {
        super(props);

        this.state = {
        };
    }

    render() {
        return (
            <>
            <Nav variant="pills" defaultActiveKey="/" as="ul">
                <Nav.Item as="li"><Nav.Link as={Link} eventKey="home" to="/">Home</Nav.Link></Nav.Item>
                <Nav.Item as="li"><Nav.Link as={Link} eventKey="login" to="/login">Login</Nav.Link></Nav.Item>
                <Nav.Item as="li"><Nav.Link as={Link} eventKey="register" to="/register">Register</Nav.Link></Nav.Item>
            </Nav>
            </>
        );
    }
}
