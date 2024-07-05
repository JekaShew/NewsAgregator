import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWSSTATUS_LOADALL_SUCCESS":
            return {
                ...state.newsStatuses,
                value: action.data,
            }

        default:
            return state
    }
}