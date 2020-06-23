import { ThunkAction, ThunkDispatch } from "redux-thunk";

import { IAccountState } from "./reducer";
import { ReduxActionTypes, IReduxBaseAction } from "../_store/rootReducer";

// import { userService } from "../_services/userService";

export interface IGetAccountsAction extends IReduxBaseAction {
    type: ReduxActionTypes.GET_ACCOUNT_LIST;
    data: IAccountState;
}

export interface ICreateAccountAction extends IReduxBaseAction {
    type: ReduxActionTypes.CREATE_ACCOUNT;
    data: IAccountState;
}

export interface IUpdateAccountAction extends IReduxBaseAction {
    type: ReduxActionTypes.UPDATE_ACCOUNT;
    data: IAccountState;
}

export function getAccounts(
    email: string,
    password: string
): ThunkAction<Promise<IGetAccountsAction>, IAccountState, undefined, IGetAccountsAction> {
    return async (dispatch: ThunkDispatch<IAccountState, undefined, IGetAccountsAction>) => {
        //call service
        const acc: IAccountState = { id: 0, isActive: false, name: "", typeId: 0 } //= await userService.login(email, password);
        return await dispatch({
            type: ReduxActionTypes.GET_ACCOUNT_LIST,
            data: acc
        });
    }
}

export function createAccount(): ThunkAction<Promise<ICreateAccountAction>, IAccountState, undefined, ICreateAccountAction> {
    return async (dispatch: ThunkDispatch<IAccountState, undefined, ICreateAccountAction>) => {
        var localUser = localStorage.getItem('user');
        let acc: IAccountState = { id: 0, isActive: false, name: "", typeId: 0 };
        
        return dispatch({
            type: ReduxActionTypes.CREATE_ACCOUNT,
            data: acc
        });
    }
}

export function updateAccount(): ThunkAction<Promise<IUpdateAccountAction>, IAccountState, undefined, IUpdateAccountAction> {
    return async (dispatch: ThunkDispatch<IAccountState, undefined, IUpdateAccountAction>) => {
        return await dispatch({
            type: ReduxActionTypes.UPDATE_ACCOUNT,
            data: { id: 0, isActive: false, name: "", typeId: 0 }
        });
    }
}
