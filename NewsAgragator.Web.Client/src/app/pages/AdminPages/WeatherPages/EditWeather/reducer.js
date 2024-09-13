import initialState from "../../../../../initialState";



export default (state = initialState.editWeather, action) => {
    switch (action.type) {

        case "WEATHER_SELECT":
            return {
                ...state,
                // editWeather:
                // {
                //     ...state.editWeather,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "WEATHER_SELECT_PARAMETER":
            return {
                ...state,
                // editWeather:
                // {
                //     ...state.editWeather,
                    [action.name]:
                    {

                        id: action.val.value,
                        text: action.val.text,

                    },
                // }
            }

        case "WEATHER_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editWeather:
                // {
                    loadingParameters: true,
                    id: {
                        value: '',
                    },
                    city: {
                        value: '',
                    },
                    temperatureMorning: {
                        value: '',
                    },
                    temperatureDay: {
                        value: '',
                    },
                    temperatureEvening: {
                        value: '',
                    },
                    temperatureNight: {
                        value: '',
                    },
                    temperatureCommon: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    percipitaion: {
                        value: '',
                    },
                    wind: {
                        value: '',
                    },
                    windDirection: {
                        value: '',
                    },
                    pressure: {
                        value: '',
                    },
                    humidity: {
                        value: '',
                    },

                    weatherStatuses: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    weatherStatusMorning: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    weatherStatusDay: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    weatherStatusEvening: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    weatherStatusNight: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    weatherStatusCommon: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                // }
            }

        case 'WEATHER_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                // editWeather:
                // {
                //     ...state.editWeather,
                    
                    loadingParameters: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        }
                    ])))
                // }

            }

        case "WEATHER_LOAD_START":
            return {
                ...state,
                // ...state.editWeather,
                // editWeather:
                // {
                    loadingData: true,

                // }
            }

        case "WEATHER_LOAD_SUCCESS":
            return {
                ...state,
                // editWeather:
                // {
                //     ...state.editWeather,
                    ...state.weatherStatuses,
                    loadingData: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                // }
            }

        default:
            return state
    }
}