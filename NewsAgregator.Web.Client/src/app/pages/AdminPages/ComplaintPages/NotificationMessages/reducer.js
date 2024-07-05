import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NOTIFICATIONMESSAGE_LOADALL_SUCCESS":
            return {
                ...state.notificationMessages,
                value: action.data,
            }

        default:
            return state
    }
}