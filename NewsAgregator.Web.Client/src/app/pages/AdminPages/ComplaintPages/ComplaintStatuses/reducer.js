import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINTSTATUS_LOADALL_SUCCESS":
            return {
                ...state.complaintStatuses,
                value: action.data,
            }

        default:
            return state
    }
}