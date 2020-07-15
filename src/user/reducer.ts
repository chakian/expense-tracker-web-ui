import { ILoginUserAction, ICheckUserLoggedInAction } from "./actions";
import { ReduxActionTypes } from "../_store/rootReducer";

export interface IUserState {
    email: string;
    name: string;
    token: string;
    defaultBudgetId: number;
}

const initialState: IUserState = {
    email: "",
    token: "",
    name: "",
    defaultBudgetId: 0
};

type UserReducerActions = ILoginUserAction | ICheckUserLoggedInAction;

export default function(
    state: IUserState = initialState,
    action: UserReducerActions
) {
    switch (action.type) {
        case ReduxActionTypes.USER_LOGIN:
            return {
                ...state,
                email: action.data.email,
                name: action.data.name,
                token: action.data.token,
                defaultBudgetId: action.data.defaultBudgetId
            };
        case ReduxActionTypes.CHECK_LOGGED_IN:
            return{
                ...state,
                email: action.data.email,
                name: action.data.name,
                token: action.data.token,
                defaultBudgetId: action.data.defaultBudgetId
            };
        default:
            return state;
    }
}
