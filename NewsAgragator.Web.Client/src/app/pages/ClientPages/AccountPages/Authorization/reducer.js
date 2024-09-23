import initialState from "../../../../../initialState";

export default ( state = initialState.authorization, action) => {
    switch (action.type) {
        case "AUTHORIZATION_SIGN_IN_SUCCESS":
            return {
               
                ...state,
                atoken: action.data.atoken,
                rtoken: action.data.rtoken,
                userName: action.data.userName,
                role: action.data.role
                            
            }
        case "AUTHORIZATION_SIGN_OUT":
            return {
                ...state,
                atoken: null,
                rtoken: null,
                userName: null,
                role: null
            }
        default:
            return state
    }
}