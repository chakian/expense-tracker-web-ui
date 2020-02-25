import React, { Component } from 'react';
import { Navbar } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import logo from './logo.svg';

export default class SideMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
        };
    }

    render() {
        return (
            <Navbar bg="dark" variant="dark">
                <Navbar.Brand as={Link} to="/">
                    <img alt="" src={logo} width="30" height="30" className="d-inline-block align-top" />{' '}
                    Expense Tracker
                </Navbar.Brand>
            </Navbar>
        );
    }
}