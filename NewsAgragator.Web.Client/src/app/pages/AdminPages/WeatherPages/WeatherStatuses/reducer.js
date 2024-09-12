import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "WEATHERSTATUS_LOADALL_SUCCESS":
            return {
                ...state,
                weatherStatuses:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "WEATHERSTATUS_LOADALL_START":
            return {
                ...state,
                weatherStatuses:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                title: {
                                    value: '',
                                },
                                description: {
                                    value: '',
                                },
                            },
                        ],
                }
            }

        case "WEATHERSTATUS_REMOVE_START":
            return {
                ...state,
                weatherStatuses:
                {
                    ...state.weatherStatuses,
                    loading: true,
                }
            }

        default:
            return state
    }
}