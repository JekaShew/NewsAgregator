import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHERSTATUS_LOADALL_SUCCESS":
            return {
                ...state.weatherStatuses,
                value: action.data,
            }

        default:
            return state
    }
}