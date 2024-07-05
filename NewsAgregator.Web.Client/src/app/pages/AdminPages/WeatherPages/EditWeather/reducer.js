import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHER_SELECT":
            return {
                ...state,
                editWeather:
                {
                    ...state.editWeather,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case 'WEATHER_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editWeather:
                {
                    ...state.editWeather,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editWeather[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "WEATHER_LOAD_SUCCESS":
            return {
                ...state,
                editWeather:
                {
                    ...state.editWeather,
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