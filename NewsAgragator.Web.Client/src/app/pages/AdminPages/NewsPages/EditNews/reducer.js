import initialState from "../../../../../initialState";



export default (state = initialState.editNews, action) => {
    switch (action.type) {

        case "NEWS_SELECT":
            return {
                ...state,
                // editNews:
                // {
                //     ...state.editNews,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "NEWS_SELECT_PARAMETER":
            return {
                ...state,
                // editNews:
                // {
                //     ...state.editNews,
                    [action.name]:
                    {

                        id: action.val.value,
                        text: action.val.text,

                    },
                // }
            }

        case "NEWS_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editNews:
                // {
                    loadingParameters: true,
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    positiveRating: {
                        value: '',
                    },
                    newsStatus: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    newsStatuses: {
                        value: {
                            id: '',
                            text: '',
                        },
                        options: [
                            {
                                id: '',
                                text: ''
                            },
                        ],
                    },
                // }
            }

        case "NEWS_LOAD_PARAMETERS_SUCCESS":
            return {
                ...state,
                // editNews:
                // {
                //     ...state.editNews,
                    loadingParameters: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        }
                    ])))
                // }

            }

        case "NEWS_LOAD_START":
            return {
                ...state,
                // ...state.editNews,
                // editNews:
                // {
                    loadingData: true,

                // }
            }

        case "NEWS_LOAD_SUCCESS":
            return {
                ...state,
                // editNews:
                // {
                //     ...state.editNews,
                    ...state.newsStatuses,
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