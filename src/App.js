import React from 'react';
import { Container } from 'react-bootstrap';

import TopBar from './components/TopBar'
import SideMenu from './components/SideMenu'

import "materialize-css/dist/css/materialize.min.css";
import "materialize-css/dist/js/materialize.min.js";

import './App.css';

import { Button, MediaBox } from "react-materialize";

const App = () => (
  <Container fluid>
    <Button > Click Me </Button>
    <CustomMediaBox></CustomMediaBox>
    <TopBar />
    <SideMenu />
  </Container>
)

function CustomMediaBox() {
  return (
    <MediaBox
      options={{
        inDuration: 275,
        onCloseEnd: null,
        onCloseStart: null,
        onOpenEnd: null,
        onOpenStart: null,
        outDuration: 200
      }}
    >
      <img
        alt=""
        src="https://materializecss.com/images/sample-1.jpg"
        width="650"
      />
    </MediaBox>
  );
}

export default App;
