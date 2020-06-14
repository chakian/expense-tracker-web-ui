import { ThunkAction, ThunkDispatch } from "redux-thunk";
import { useHistory } from "react-router-dom";

import { IUserState } from "./reducer";
import { ReduxActionTypes, IReduxBaseAction } from "../_store/rootReducer";

import { userService } from "../_services/userService";

export interface ILoginUserAction extends IReduxBaseAction {
    type: ReduxActionTypes.USER_LOGIN;
    data: IUserState;
}

export interface ICheckUserLoggedInAction extends IReduxBaseAction {
    type: ReduxActionTypes.CHECK_LOGGED_IN;
    data: IUserState;
}

export function userLogin(
    email: string, 
    password: string
    ) : ThunkAction<Promise<ILoginUserAction>, IUserState, undefined, ILoginUserAction> {
    return async (dispatch: ThunkDispatch<IUserState, undefined, ILoginUserAction>) => {
        //call service
        const myUser: IUserState = await userService.login(email, password);
        // let history = useHistory();
        // history.push("/Dashboard");
        return await dispatch({
            type: ReduxActionTypes.USER_LOGIN,
            data: myUser
        });
    }
}

export function checkLoggedIn() : ThunkAction<Promise<ICheckUserLoggedInAction>, IUserState, undefined, ICheckUserLoggedInAction> {
    return async (dispatch: ThunkDispatch<IUserState, undefined, ICheckUserLoggedInAction>) => {
        var localUser = localStorage.getItem('user');
        let myUser: IUserState = {
            email: "",
            name: "",
            token: ""
        };
        if(localUser != undefined && localUser != ""){
            myUser = JSON.parse(localUser);
        }
        return dispatch({
            type: ReduxActionTypes.CHECK_LOGGED_IN,
            data: myUser
        });
    }
}
