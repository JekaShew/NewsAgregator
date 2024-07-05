import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHER_LOADALL_SUCCESS":
            return {
                ...state.weathers,
                value: action.data,
            }

        default:
            return state
    }
}