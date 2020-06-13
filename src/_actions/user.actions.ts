import { userService } from '../_services/user.service';

import { UserLoginRequest, User } from '../_types/user';
import { UserActionTypes, LOGIN_REQUEST, LOGOUT_REQUEST } from '../_constants/user.constants';

// TypeScript infers that this function is returning SendMessageAction
export function login(loginUser: UserLoginRequest): UserActionTypes {
    return {
        type: LOGIN_REQUEST,
        requestPayload: loginUser
    }
}

export function logout(): UserActionTypes {
    userService.logout();
    return {
        type: LOGOUT_REQUEST
    };
}
