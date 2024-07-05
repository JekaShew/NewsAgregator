import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ACCOUNT_LOADALL_SUCCESS":
            return {
                ...state.accounts,
                value: action.data,
            }

        default:
            return state
    }
}