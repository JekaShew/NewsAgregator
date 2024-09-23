import { error } from "jquery";
import initialState from "../../../../../initialState";

export default (state = initialState.signUP, action) => {
    switch (action.type) {

        case "SIGNUP_SELECT":
            return {
                ...state,
               
                [action.name]:
                {
                    value: action.val,
                },
               
            }

        // case "SIGNUP_LOAD_START":
        // return {
        //     ...state,
        //     loading: true,
        // }

        case "AUTHORIZATION_SIGN_UP_FAIL":
        return {
            ...state,
            error: action.error,
            
        }

        case "AUTHORIZATION_SIGN_UP_START":
        return {
            ...state,
            loading: true,
            
            
        }

            default:
                return state
        }
    }