import { SUCCESS, ERROR, CLEAR, AlertActionTypes } from '../_constants/alert.constants';

export function success(message: string): AlertActionTypes {
    return { type: SUCCESS, response: { isSuccess: true, message: message } };
}

export function error(message): AlertActionTypes {
    return { type: ERROR, response: { isSuccess: false, message: message} };
}

export function clear(): AlertActionTypes {
    return { type: CLEAR };
}
