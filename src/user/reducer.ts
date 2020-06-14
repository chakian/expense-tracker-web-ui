import { ILoginUserAction } from "./actions";
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

type UserReducerActions = ILoginUserAction;// | UserLogoutAction;

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
        default:
            return state;
    }
}
