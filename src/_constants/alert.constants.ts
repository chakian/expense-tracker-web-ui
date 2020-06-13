import { TypeBase } from "../_types/typeBase";

export const SUCCESS = 'ALERT_SUCCESS';
export const ERROR = 'ALERT_ERROR';
export const CLEAR = 'ALERT_CLEAR';

interface SuccessAction {
    type: typeof SUCCESS;
    response: TypeBase;
}

interface ErrorAction {
    type: typeof ERROR;
    response: TypeBase;
}

interface ClearAction {
    type: typeof CLEAR;
}

export type AlertActionTypes = SuccessAction | ErrorAction | ClearAction;
