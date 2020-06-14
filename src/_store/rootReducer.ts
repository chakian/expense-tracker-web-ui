import { createStore, combineReducers, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
import { createLogger } from 'redux-logger';

import userReducer from '../user/reducer';

export enum ReduxActionTypes {
    USER_LOGIN = "USER_LOGIN",
    USER_LOGOUT = "USER_LOGOUT",
    CHANGE_PASSWORD = "CHANGE_PASSWORD",
    FORGOT_PASSWORD = "FORGOT_PASSWORD",
    CONFIRM_EMAIL = "CONFIRM_EMAIL"
}

export interface IReduxBaseAction {
    type: ReduxActionTypes;
}

const rootReducer = combineReducers({
    user: userReducer,
});

const loggerMiddleware = createLogger();

export type AppState = ReturnType<typeof rootReducer>;

export default function configureStore() {
    const middlewares = [thunk, loggerMiddleware];
    const middleWareEnhancer = applyMiddleware(...middlewares);

    const store = createStore(
        rootReducer,
        // composeEnhancers(middleWareEnhancer),
        composeWithDevTools(middleWareEnhancer)
    );

    return store;
}