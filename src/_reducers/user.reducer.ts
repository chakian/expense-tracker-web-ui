import { LOGIN_REQUEST, LOGOUT_REQUEST, UserActionTypes } from '../_constants/user.constants';
import { User } from '../_types/user';

// let user: User = JSON.parse(localStorage.getItem('user'));
let user: User = {isLoggedIn: false, isSuccess: true};
const initialState: User = user;

export function userReducer(
    state = initialState,
    action: UserActionTypes
): User {
    switch (action.type) {
        case LOGIN_REQUEST:
            return action.responsePayload ? action.responsePayload : { isLoggedIn: false, isSuccess: true };
        case LOGOUT_REQUEST:
            return {
                isLoggedIn: false,
                isSuccess: true
            }
        default:
            return state;
    }
}