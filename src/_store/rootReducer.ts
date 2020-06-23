import { createStore, combineReducers, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
import { createLogger } from 'redux-logger';

import userReducer from '../user/reducer';
import accountReducer from '../account/reducer';

export enum ReduxActionTypes {
    USER_LOGIN = "USER_LOGIN",
    CHECK_LOGGED_IN = "CHECK_LOGGED_IN",
    USER_LOGOUT = "USER_LOGOUT",
    CHANGE_PASSWORD = "CHANGE_PASSWORD",
    FORGOT_PASSWORD = "FORGOT_PASSWORD",
    CONFIRM_EMAIL = "CONFIRM_EMAIL",

    GET_ACCOUNT_LIST = "GET_ACCOUNT_LIST",
    CREATE_ACCOUNT = "CREATE_ACCOUNT",
    UPDATE_ACCOUNT = "UPDATE_ACCOUNT",
}

export interface IReduxBaseAction {
    type: ReduxActionTypes;
}

const rootReducer = combineReducers({
    user: userReducer,
    account: accountReducer,
});

const loggerMiddleware = createLogger();

export type AppState = ReturnType<typeof rootReducer>;

export default function configureStore() {
    const middlewares = [thunk, loggerMiddleware];
    const middleWareEnhancer = applyMiddleware(...middlewares);

    const store = createStore(
        rootReducer,
        composeWithDevTools(middleWareEnhancer)
    );

    return store;
}
