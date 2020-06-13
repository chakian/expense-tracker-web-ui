import { combineReducers } from "redux";
import { userReducer } from './user.reducer';
import { alertReducer } from './alert.reducer';

export const rootReducer = combineReducers({
    user: userReducer,
    alert: alertReducer
})

export type RootState = ReturnType<typeof rootReducer>;
