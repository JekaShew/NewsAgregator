import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMMENT_LOADALL_SUCCESS":
            return {
                ...state.comments,
                value: action.data,
            }

        default:
            return state
    }
}