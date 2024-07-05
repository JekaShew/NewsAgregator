import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWS_LOADALL_SUCCESS":
            return {
                ...state.news,
                value: action.data,
            }

        default:
            return state
    }
}