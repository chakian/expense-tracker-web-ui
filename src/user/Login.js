import React, { Component } from 'react';
import { Form, Button } from 'react-bootstrap';

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
    };
  }

  loginClick = () => {
    alert('yes');
  }

  render() {
    return (
      <Form>
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control
            placeholder="Enter email"
            type="email"
          />
          <Form.Text className="text-muted">
            We'll never share your email with anyone else.
          </Form.Text>
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control
            placeholder="Password" 
            type="password"
          />
        </Form.Group>
        <Form.Group controlId="formBasicCheckbox">
          <Form.Check
            label="Check me out"
            type="checkbox"
          />
        </Form.Group>
        <Button
          onClick="click"
          type="submit"
          variant="primary"
        >
          Submit
        </Button>
      </Form>
    );
  }
}