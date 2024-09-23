import { error } from "jquery";
import initialState from "../../../../../initialState";


export default (state = initialState.signIN, action) => {
    switch (action.type) {

        case "SIGNIN_SELECT":
            return {
                ...state,
                
                [action.name]:
                {
                    value: action.val,
                },
                
            }

        case "AUTHORIZATION_SIGN_IN_SUCCESS":
        return {
            
            isSuccess: true,

        }

        case "AUTHORIZATION_SIGN_IN_FAIL":
        return {
            
            isSuccess: false,
            error: "Authorization Failed!"

        }

            

        default:
            return state
    }
}