import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ACCOUNTSTATUS_LOADALL_SUCCESS":
            return {
                ...state.accountStatuses,
                value: action.data,
            }

        default:
            return state
    }
}