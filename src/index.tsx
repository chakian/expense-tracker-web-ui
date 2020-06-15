import * as React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import configureStore from "./_store/rootReducer";

import App from "./App";

import 'antd/dist/antd.css';

const store = configureStore();

const Root = () => (
  <Provider store={store}>
    <App />
  </Provider>
);

render(<Root />, document.getElementById("app"));
