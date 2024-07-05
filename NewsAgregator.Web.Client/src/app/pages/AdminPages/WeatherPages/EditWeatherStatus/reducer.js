import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHERSTATUS_SELECT":
            return {
                ...state,
                editWeatherStatus:
                {
                    ...state.editWeatherStatus,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "WEATHERSTATUS_LOAD_SUCCESS":
            return {
                ...state,
                editWeatherStatus:
                {
                    ...state.editWeatherStatus,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}