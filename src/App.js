import React from 'react';
import { Route, Link, Switch } from 'react-router-dom';
import { Container, Navbar, Nav } from 'react-bootstrap';
import Login from './user/Login';
import Register from './user/Register';
import logo from './logo.svg';
import './App.css';

const App = () => (
  <Container fluid>

    <Navbar bg="dark" variant="dark">
      <Navbar.Brand href="#home">
        <img alt="" src={logo} width="30" height="30" className="d-inline-block align-top" />{' '}
        Expense Tracker
        </Navbar.Brand>
    </Navbar>

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
  </Container>
)

const Home = () => <h1>Home</h1>;

export default App;
