import { IGetAccountsAction, ICreateAccountAction, IUpdateAccountAction } from "./actions";
import { ReduxActionTypes } from "../_store/rootReducer";

export interface IAccountState {
    id: number;
    name: string;
    typeId: number;
    isActive: boolean;
}

const initialState: IAccountState = {
    id: 0,
    name: "",
    typeId: 0,
    isActive: false
};

type AccountReducerActions = IGetAccountsAction | ICreateAccountAction | IUpdateAccountAction;

export default function (
    state: IAccountState = initialState,
    action: AccountReducerActions
) {
    switch (action.type) {
        case ReduxActionTypes.GET_ACCOUNT_LIST:
            return {
                ...state
            };
        case ReduxActionTypes.CREATE_ACCOUNT:
            return {
                ...state
            };
        case ReduxActionTypes.UPDATE_ACCOUNT:
            return {
                ...state
            };
        default:
            return state;
    }
}
