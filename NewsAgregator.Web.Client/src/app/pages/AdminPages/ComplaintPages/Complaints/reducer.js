import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINT_LOADALL_SUCCESS":
            return {
                ...state.complaints,
                value: action.data,
            }

        default:
            return state
    }
}