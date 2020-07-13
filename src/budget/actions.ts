import { ThunkAction, ThunkDispatch } from "redux-thunk";

import { IBudgetState } from "./reducer";
import { ReduxActionTypes, IReduxBaseAction } from "../_store/rootReducer";

// import { userService } from "../_services/userService";

export interface IGetBudgetsAction extends IReduxBaseAction {
    type: ReduxActionTypes.GET_BUDGET_LIST;
    data: IBudgetState;
}

// export interface ICreateBudgetAction extends IReduxBaseAction {
//     type: ReduxActionTypes.CREATE_BUDGET;
//     data: IBudgetState;
// }

// export interface IUpdateBudgetAction extends IReduxBaseAction {
//     type: ReduxActionTypes.UPDATE_BUDGET;
//     data: IBudgetState;
// }

export function getBudgets(
    token: string
): ThunkAction<Promise<IGetBudgetsAction>, IBudgetState, undefined, IGetBudgetsAction> {
    return async (dispatch: ThunkDispatch<IBudgetState, undefined, IGetBudgetsAction>) => {
        //call service
        const budgetList: IBudgetState = { id: 0, isActive: false, name: "", isDefault: false } //= await userService.login(email, password);
        return await dispatch({
            type: ReduxActionTypes.GET_BUDGET_LIST,
            data: budgetList
        });
    }
}

// export function createAccount(): ThunkAction<Promise<ICreateAccountAction>, IAccountState, undefined, ICreateAccountAction> {
//     return async (dispatch: ThunkDispatch<IAccountState, undefined, ICreateAccountAction>) => {
//         var localUser = localStorage.getItem('user');
//         let acc: IAccountState = { id: 0, isActive: false, name: "", typeId: 0 };
        
//         return dispatch({
//             type: ReduxActionTypes.CREATE_ACCOUNT,
//             data: acc
//         });
//     }
// }

// export function updateAccount(): ThunkAction<Promise<IUpdateAccountAction>, IAccountState, undefined, IUpdateAccountAction> {
//     return async (dispatch: ThunkDispatch<IAccountState, undefined, IUpdateAccountAction>) => {
//         return await dispatch({
//             type: ReduxActionTypes.UPDATE_ACCOUNT,
//             data: { id: 0, isActive: false, name: "", typeId: 0 }
//         });
//     }
// }
