import { ILoginUserAction, ICheckUserLoggedInAction } from "./actions";
import { ReduxActionTypes } from "../_store/rootReducer";

export interface IUserState {
    email: string;
    name: string;
    token: string;
}

const initialState: IUserState = {
    email: "",
    token: "",
    name: ""
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
                token: action.data.token
            };
        case ReduxActionTypes.CHECK_LOGGED_IN:
            return{
                ...state,
                email: action.data.email,
                name: action.data.name,
                token: action.data.token
            };
        default:
            return state;
    }
}
