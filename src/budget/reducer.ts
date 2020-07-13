import { IGetBudgetsAction } from "./actions";
import { ReduxActionTypes } from "../_store/rootReducer";

export interface IBudgetState {
    id: number;
    name: string;
    isDefault: boolean;
    isActive: boolean;
}

export interface IBudgetListState {
    list: IBudgetState[];
}

const initialState: IBudgetState = {
    id: 0,
    name: "",
    isDefault: false,
    isActive: false
};

type BudgetReducerActions = IGetBudgetsAction;// | ICreateAccountAction | IUpdateAccountAction;

export default function (
    state: IBudgetState = initialState,
    action: BudgetReducerActions
) {
    switch (action.type) {
        case ReduxActionTypes.GET_BUDGET_LIST:
            return {
                ...state
            };
        // case ReduxActionTypes.CREATE_BUDGET:
        //     return {
        //         ...state
        //     };
        // case ReduxActionTypes.UPDATE_BUDGET:
        //     return {
        //         ...state
        //     };
        default:
            return state;
    }
}
