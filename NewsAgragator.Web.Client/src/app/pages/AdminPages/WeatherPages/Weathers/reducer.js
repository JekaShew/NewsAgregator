import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHER_LOADALL_SUCCESS":
            return {
                ...state,
                weathers:
                {
                    loading: false,

                    value: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))),
                        weatherStatusCommon: {
                            ...Object.fromEntries(Object.entries(tpr.weatherStatusCommon).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        weatherStatusMorning: {
                            ...Object.fromEntries(Object.entries(tpr.weatherStatusMorning).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        weatherStatusDay: {
                            ...Object.fromEntries(Object.entries(tpr.weatherStatusDay).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        weatherStatusEvening: {
                            ...Object.fromEntries(Object.entries(tpr.weatherStatusEvening).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        weatherStatusNight: {
                            ...Object.fromEntries(Object.entries(tpr.weatherStatusNight).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                    })),
                },
            }

        case "WEATHER_REMOVE_START":
            return {
                ...state,
                weatehrs:
                {
                    ...state.weatehrs,
                    loading: true,
                }
            }

        case "WEATHER_LOADALL_START":
            return {
                ...state,
                weatehrs:
                {
                    ...state.weatehrs,
                    loading: true,
                }
            }

        default:
            return state
    }
}