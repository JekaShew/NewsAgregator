import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWS_LOADALL_SUCCESS":
            return {
                ...state,
                    news:
                {
                    loading: false,

                        value: action.data.map(tpr => ({
                            ...Object.fromEntries(Object.entries(tpr).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                            newsStatus: {
                                ...Object.fromEntries(Object.entries(tpr.newsStatus).map(x => ([
                                    x[0],
                                    {
                                        value: x[1],
                                    },
                                ]))),
                            },
                        })),
                },
            }

        case "NEWS_REMOVE_START":
            return {
                ...state,
                news:
                {
                    ...state.news,
                    loading: true,
                }   
            }

        case "NEWS_LOADALL_START":
            return {
                ...state,
                news:
                {
                    ...state.news,
                    loading: true,   
                }
            }

        default:
            return state
    }
}