import { TypeBase } from "./typeBase";

export interface User extends TypeBase {
    isLoggedIn: boolean;
    name?: string;
    token?: string;
}

export interface UserLoginRequest {
    email: string;
    password: string;
}