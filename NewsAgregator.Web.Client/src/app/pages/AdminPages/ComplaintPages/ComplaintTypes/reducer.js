import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINTTYPE_LOADALL_SUCCESS":
            return {
                ...state.complaintTypes,
                value: action.data,
            }

        default:
            return state
    }
}