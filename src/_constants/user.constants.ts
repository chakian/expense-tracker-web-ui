import { UserLoginRequest, User } from "../_types/user";

export const LOGIN_REQUEST = 'USERS_LOGIN_REQUEST';
export const LOGOUT_REQUEST = 'USERS_LOGOUT';

interface LoginAction {
  type: typeof LOGIN_REQUEST;
  requestPayload: UserLoginRequest;
  responsePayload?: User;
}

interface LogoutAction {
  type: typeof LOGOUT_REQUEST;
}

export type UserActionTypes = LoginAction | LogoutAction;
