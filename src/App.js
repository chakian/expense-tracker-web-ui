import React from 'react';
import { Container } from 'react-bootstrap';

import TopBar from './TopBar'
import SideMenu from './SideMenu'

import './App.css';

const App = () => (
  <Container fluid>
    <TopBar />
    <SideMenu />
  </Container>
)

export default App;
